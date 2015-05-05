using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using AwareClassLibrary;
using Newtonsoft.Json;
//using Repository.Model;

namespace AwareServer
{
    class InputHandler
    {
        public string GetReturnString(string content)
        {
            Repository.Model.Service service = new Repository.Model.Service();
            string ret = "";
            #region Get
            // GET
            if (content.IndexOf("GET") > -1)
            {
                if (content.IndexOf("GET/orders") > -1)
                {
                    if (content.IndexOf("GET/orders/id=") > -1)
                    {
                        string id = content.Replace("GET/orders/id=", "");
                        //Repository.Model.Order order = service.GetOrderById(1);
                        AwareClassLibrary.Order order = service.GetOrderById(int.Parse(id));
                        //string ret = String.Format("ID: {0} Customer ID: {1} Date: {2} Last update: {3} Payment status: {4} Payment method: LÄGG TILL DÅ"
                        //    , order.OrderId, order.CustomerId, order.Date, order.LastUpdate, order.PaymentStatus);
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        ret += serializer.Serialize(order);
                    }

                    else if (content.IndexOf("GET/orders") > -1)
                    {
                        IEnumerable<AwareClassLibrary.Order> orders = service.GetOrders();

                        int i = 0;

                        foreach (AwareClassLibrary.Order order in orders)
                        {
                            if (i > 0)
                            {
                                ret += "";
                            }
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            ret += serializer.Serialize(order);

                            i++;
                        }

                    }
                    else
                    {
                        // TODO: Kasta exception
                        ret = "An error has occured! Format errors in the call to the server.";
                    }
                }

                else if (content.IndexOf("GET/products") > -1)
                {
                    if (content.IndexOf("GET/products/id=") > -1)
                    {
                        string id = content.Replace("GET/products/id=", "");
                        AwareClassLibrary.Product product = service.GetProductById(int.Parse(id));
                        //string ret = String.Format("ID: {0} Name: {1} SKU: {2} Quantity: {3} Weight: {4} Shelf: {5}  Barcode Number: {6} Image URL: {7}"
                        //    , product.ProductId, product.Name, product.SKU, product.Quantity, product.Weight, product.StorageSpace, product.BarcodeNumber, product.ImageLocation);
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        ret += serializer.Serialize(product);
                    }
                    else if (content.IndexOf("GET/products/sku=") > -1)
                    {
                        string sku = content.Replace("GET/products/sku=", "");
                        AwareClassLibrary.Product product = service.GetProductBySKU(int.Parse(sku));
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        ret += serializer.Serialize(product);
                        //GetProductBySKU(int.Parse(sku), handler);
                    }
                    else if (content.IndexOf("GET/products/barcodenumber=") > -1)
                    {
                        string barcodenumber = content.Replace("GET/products/barcodenumber=", "");
                        AwareClassLibrary.Product product = service.GetProductByBarcodeNumber(int.Parse(barcodenumber));
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        ret += serializer.Serialize(product);
                    }
                    else if (content.IndexOf("GET/products") > -1)
                    {
                        IEnumerable<AwareClassLibrary.Product> products = service.GetProducts();

                        int i = 0;

                        foreach (AwareClassLibrary.Product product in products)
                        {
                            /*
                            if (i > 0)
                            {
                                ret += "";
                            }
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            ret += serializer.Serialize(product);

                            i++;
                             * */

                            ret = JsonConvert.SerializeObject(products);
                                
                        }

                    }
                    else
                    {
                        // TODO: Kasta exception
                        ret = "An error has occured! Format errors in the call to the server.";
                    }
                }

                // Customers
                else if (content.IndexOf("GET/customers") > -1)
                {
                    if (content.IndexOf("GET/customers/id=") > -1)
                    {
                        string id = content.Replace("GET/customers/id=", "");
                        AwareClassLibrary.Customer customer = service.GetCustomerById(int.Parse(id)) as AwareClassLibrary.Customer;
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        ret += serializer.Serialize(customer);
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
                        // TODO: Kasta exception
                        ret = "An error has occured! Format errors in the call to the server.";
                    }
                }
                else
                {
                    // TODO: Kasta exception
                    ret = "An error has occured! Format errors in the call to the server.";
                }
            }
            #endregion
            #region Put
            // PUT
            else if (content.IndexOf("PUT") > -1)
            {
                string json = "";

                if (content.IndexOf("products") > -1)
                {
                    try
                    {
                        json = content.Replace("PUT/products/json=", "");
                        Product result = JsonConvert.DeserializeObject<Product>(json);
                        service.UpdateProduct(result);
                    }
                    catch (Exception e)
                    {
                        //Kasta något JSON-exception
                    }
                }

                else if (content.IndexOf("orders") > -1)
                {
                    try
                    {
                        json = content.Replace("PUT/orders/json=", "");
                        Order result = JsonConvert.DeserializeObject<Order>(json);
                        service.UpdateOrder(result);
                    }
                    catch (Exception e)
                    {
                        //Kasta något JSON-exception
                    }
                }
                return "";
            }
            #endregion
 
            return ret;
        }
    }
}
