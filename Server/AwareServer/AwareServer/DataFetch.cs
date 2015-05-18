using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using MagentoConnection;
using Repository.Model;
using AwareClassLibrary;

using System.Diagnostics;

namespace AwareServer
{
    class DataFetch
    {
        private MagentoHelper magentoHelper;
        private Service service;
        private State state;
        private Timer timer;

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
                timer = new Timer(180000);
                timer.Elapsed += new ElapsedEventHandler(FetchAndInsert);
                timer.Enabled = true;
            }
            catch (Exception e)
            {
                ExceptionLog log = new ExceptionLog(0, e.GetType().ToString(), e.Message, e.Source, e.StackTrace);
                service.InsertException(log);
            }
        }

        public void FetchAndInsert(Object source, ElapsedEventArgs e)
        {
            try
            {
                Debug.WriteLine("Hämtar produkter. Tid: {0}", e.SignalTime);
                state = State.Busy;
                List<Product> products = magentoHelper.GetAllProductsWithInventory();
                List<Product> productsWithImages = magentoHelper.DownloadAllProductImages();

                foreach (Product product in productsWithImages)
                {
                    products.First(p => p.ProductId == product.ProductId).ImageLocation = product.ImageLocation;
                }

                service.InsertAndUpdateProductList(products);
                state = State.Free;
            }
            catch (Exception e)
            {
                ExceptionLog log = new ExceptionLog(0, e.GetType().ToString(), e.Message, e.Source, e.StackTrace);
                service.InsertException(log);
            }
        }
    }

    enum State
    {
        Free,
        Busy
    }
}
