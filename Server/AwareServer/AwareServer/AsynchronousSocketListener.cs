using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Repository.Model;
using AwareClassLibrary;
using System.IO;
using System.Drawing;

namespace AwareServer
{
    public class AsynchronousSocketListener
    {
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public static Service service = new Service();

        public static void StartListening()
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[10024];

            // Establish the local endpoint for the socket.
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Parse(Settings.Default.IpAddress);
            int port = Settings.Default.Port;
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

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

                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait for a connection.
                    allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                ExceptionLog log = new ExceptionLog(0, e.GetType().ToString(), e.Message, e.Source, e.StackTrace);
                service.InsertException(log);
            }
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
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            String content = String.Empty;
            int bytesRead = handler.EndReceive(ar);
        
            if (bytesRead > 0)
            {
                state.sb.Append(Encoding.UTF8.GetString(
                    state.buffer, 0, bytesRead));

                content = state.sb.ToString();
            }

            try
            {
                InputHandler inputHandler = new InputHandler();
                byte[] returnStr = inputHandler.GetReturnBytes(content);
                Send(handler, returnStr);
            }
            catch (Exception e)
            {
                ExceptionLog log = new ExceptionLog(0, e.GetType().ToString(), e.Message, e.Source, e.StackTrace);
                service.InsertException(log);
            }
        }


        // Returns to client.
        private static void Send(Socket handler, byte[] data)
        {
            // Convert the string data to byte data using ASCII encoding.
            //byte[] byteData = Encoding.UTF8.GetBytes(data);

            // Begin sending the data to the remote device.
            try
            {
                handler.BeginSend(data, 0, data.Length, 0,
                    new AsyncCallback(SendCallback), handler);
            }
            catch (SocketException e)
            {
                ExceptionLog log = new ExceptionLog(0, e.GetType().ToString(), e.Message, e.Source, e.StackTrace);
                service.InsertException(log);
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

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                ExceptionLog log = new ExceptionLog(0, e.GetType().ToString(), e.Message, e.Source, e.StackTrace);
                service.InsertException(log);
            }
        }


        public static int Start()
        {
            StartListening();
            return 0;
        }
    }
}
