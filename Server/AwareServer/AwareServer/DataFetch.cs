using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagentoConnection;
using Repository.Model;
using AwareClassLibrary;

namespace AwareServer
{
    class DataFetch
    {
        private MagentoHelper magentoHelper;
        private Service service;

        public DataFetch()
        {
            magentoHelper = new MagentoHelper();
            service = new Service();
        }

        public void FetchAndInsert()
        {
            List<Product> products = magentoHelper.GetAllProductsWithInventory();

            foreach (Product product in products)
            {
                products.First(p => p.ProductId == product.ProductId).ImageLocation = magentoHelper.DownloadProductImage(product).ImageLocation;
            }

            service.InsertAndUpdateProductList(products);
        }
    }
}
