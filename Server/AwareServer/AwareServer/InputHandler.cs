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
                        Order order = service.GetOrderById(int.Parse(id));
                        ret = JsonConvert.SerializeObject(order);
                    }

                    else if (content.IndexOf("GET/orders") > -1)
                    {
                        IEnumerable<Order> orders = service.GetOrders();
                        foreach (Order order in orders)
                        {
                            ret = JsonConvert.SerializeObject(orders);
                        }
                    }
                    else
                    {
                        // TODO: Kasta exception
                        //ExceptionLog exceptionLog = new ExceptionLog(0, "Type", "Invalid call string from the client", "", "");
                        //service.InsertException(exceptionLog);
                        ret = "An error has occured! Format errors in the call to the server.";
                    }
                }

                else if (content.IndexOf("GET/products") > -1)
                {
                    if (content.IndexOf("GET/products/id=") > -1)
                    {
                        string id = content.Replace("GET/products/id=", "");
                        Product product = service.GetProductById(int.Parse(id));
                        ret = JsonConvert.SerializeObject(product);
                    }
                    else if (content.IndexOf("GET/products/sku=") > -1)
                    {
                        string sku = content.Replace("GET/products/sku=", "");
                        Product product = service.GetProductBySKU(int.Parse(sku));
                        ret = JsonConvert.SerializeObject(product);
                    }
                    else if (content.IndexOf("GET/products/barcodenumber=") > -1)
                    {
                        string barcodenumber = content.Replace("GET/products/barcodenumber=", "");
                        Product product = service.GetProductByBarcodeNumber(int.Parse(barcodenumber));
                        ret = JsonConvert.SerializeObject(product);
                    }
                    else if (content.IndexOf("GET/products") > -1)
                    {
                        IEnumerable<Product> products = service.GetProducts();

                        foreach (Product product in products)
                        {
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
                        Customer customer = service.GetCustomerById(int.Parse(id));
                        ret += JsonConvert.SerializeObject(customer);
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
