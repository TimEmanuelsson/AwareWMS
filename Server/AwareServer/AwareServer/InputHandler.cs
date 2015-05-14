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

namespace AwareServer
{
    class InputHandler
    {
        public Service service = new Service();
        ExceptionLog exceptionLog = null;
        string ret = "";
        byte[] retByte = new Byte[10024];

        public byte[] GetReturnBytes(string content)
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
                            retByte = Encoding.UTF8.GetBytes(ret);
                        }
                        else if (content.IndexOf("GET/orders/rows/id=") > -1)
                        {
                            string id = content.Replace("GET/orders/rows/id=", "");
                            IEnumerable<OrderRow> orderRows = service.GetOrderRowsByOrderId(int.Parse(id));
                            ret = JsonConvert.SerializeObject(orderRows);
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

                    else if (content.IndexOf("GET/products") > -1)
                    {
                        if (content.IndexOf("GET/products/id=") > -1)
                        {
                            string id = content.Replace("GET/products/id=", "");
                            Product product = service.GetProductById(int.Parse(id));
                            ret = JsonConvert.SerializeObject(product);
                            retByte = Encoding.UTF8.GetBytes(ret);
                        }
                        else if (content.IndexOf("GET/products/sku=") > -1)
                        {
                            string sku = content.Replace("GET/products/sku=", "");
                            Product product = service.GetProductBySKU(int.Parse(sku));
                            ret = JsonConvert.SerializeObject(product);
                            retByte = Encoding.UTF8.GetBytes(ret);
                        }
                        else if (content.IndexOf("GET/products/ean=") > -1)
                        {
                            string barcodenumber = content.Replace("GET/products/ean=", "");
                            Product product = service.GetProductByEAN(int.Parse(barcodenumber));
                            ret = JsonConvert.SerializeObject(product);
                            retByte = Encoding.UTF8.GetBytes(ret);
                        }
                        else if (content.IndexOf("GET/products/count") > -1)
                        {
                            ret = String.Format("{0}", service.GetProductCount().ToString());
                            retByte = Encoding.UTF8.GetBytes(ret);
                        }
                        else if (content.IndexOf("GET/products/image/id=") > -1)
                        {
                            string id = content.Replace("GET/products/image/id=", "");
                            Product product = service.GetProductById(int.Parse(id));
                            string imgUrl = product.ImageLocation;
                            Bitmap tImage = new Bitmap(imgUrl);
                            //imageToByte(tImage, tImage.RawFormat);
                            retByte = imageToByte(tImage, tImage.RawFormat);
                            //byte[] bStream = ImageToByte(tImage);    
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
                        json = content.Replace("PUT/orders/json=", "");
                        Order result = JsonConvert.DeserializeObject<Order>(json);
                        service.UpdateOrder(result);
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
    }
}
