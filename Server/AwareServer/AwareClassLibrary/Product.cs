using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AwareClassLibrary
{
    public class Product
    {
        //Kanske någon input validering

        #region Fields

        public int ProductId { get; set;}
        public string Name { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public decimal Weight { get; set; }
        public string StorageSpace { get; set; }
        public string BarcodeNumber { get; set; }
        public string ImageLocation { get; set; }
        public int LastInventory { get; set; }

        #endregion

        #region Constructor

        public Product(int productId, string name, string sku, int quantity,
    decimal weight, string storageSpace, string barcodeNumber, string imageLocation)
        {
            ProductId = productId;
            Name = name;
            SKU = sku;
            Quantity = quantity;
            Weight = weight;
            StorageSpace = storageSpace;
            BarcodeNumber = barcodeNumber;
            ImageLocation = imageLocation;
        }

        public Product(int productId, string name, string sku, int quantity,
            decimal weight, string storageSpace, string barcodeNumber, string imageLocation, DateTime lastInventory)
        {
            ProductId = productId;
            Name = name;
            SKU = sku;
            Quantity = quantity;
            Weight = weight;
            StorageSpace = storageSpace;
            BarcodeNumber = barcodeNumber;
            ImageLocation = imageLocation;
            LastInventory = (int)Math.Ceiling((DateTime.Now.Date - lastInventory.Date).TotalDays);
        }

                [JsonConstructor]
        public Product(int productId, string name, string sku, int quantity,
            decimal weight, string storageSpace, string barcodeNumber, string imageLocation, int lastInventory)
        {
            ProductId = productId;
            Name = name;
            SKU = sku;
            Quantity = quantity;
            Weight = weight;
            StorageSpace = storageSpace;
            BarcodeNumber = barcodeNumber;
            ImageLocation = imageLocation;
            LastInventory = lastInventory;
        }

        #endregion
    }
}
