﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagentoConnection.Magento;
using AwareClassLibrary;
using System.Globalization;
using System.Diagnostics;

namespace MagentoConnection
{
    public class ProductAdapter
    {
        public static List<Product> AdaptToProducts(List<catalogProductReturnEntity> magentoProducts,
            List<catalogInventoryStockItemEntity> inventory)
        {
            List<Product> products = new List<Product>();

            foreach(catalogProductReturnEntity magentoProduct in magentoProducts)
            {
                try {
                    int quantity = new int();
                    int.TryParse(inventory.First(
                        catalogInventoryStockItemEntity => catalogInventoryStockItemEntity.product_id == magentoProduct.product_id).qty,
                        NumberStyles.Float, CultureInfo.InvariantCulture, out quantity);
                    Product product = AdaptToProduct(magentoProduct, quantity);
                    products.Add(product);
                } catch(Exception e) {
                    Debug.WriteLine(e.Message);
                }
                
            }

            return products;
        }

        public static List<Product> AdaptToProducts(List<catalogProductEntity> magentoProducts,
            List<catalogInventoryStockItemEntity> inventory)
        {
            List<Product> products = new List<Product>();

            foreach (catalogProductEntity magentoProduct in magentoProducts)
            {
                int quantity = new int();
                int.TryParse(inventory.First(
                    catalogInventoryStockItemEntity => catalogInventoryStockItemEntity.product_id == magentoProduct.product_id).qty,
                    NumberStyles.Float, CultureInfo.InvariantCulture, out quantity);
                Product product = AdaptToProduct(magentoProduct, quantity);
                products.Add(product);
            }

            return products;
        }

        public static Product AdaptToProduct(catalogProductReturnEntity magentoProduct, int quantity)
        {
            int productId = new int();
            int.TryParse(magentoProduct.product_id, out productId);
            
            decimal weight = new decimal();
            decimal.TryParse(magentoProduct.weight, NumberStyles.Float, CultureInfo.InvariantCulture, out weight);

            Product product = new Product(productId, magentoProduct.name, magentoProduct.sku, quantity, weight, "", "", "");
            return product;
        }

        public static Product AdaptToProduct(catalogProductEntity magentoProduct, int quantity)
        {
            int productId = new int();
            int.TryParse(magentoProduct.product_id, out productId);

            Product product = new Product(productId, magentoProduct.name, magentoProduct.sku, quantity, 0m, "", "", "");
            return product;
        }
    }
}
