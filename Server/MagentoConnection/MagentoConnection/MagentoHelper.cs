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
using System.Diagnostics;

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

        public void DownloadAllProductImages()
        {
            List<catalogProductEntity> magentoProducts = connection.GetAllProducts();

            List<catalogProductImageEntity[]> productImages = connection.GetAllImages(magentoProducts);

            WebClient webClient = new WebClient();
            string rootPath = Environment.CurrentDirectory;
            // Denna loop är bara för testning och kan tas bort.
            foreach (catalogProductImageEntity[] imageArray in productImages)
            {
                foreach (catalogProductImageEntity image in imageArray)
                {
                    
                    // Check if the current image is the base image for the product, otherwise we skip it.
                    bool isBaseImage = false;
                    foreach (string type in image.types)
                    {
                        if (type == "image")
                        {
                            isBaseImage = true;
                        }
                    }

                    if (isBaseImage)
                    {
                        string[] pathParts = image.file.Split('/'); // Make the filepath into an array and remove forward slashes.

                        string filename = pathParts.Last();
                        pathParts = pathParts.Take(pathParts.Count() - 1).ToArray();  // Removes the last element (filename) in the array

                        string path = String.Join("\\", pathParts); // Add backslashes to filepath (windows environment).
                        string imageFolderPath = String.Format("{0}{1}{2}", rootPath, "\\Product Images\\", path); // Make a complete filepath.

                        Directory.CreateDirectory(imageFolderPath); // Create all directories in the path if they don't already exist.

                        string filePath = String.Format("{0}{1}", imageFolderPath, filename);
                        if (!File.Exists(filePath))
                        {
                            try
                            {
                                webClient.DownloadFile(image.url, filePath);
                            }
                            catch (WebException)
                            {
                                continue;
                            }
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
            
        }
    }
}
