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
        private Connection connection;
        private Service service;

        public DataFetch()
        {
            connection = new Connection();
            service = new Service();
        }

        public void FetchAndInsert()
        {
            List<Product> products = ProductAdapter.AdaptToProducts(connection.GetAllDetailedProducts(connection.GetAllProducts()), connection.GetAllInventory(connection.GetAllProducts()));
            service.InsertAndUpdateProductList(products);
        }
    }
}
