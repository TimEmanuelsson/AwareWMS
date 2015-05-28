using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagentoConnection.Magento;
using System.Diagnostics;
using System.Threading;

namespace MagentoConnection
{
    class Connection
    {
        private Mage_Api_Model_Server_V2_HandlerPortTypeClient client;
        private string session;

        public Connection()
        {
            // The url to the Magento source is set through the service reference.
            // The url is located in App.config
            // To change the url at runtime, look at this: http://stackoverflow.com/questions/2008800/changing-app-config-at-runtime

            this.client = new Mage_Api_Model_Server_V2_HandlerPortTypeClient();
            this.session = this.client.login(Settings.Default.Username, Settings.Default.Password);
        }

        public void RefreshConnection()
        {
            this.session = this.client.login(Settings.Default.Username, Settings.Default.Password);
        }

        #region Products

        public List<catalogProductEntity> GetAllProducts()
        {
            catalogProductEntity[] productsArray = this.client.catalogProductList(this.session, null, null);
            List<catalogProductEntity> products = productsArray.ToList();

            return products;
        }

        public List<catalogProductReturnEntity> GetAllDetailedProducts(List<catalogProductEntity> products)
        {
            List<catalogProductReturnEntity> detailedProducts = GetDetailedProducts(products).Result;
            
            
            return detailedProducts;
        }

        public async Task<List<catalogProductReturnEntity>> GetDetailedProducts(List<catalogProductEntity> products)
        {
            List<catalogProductReturnEntity> detailedProducts = new List<catalogProductReturnEntity>();
            List<Task<catalogProductReturnEntity>> tasks = new List<Task<catalogProductReturnEntity>>();
            SemaphoreSlim maxThread = new SemaphoreSlim(20);

            foreach (catalogProductEntity product in products)
            {
                maxThread.Wait();
                Task.Factory.StartNew(() =>
                {
                    Debug.WriteLine("Fetching details for product {0}", product.product_id);
                    catalogProductReturnEntity detailedProduct = GetProductById(product.product_id).Result;
                    detailedProducts.Add(detailedProduct);
                }
                    , TaskCreationOptions.LongRunning)
                .ContinueWith((task) => maxThread.Release());
            }

            return detailedProducts;
        }

        public async Task<catalogProductReturnEntity> GetProductById(string productId)
        {
            try
            {
                Task<catalogProductReturnEntity> detailedProductTask = this.client.catalogProductInfoAsync(session, productId, null, null, null);
                catalogProductReturnEntity detailedProduct = await detailedProductTask;
                return detailedProduct;
            }
            catch (Exception e)
            {
                if (e is AggregateException || e is TimeoutException)
                {
                    return GetProductById(productId).Result; // Really crappy error handling here, but if we time out we just call the function again. Very shitty. Sorry.
                }
                throw;
            }
        }

        public catalogProductReturnEntity GetProductBySKU(string sku)
        {
            catalogProductReturnEntity detailedProduct = this.client.catalogProductInfo(session, sku, null, null, "SKU");
            return detailedProduct;
        }

        #endregion

        #region Inventory

        public List<catalogInventoryStockItemEntity> GetAllInventory(List<catalogProductEntity> products)
        {
            string[] productsId = new string[products.Count];

            for (int i = 0; i < products.Count; i++)
            {
                productsId[i] = products.ElementAt(i).product_id;
            }

            catalogInventoryStockItemEntity[] inventoryArray = this.client.catalogInventoryStockItemList(session, productsId);
            List<catalogInventoryStockItemEntity> inventory = inventoryArray.ToList();

            return inventory;
        }

        public void UpdateProductInventory(string productId, catalogInventoryStockItemUpdateEntity inventoryUpdateEntity)
        {
            this.client.catalogInventoryStockItemUpdate(this.session, productId, inventoryUpdateEntity);
        }

        #endregion

        #region ProductImages

        public List<catalogProductImageEntity[]> GetAllImages(List<catalogProductEntity> products)
        {
            List<catalogProductImageEntity[]> productImages = new List<catalogProductImageEntity[]>();

            for (int i = 0; i < products.Count; i++)
            {
                catalogProductImageEntity[] images = GetProductImages(products.ElementAt(i).product_id);
                productImages.Add(images);
            }

            return productImages;
        }

        public catalogProductImageEntity[] GetProductImages(string productId)
        {
            catalogProductImageEntity[] images = this.client.catalogProductAttributeMediaList(this.session, productId, null, null);
            return images;
        }

        #endregion

        #region Orders

        public List<salesOrderListEntity> GetAllOrders()
        {
            salesOrderListEntity[] orders = this.client.salesOrderList(this.session, null);
            return orders.ToList();
        }

        #endregion

        #region Customers



        #endregion
    }
}
