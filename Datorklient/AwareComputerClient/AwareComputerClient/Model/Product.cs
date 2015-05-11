
/**
 * Model class for a Product.
 *
 * @author Joakim Nilsson
 */
using System.ComponentModel;
namespace AwareComputerClient.Model
{
    public class Product
    {
        //Prevents generation of column
        [Browsable(false)]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public double Weight { get; set; }
        public string StorageSpace { get; set; }
        public string BarcodeNumber { get; set; }
        public string ImageLocation { get; set; }
        [Browsable(false)]
       public int LastInventory { get; set; }
    }
}

    