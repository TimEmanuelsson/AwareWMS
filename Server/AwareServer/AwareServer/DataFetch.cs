﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using MagentoConnection;
using Repository.Model;
using AwareClassLibrary;

using System.Diagnostics;
using System.Threading;

namespace AwareServer
{
    class DataFetch
    {
        private MagentoHelper magentoHelper;
        private Service service;
        private State state;
        private System.Timers.Timer timer;

        public DataFetch()
        {
            magentoHelper = new MagentoHelper();
            service = new Service();
            state = State.Free;
        }

        public void Initialize()
        {
            try
            {
                CompleteFetchTimer(new TimeSpan(23,59,59));
                timer = new System.Timers.Timer(5000);
                timer.Elapsed += new ElapsedEventHandler(UpdateFetch);
                timer.Enabled = true;
            }
            catch (Exception e)
            {
                ExceptionLog log = new ExceptionLog(0, e.GetType().ToString(), e.Message, e.Source, e.StackTrace);
                service.InsertException(log);
            }
        }

        /*
         * UpdateFetch will only update existing products, not insert new ones.
         * It's many times quicker than CompleteFetch. Run it regularly to keep stock values consistent between platforms.
         * */
        public void UpdateFetch(Object source, ElapsedEventArgs eventArgs)
        {
            if (state == State.Free)
            {
                try
                {
                    Debug.WriteLine("Hämtar produkter. Tid: {0}", eventArgs.SignalTime);
                    state = State.Busy;
                    List<Product> magentoProducts = magentoHelper.GetAllProductsWithInventory();
                    List<Product> localProducts = service.GetProducts() as List<Product>;

                    InventoryCheck(magentoProducts, localProducts);


                    List<Product> newProducts = new List<Product>();
                    // Merge the products from magento with the ones that we store locally.
                    foreach (Product product in magentoProducts)
                    {
                        if (localProducts.Exists(p => p.SKU == product.SKU))
                        {
                            magentoProducts.First(p => p.SKU == product.SKU).Merge(localProducts.First(p => p.SKU == product.SKU));
                        }

                        else
                        {
                            newProducts.Add(magentoProducts.First(p => p.SKU == product.SKU));
                        }
                    }

                    foreach (Product newProduct in newProducts)
                    {
                        magentoProducts.Remove(newProduct);
                    }

                    service.InsertAndUpdateProductList(magentoProducts);
                    state = State.Free;
                }
                catch (Exception e)
                {
                    state = State.Free;
                    ExceptionLog log = new ExceptionLog(0, e.GetType().ToString(), e.Message, e.Source, e.StackTrace);
                    service.InsertException(log);
                }
            }
        }

        /*
         * CompleteFetch grabs all products from the eCommerce system. Its runtime depends on the number of products and the response time of the system.
         * Run it once to get everything, and then run it either manually every time changes are made or at greater intervals (once per day/week).
         * For updating existing products, please refer to UpdateFetch.
         * */
        public void CompleteFetch()
        {
            if (state == State.Free)
            {
                try
                {
                    state = State.Busy;
                    List<Product> magentoProducts = magentoHelper.GetAllDetailedProductsWithInventory();
                    List<Product> productsWithImages = magentoHelper.DownloadAllProductImages();
                    List<Product> localProducts = service.GetProducts() as List<Product>;

                    InventoryCheck(magentoProducts, localProducts);

                    // Merge the products from magento with the ones that we store locally.
                    foreach (Product product in magentoProducts)
                    {
                        if (localProducts.Exists(p => p.SKU == product.SKU))
                        {
                            magentoProducts.First(p => p.SKU == product.SKU).Merge(localProducts.First(p => p.SKU == product.SKU));
                        }
                    }

                    foreach (Product product in productsWithImages)
                    {
                        if (localProducts.Exists(p => p.ProductId == product.ProductId))
                        {
                            Debug.WriteLine("Image matched to product.");
                            magentoProducts.First(p => p.ProductId == product.ProductId).ImageLocation = product.ImageLocation;
                        }
                        else
                        {
                            Debug.WriteLine("Image with id {0} did not match any products.", product.ProductId.ToString());
                        }
                    }

                    service.InsertAndUpdateProductList(magentoProducts);
                    state = State.Free;
                }
                catch (Exception e)
                {
                    state = State.Free;
                    ExceptionLog log = new ExceptionLog(0, e.GetType().ToString(), e.Message, e.Source, e.StackTrace);
                    service.InsertException(log);
                }
            }
        }

        /*
         * InventoryCheck compares the stock values of the locally stored products with their counterparts in the eCommerce system.
         * If the values differ, the local values are assumed to be correct, and the eCommerce system is updated with the new values.
         * */
        public void InventoryCheck(List<Product> magentoProducts, List<Product> localProducts)
        {
            try
            {
                foreach (Product magentoProduct in magentoProducts)
                {
                    if (localProducts.Exists(p => p.SKU == magentoProduct.SKU))
                    {
                        Product localProduct = localProducts.First(p => p.SKU == magentoProduct.SKU);
                        if (localProduct.Quantity != magentoProduct.Quantity)
                        {
                            magentoProduct.Quantity = localProduct.Quantity;
                            magentoHelper.UpdateProductInventory(magentoProduct);
                        }
                    }
                }
            }

            catch (Exception e)
            {
                ExceptionLog log = new ExceptionLog(0, e.GetType().ToString(), e.Message, e.Source, e.StackTrace);
                service.InsertException(log);
            }
        }

        private void CompleteFetchTimer(TimeSpan alertTime)
        {
            DateTime current = DateTime.Now;
            TimeSpan timeToGo = alertTime - current.TimeOfDay;
            if (timeToGo < TimeSpan.Zero)
            {
                return;//time already passed
            }
            System.Threading.Timer timer = new System.Threading.Timer(x =>
            {
                this.CompleteFetch();
            }, null, timeToGo, Timeout.InfiniteTimeSpan);
        }
    }

    enum State
    {
        Free,
        Busy
    }
}
