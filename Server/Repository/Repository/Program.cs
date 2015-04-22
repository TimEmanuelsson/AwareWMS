using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
//using async_server;
namespace Repository
{
    // State object for reading client data asynchronously
    public class StateObject
    {
        // Client  socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 1024;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }

    public class AsynchronousSocketListener
    {
        // Thread signal.
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public AsynchronousSocketListener()
        {
        }

        public static void StartListening()
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.
            // The DNS name of the computer
            // running the listener is "host.contoso.com".
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            //IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPAddress ipAddress = IPAddress.Parse("194.47.106.137");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            allDone.Set();

            // Get the socket that handles the client request.
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            Model.Service service = new Model.Service();
            String content = String.Empty;
            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            int bytesRead = 0;
            try
            {
                // Read data from the client socket. 
                bytesRead = handler.EndReceive(ar);
            }
            catch (SocketException e)
            {
                Console.WriteLine("[ERROR]: " + e);
            }
            

            if (bytesRead > 0)
            {
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                content = state.sb.ToString();
                Console.WriteLine("Recieved string: {0}", content);

                // Checks the recieved strings for what to return to the client.
                try
                {
                    // GET
                    if (content.IndexOf("GET") > -1)
                    {
                        if (content.IndexOf("GET/orders") > -1)
                        {
                            if (content.IndexOf("GET/orders/id=") > -1)
                            {
                                string id = content.Replace("GET/orders/id=", "");
                                Model.Order order = service.GetOrderById(int.Parse(id));
                                string json = "";
                                //string ret = String.Format("ID: {0} Customer ID: {1} Date: {2} Last update: {3} Payment status: {4} Payment method: LÄGG TILL DÅ"
                                //    , order.OrderId, order.CustomerId, order.Date, order.LastUpdate, order.PaymentStatus);
                                JavaScriptSerializer serializer = new JavaScriptSerializer();
                                json += serializer.Serialize(order);
                                Send(handler, json);
                            }
                            //else if (content.IndexOf("customer/id") > -1)
                            //{
                            //    string id = content.Replace("GET/orders/customer/id=", "");
                            //    Model.Customer customer = service.GetCustomerById(int.Parse(id));
                            //    string ret = String.Format("ID: {0} First name: {1} Last name: {2} Address line 1: {3} Address line 2: {4} City: {5} Region: {6} Zip Code: {7} Country: {8} Phone Number: {9} E-mail: {10}"
                            //        , customer.CustomerId, customer.FirstName, customer.LastName, customer.AddressLine1, customer.AddressLine2, customer.City, customer.Region, customer.ZipCode, customer.PhoneNumber, customer.Email);
                            //    Send(handler, ret);
                            //}
                            else if (content.IndexOf("GET/orders") > -1)
                            {
                                IEnumerable<Model.Order> orders = service.GetOrders();

                                string json = "";
                                int i = 0;

                                foreach (Model.Order order in orders)
                                {
                                    if (i > 0)
                                    {
                                        json += "#";
                                    }
                                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                                    json += serializer.Serialize(order);

                                    i++;
                                }

                                Send(handler, json);
                            }
                            else
                            {
                                string ret = "An error has occured! Format errors in the call to the server.";
                                Send(handler, ret);
                            }
                        }

                        else if (content.IndexOf("GET/products") > -1)
                        {
                            if (content.IndexOf("GET/products/id=") > -1)
                            {
                                string id = content.Replace("GET/products/id=", "");
                                string json = "";
                                Repository.Model.Product product = service.GetProductById(int.Parse(id));
                                //string ret = String.Format("ID: {0} Name: {1} SKU: {2} Quantity: {3} Weight: {4} Shelf: {5}  Barcode Number: {6} Image URL: {7}"
                                //    , product.ProductId, product.Name, product.SKU, product.Quantity, product.Weight, product.StorageSpace, product.BarcodeNumber, product.ImageLocation);
                                JavaScriptSerializer serializer = new JavaScriptSerializer();
                                json += serializer.Serialize(product);
                                Send(handler, json);
                            }
                            else if (content.IndexOf("GET/products/sku=") > -1)
                            {
                                string sku = content.Replace("GET/products/sku=", "");
                                string json = "";
                                Model.Product product = service.GetProductBySKU(int.Parse(sku));
                                JavaScriptSerializer serializer = new JavaScriptSerializer();
                                json += serializer.Serialize(product);
                                Send(handler, json);
                                //GetProductBySKU(int.Parse(sku), handler);
                            }
                            else if (content.IndexOf("GET/products/barcodenumber=") > -1)
                            {
                                string barcodenumber = content.Replace("GET/products/barcodenumber=", "");
                                string json = "";
                                Model.Product product = service.GetProductByBarcodeNumber(int.Parse(barcodenumber));
                                JavaScriptSerializer serializer = new JavaScriptSerializer();
                                json += serializer.Serialize(product);
                                Send(handler, json);
                            }
                            else if (content.IndexOf("GET/products") > -1)
                            {
                                IEnumerable<Model.Product> products = service.GetProducts();

                                string json = "";
                                int i = 0;

                                foreach (Model.Product product in products)
                                {
                                    if (i > 0)
                                    {
                                        json += "#";
                                    }
                                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                                    json += serializer.Serialize(product);

                                    i++;
                                }

                                Send(handler, json);
                            }
                            else
                            {
                                string ret = "An error has occured! Format errors in the call to the server.";
                                Send(handler, ret);
                            }
                        }

                        // Customers
                        else if (content.IndexOf("GET/customers") > -1)
                        {
                            if (content.IndexOf("GET/customers/id=") > -1)
                            {
                                string id = content.Replace("GET/customers/id=", "");
                                string json = "";
                                Repository.Model.Customer customer = service.GetCustomerById(int.Parse(id));
                                JavaScriptSerializer serializer = new JavaScriptSerializer();
                                json += serializer.Serialize(customer);
                                Send(handler, json);
                            }
                            //else if (content.IndexOf("name") > -1)
                            //{
                            //    char[] splitChar = { '_' };
                            //    string name = content.Replace("GET/customers/name=", "");
                            //    string[] nameSplit = name.Split(splitChar);
                            //    string firstname = nameSplit[0];
                            //    string lastname = nameSplit[1];
                            //    GetCustomerByName(firstname, lastname, handler);
                            //}
                            else
                            {
                                string ret = "An error has occured! Format errors in the call to the server.";
                                Send(handler, ret);
                            }
                        }
                        else
                        {
                            string ret = "An error has occured! Format errors in the call to the server.";
                            Send(handler, ret);
                        }
                    }
                    
                }

                catch (ApplicationException e)
                {
                    string error = "[ERROR]: " + e;
                    Console.WriteLine(error);
                    Send(handler, "An error with the application has occured!");
                }
                catch (FormatException e) 
                {
                    string error = "[ERROR]: " + e;
                    Console.WriteLine(error);
                    string ret = "An error has occured! Format errors in the call to the server.";
                    Send(handler, ret);
                }

                //// PUT
                //else if (content.IndexOf("PUT") > -1)
                //{

                //    if (content.IndexOf("products") > -1)
                //    {
                //        JavaScriptSerializer serializer = new JavaScriptSerializer();
                //        string json = content.Replace("PUT/products/json=", "");
                //        Repository.Model.Product result = serializer.Deserialize<Repository.Model.Product>(json);
                //        Console.WriteLine("ID: {0} Name: {1} SKU: {2} Quantity: {3} Weight: {4} Shelf: {5} Barcode Number: {6} Image URL: {7}"
                //            , result.ProductId, result.Name, result.SKU, result.Quantity, result.Weight, result.StorageSpace, result.BarcodeNumber, result.ImageLocation);
                //        Send(handler, json);
                //    }
                //    else if (content.IndexOf("customers") > -1)
                //    {
                //        JavaScriptSerializer serializer = new JavaScriptSerializer();
                //        string json = content.Replace("PUT/customers/json=", "");
                //        //Customer result = serializer.Deserialize<Customer>(json);
                //        //Console.WriteLine("ID: {0} Name: {1} Address: {2} Phone number: {3} E-mail: {4}"
                //        //    , result.ID, result.Name, result.Address, result.Phone, result.Email);
                //        Send(handler, json);
                //    }
                //}

                //else
                //{
                //    // Not all data received. Get more.
                //    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                //    new AsyncCallback(ReadCallback), state);
                //}
            }
        }

        // Returns to client.
        private static void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            Console.WriteLine("Sent data: {0}", data);
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            try
            {
                handler.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), handler);
            }
            catch (SocketException e)
            {
                Console.WriteLine("[ERROR]: " + e);
            }
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        public static int Main(String[] args)
        {
            StartListening();
            return 0;
        }
    }
}