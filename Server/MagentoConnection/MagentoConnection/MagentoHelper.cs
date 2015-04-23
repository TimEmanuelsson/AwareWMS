using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagentoConnection.Magento;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using AwareClassLibrary;
using System.Net;

namespace MagentoConnection
{
    public class MagentoHelper
    {
        private Connection connection;

        public MagentoHelper()
        {
            connection = new Connection();
        }
        public List<Product> GetAllProductsWithInventory()
        {
            List<catalogProductEntity> magentoProducts = connection.GetAllProducts();

            List<catalogProductReturnEntity> detailedProducts = connection.GetAllDetailedProducts(magentoProducts);

            List<catalogInventoryStockItemEntity> inventory = connection.GetAllInventory(magentoProducts);

            List<Product> products = ProductAdapter.AdaptToProducts(detailedProducts, inventory);

            return products;
        }

        public void LeftoverCode()
        {
            /*
            List<salesOrderListEntity> orders = connection.GetAllOrders();

            foreach (salesOrderListEntity order in orders)
            {
                foreach (PropertyDescriptor desc in TypeDescriptor.GetProperties(order))
                {
                    string name = desc.Name;
                    object value = desc.GetValue(order);
                    Console.WriteLine("{0}={1}", name, value);
                }
            }
            */
            List<catalogProductEntity> magentoProducts = connection.GetAllProducts();

            List<catalogProductImageEntity[]> productImages = connection.GetAllImages(magentoProducts);

            WebClient webClient = new WebClient();
            string rootPath = Environment.CurrentDirectory;
            // Denna loop är bara för testning och kan tas bort.
            foreach (catalogProductImageEntity[] imageArray in productImages)
            {
                foreach (catalogProductImageEntity image in imageArray)
                {
                    string[] pathParts = image.file.Split('/');

                    string filename = pathParts.Last();
                    pathParts = pathParts.Take(pathParts.Count() - 1).ToArray();  // Removes the last element (filename) in the array

                    string path = String.Join("\\", pathParts);
                    string imageFolderPath = String.Format("{0}{1}{2}", rootPath, "\\Product Images\\", path);

                    Directory.CreateDirectory(imageFolderPath);

                    string filePath = String.Format("{0}{1}", imageFolderPath, filename);
                    try
                    {
                        webClient.DownloadFile(image.url, filePath);
                    }
                    catch (WebException)
                    {
                        continue;
                    }
                    /*
                    foreach (PropertyDescriptor desc in TypeDescriptor.GetProperties(image))
                    {
                        string name = desc.Name;
                        object value = desc.GetValue(image);
                        Console.WriteLine("{0}={1}", name, value);
                    }
                    */
                }

            }
        }
    }
}
