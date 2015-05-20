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
            connection.RefreshConnection();

            List<catalogProductEntity> magentoProducts = connection.GetAllProducts();

            List<catalogProductReturnEntity> detailedProducts = connection.GetAllDetailedProducts(magentoProducts);

            List<catalogInventoryStockItemEntity> inventory = connection.GetAllInventory(magentoProducts);

            List<Product> products = ProductAdapter.AdaptToProducts(detailedProducts, inventory);

            return products;
        }

        public Product DownloadProductImage(Product product)
        {
            connection.RefreshConnection();

            WebClient webClient = new WebClient();
            string rootPath = Environment.CurrentDirectory;
            string imageFolderPath = String.Format("{0}{1}", rootPath, "\\Product Images\\"); // Make a complete filepath.

            catalogProductImageEntity[] productImages = connection.GetProductImages(product.ProductId.ToString());

            foreach (catalogProductImageEntity image in productImages)
            {
                // Check if the current image is the base image for the product, otherwise we skip it.
                bool isBaseImage = false;
                foreach (string type in image.types)
                {
                    if (type == "image")    // Having the type "image" means this is the base image for the product.
                    {
                        isBaseImage = true;
                    }
                }

                if (isBaseImage)
                {
                    string[] pathParts = image.file.Split('.'); // Make the filepath into an array and remove forward slashes.

                    string fileType = pathParts.Last(); // Get the filetype (.jpg, .gif, .png, etc.)

                    Directory.CreateDirectory(imageFolderPath); // Create the product image directory if it doesn't already exist.

                    int increment = 0;

                    string filePath = String.Format("{0}{1}_{2}{3}", imageFolderPath, product.ProductId.ToString(), increment.ToString(), fileType);
                    if (!File.Exists(filePath))
                    {
                        try
                        {
                            webClient.DownloadFile(image.url, filePath);
                            product.ImageLocation = filePath;
                        }
                        catch (WebException)
                        {
                            continue;
                        }
                    }
                }
            }
            return product;
        }

        public List<Product> DownloadAllProductImages()
        {
            connection.RefreshConnection();

            List<Product> productsAndImagePaths = new List<Product>();

            List<catalogProductEntity> magentoProducts = connection.GetAllProducts();

            WebClient webClient = new WebClient();
            string rootPath = Environment.CurrentDirectory;
            string imageFolderPath = String.Format("{0}{1}", rootPath, "\\Product Images\\"); // Make a complete filepath.


            foreach (catalogProductEntity product in magentoProducts)
            {
                catalogProductImageEntity[] productImages = connection.GetProductImages(product.product_id);
                int productId = 0;
                int.TryParse(product.product_id, out productId);

                foreach (catalogProductImageEntity image in productImages)
                {
                    // Check if the current image is the base image for the product, otherwise we skip it.
                    bool isBaseImage = false;
                    foreach (string type in image.types)
                    {
                        if (type == "image")    // Having the type "image" means this is the base image for the product.
                        {
                            isBaseImage = true;
                        }
                    }

                    if (isBaseImage)
                    {
                        string[] pathParts = image.file.Split('.'); // Make the filepath into an array and remove forward slashes.

                        string fileType = pathParts.Last(); // Get the filetype (.jpg, .gif, .png, etc.)

                        Directory.CreateDirectory(imageFolderPath); // Create the product image directory if it doesn't already exist.

                        int increment = 0;

                        string filePath = String.Format("{0}{1}_{2}.{3}", imageFolderPath, product.product_id, increment.ToString(), fileType);
                        if (!File.Exists(filePath))
                        {
                            try
                            {
                                webClient.DownloadFile(image.url, filePath);
                                productsAndImagePaths.Add(new Product(productId, null, null, 0, 0.0m, null, null, filePath));
                            }
                            catch (WebException)
                            {
                                continue;
                            }
                        }
                    }
                }
            }
            return productsAndImagePaths;
        }

        public void UpdateProductInventory(Product product)
        {
            connection.RefreshConnection();

            catalogProductReturnEntity productEntity = connection.GetProductBySKU(product.SKU);

            catalogInventoryStockItemUpdateEntity inventoryUpdate = new catalogInventoryStockItemUpdateEntity();
            inventoryUpdate.qty = product.Quantity.ToString();

            connection.UpdateProductInventory(productEntity.product_id, inventoryUpdate);
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
