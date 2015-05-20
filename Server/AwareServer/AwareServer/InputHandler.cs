using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Drawing;
using AwareClassLibrary;
using Newtonsoft.Json;
using Repository.Model;
using System.Windows.Media.Imaging;
using System.IO;
using System.Security.Cryptography;

namespace AwareServer
{
    class InputHandler
    {
        public Service service = new Service();
        ExceptionLog exceptionLog = null;
        string ret = "";
        byte[] retByte = new Byte[10024];
        char[] split = { '=' };

        public byte[] GetReturnBytes(string content)
        {   
            try
            {
                if (content.IndexOf("pw") > -1)
                {
                    string[] splitString = content.Split(split);
                    int i = -1;

                    foreach (string str in splitString)
                    {
                        i++;
                    }

                    splitString[i] = splitString[i].Replace("\n", "");

                    if (service.Authenticate(splitString[i]))
                    {
                        string passwordString = String.Format("/pw={0}", splitString[i]);
                        content = content.Replace(passwordString, "");
                        #region Get
                        if (content.IndexOf("GET") > -1)
                        {
                            if (content.IndexOf("GET/orders") > -1)
                            {
                                if (content.IndexOf("GET/orders/id=") > -1)
                                {
                                    string id = content.Replace("GET/orders/id=", "");
                                    id = id.Replace("\n", "");
                                    Order order = service.GetOrderById(int.Parse(id));
                                    ret = JsonConvert.SerializeObject(order);
                                    retByte = Encoding.UTF8.GetBytes(ret);
                                }
                                else if (content.IndexOf("GET/orders/rows/id=") > -1)
                                {
                                    string id = content.Replace("GET/orders/rows/id=", "");
                                    id = id.Replace("\n", "");
                                    IEnumerable<OrderRow> orderRows = service.GetOrderRowsByOrderId(int.Parse(id));
                                    ret = JsonConvert.SerializeObject(orderRows);
                                    retByte = Encoding.UTF8.GetBytes(ret);
                                }
                                else if (content.IndexOf("GET/orders/status/id=") > -1)
                                {
                                    string id = content.Replace("GET/orders/status/id=", "");
                                    id = id.Replace("\n", "");
                                    OrderStatus status = service.GetOrderStatusByOrderId(int.Parse(id));
                                    ret = JsonConvert.SerializeObject(status);
                                    retByte = Encoding.UTF8.GetBytes(ret);
                                }
                                else if (content.IndexOf("GET/orderstatus/id=") > -1)
                                {
                                    string id = content.Replace("GET/orderstatus/id=", "");
                                    id = id.Replace("\n", "");
                                    OrderStatus status = service.GetOrderStatusById(int.Parse(id));
                                    ret = JsonConvert.SerializeObject(status);
                                    retByte = Encoding.UTF8.GetBytes(ret);
                                }
                                else if (content.IndexOf("GET/orders") > -1)
                                {
                                    IEnumerable<Order> orders = service.GetOrders();
                                    ret = JsonConvert.SerializeObject(orders);
                                    retByte = Encoding.UTF8.GetBytes(ret);
                                }
                                else
                                {
                                    ret = "Input string is not correctly formatted.";
                                    retByte = Encoding.UTF8.GetBytes(ret);
                                    throw new System.ArgumentException("Input string is not correctly formatted.");
                                }
                            }

                            else if (content.IndexOf("GET/orderrows/id=") > -1)
                            {
                                string id = content.Replace("GET/orderrows/id=", "");
                                id = id.Replace("\n", "");
                                OrderRow row = service.GetOrderRowById(int.Parse(id));
                                ret = JsonConvert.SerializeObject(row);
                                retByte = Encoding.UTF8.GetBytes(ret);
                            }

                            else if (content.IndexOf("GET/products") > -1)
                            {
                                if (content.IndexOf("GET/products/id=") > -1)
                                {
                                    string id = content.Replace("GET/products/id=", "");
                                    id = id.Replace("\n", "");
                                    Product product = service.GetProductById(int.Parse(id));
                                    ret = JsonConvert.SerializeObject(product);
                                    retByte = Encoding.UTF8.GetBytes(ret);
                                }
                                else if (content.IndexOf("GET/products/sku=") > -1)
                                {
                                    string sku = content.Replace("GET/products/sku=", "");
                                    sku = sku.Replace("\n", "");
                                    Product product = service.GetProductBySKU(int.Parse(sku));
                                    ret = JsonConvert.SerializeObject(product);
                                    retByte = Encoding.UTF8.GetBytes(ret);
                                }
                                else if (content.IndexOf("GET/products/ean=") > -1)
                                {
                                    string ean = content.Replace("GET/products/ean=", "");
                                    ean = ean.Replace("\n", "");
                                    Product product = service.GetProductByEAN(int.Parse(ean));
                                    ret = JsonConvert.SerializeObject(product);
                                    retByte = Encoding.UTF8.GetBytes(ret);
                                }
                                else if (content.IndexOf("GET/products/count") > -1)
                                {
                                    ret = String.Format("{0}", service.GetProductCount().ToString());
                                    retByte = Encoding.UTF8.GetBytes(ret);
                                }
                                else if (content.IndexOf("GET/products/quantitysum") > -1)
                                {
                                    ret = String.Format("{0}", service.GetProductsQuantitySum().ToString());
                                    retByte = Encoding.UTF8.GetBytes(ret);
                                }
                                else if (content.IndexOf("GET/products/status/id=") > -1)
                                {
                                    string id = content.Replace("GET/products/status/id=", "");
                                    id = id.Replace("\n", "");
                                    int status = service.CheckIfProductBusy(int.Parse(id));
                                    if (status == 0)
                                    {
                                        ret = String.Format("Idle");
                                    }
                                    if (status > 0)
                                    {
                                        ret = String.Format("Busy");
                                    }
                                    retByte = Encoding.UTF8.GetBytes(ret);
                                }
                                else if (content.IndexOf("GET/products/image=") > -1)
                                {
                                    string imageString = content.Replace("GET/products/image=", "");
                                    imageString = imageString.Replace("\n", "");
                                    string imgUrl = imageString;
                                    Bitmap tImage = new Bitmap(imgUrl);
                                    retByte = imageToByte(tImage, tImage.RawFormat);
                                }
                                else if (content.Equals("GET/products"))
                                {
                                    IEnumerable<Product> products = service.GetProducts();
                                    ret = JsonConvert.SerializeObject(products);
                                    retByte = Encoding.UTF8.GetBytes(ret);
                                }

                                else
                                {
                                    ret = "Input string is not correctly formatted.";
                                    retByte = Encoding.UTF8.GetBytes(ret);
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
                                    retByte = Encoding.UTF8.GetBytes(ret);
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
                                    retByte = Encoding.UTF8.GetBytes(ret);
                                    throw new System.ArgumentException("Input string is not correctly formatted.");
                                }
                            }
                            else
                            {
                                ret = "Input string is not correctly formatted.";
                                retByte = Encoding.UTF8.GetBytes(ret);
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
                                if (content.IndexOf("status") > -1)
                                {
                                    string status = content.Replace("PUT/orders/id=", "");
                                    char[] splitStr = { '/' };
                                    string[] values = status.Split(splitStr);
                                    int orderId = int.Parse(values[0]);
                                    status = values[1].Replace("status=", "");
                                    int statusId = int.Parse(status);
                                    service.UpdateOrderStatus(orderId, statusId);

                                }
                                else
                                {
                                    json = content.Replace("PUT/orders/json=", "");
                                    Order result = JsonConvert.DeserializeObject<Order>(json);
                                    service.UpdateOrder(result);
                                }
                            }

                            else
                            {
                                ret = "Input string is not correctly formatted.";
                                retByte = Encoding.UTF8.GetBytes(ret);
                                throw new System.ArgumentException("Input string is not correctly formatted.");
                            }
                        }
                        else
                        {
                            ret = "Input string is not correctly formatted.";
                            retByte = Encoding.UTF8.GetBytes(ret);
                            throw new System.ArgumentException("Input string is not correctly formatted.");
                        }
                        #endregion
                    }
                    else
                    {
                        throw new InvalidDataException("Wrong password!");
                    }
                }
                else
                {
                    throw new System.ArgumentException("Input string is missing the password parameter.");
                }
            }

            catch (Exception e)
            {
                exceptionLog = new ExceptionLog(0, e.GetType().ToString(), e.Message, e.Source, e.StackTrace);
                service.InsertException(exceptionLog);
            }
 
            return retByte;
        }

        public byte[] imageToByte(System.Drawing.Image imageIn, System.Drawing.Imaging.ImageFormat format)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, format);
            return ms.ToArray();
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
