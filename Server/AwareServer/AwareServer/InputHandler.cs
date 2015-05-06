using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using AwareClassLibrary;
using Newtonsoft.Json;
using Repository.Model;

namespace AwareServer
{
    class InputHandler
    {
        public Service service = new Service();
        ExceptionLog exceptionLog = null;
        string ret = "";

        public string GetReturnString(string content)
        {   
            try
            {
                #region Get
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

                        else if (content.Equals("GET/orders"))
                        {
                            IEnumerable<Order> orders = service.GetOrders();
                            foreach (Order order in orders)
                            {
                                ret = JsonConvert.SerializeObject(orders);
                            }
                        }
                        else
                        {
                            ret = "Input string is not correctly formatted.";
                            throw new System.ArgumentException("Input string is not correctly formatted.");
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
                            ret = "Input string is not correctly formatted.";
                            throw new System.ArgumentException("Input string is not correctly formatted.");
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
                            ret = "Input string is not correctly formatted.";
                            throw new System.ArgumentException("Input string is not correctly formatted.");
                        }
                    }
                    else
                    {
                        ret = "Input string is not correctly formatted.";
                        throw new System.ArgumentException("Input string is not correctly formatted.");
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
                        if (content.IndexOf("inventory") > -1)
                        {
                            json = content.Replace("PUT/products/inventory/json=", "");
                            Product result = JsonConvert.DeserializeObject<Product>(json);
                            service.ProductInventory(result);
                        }
                        else
                        {
                            json = content.Replace("PUT/products/json=", "");
                            Product result = JsonConvert.DeserializeObject<Product>(json);
                            service.UpdateProduct(result);
                        }
                    }

                    else if (content.IndexOf("orders") > -1)
                    {
                        json = content.Replace("PUT/orders/json=", "");
                        Order result = JsonConvert.DeserializeObject<Order>(json);
                        service.UpdateOrder(result);
                    }

                    else
                    {
                        ret = "Input string is not correctly formatted.";
                        throw new System.ArgumentException("Input string is not correctly formatted.");
                    }
                }
                else
                {
                    ret = "Input string is not correctly formatted.";
                    throw new System.ArgumentException("Input string is not correctly formatted.");
                }
                #endregion
            }

            catch (Exception e)
            {
                exceptionLog = new ExceptionLog(0, e.GetType().ToString(), e.Message, e.Source, e.StackTrace);
                service.InsertException(exceptionLog);
            }
 
            return ret;
        }
    }
}
