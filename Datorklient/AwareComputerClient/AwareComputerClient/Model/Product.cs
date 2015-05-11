
/**
 * Model class for a Product.
 *
 * @author Joakim Nilsson
 */
namespace AwareComputerClient.Model
{
    public class Product
    {
        //private string name;

        //private string sku;
        //private int quantity;
        //private double weight;
        //private string storageSpace;
        //private string barcodeNumber;
        //private string imageLocation;
        //public string Name
        //{
        //    get
        //    {
        //        return name;
        //    }
        //    set
        //    {
        //        name = value;
        //    }
        //}
        //public string SKU
        //{
        //    get
        //    {
        //        return sku;
        //    }
        //    set
        //    {
        //        sku = value;
        //    }
        //}
        //public int Quantity
        //{
        //    get
        //    {
        //        return quantity;
        //    }
        //    set
        //    {
        //        quantity = value;
        //    }
        //}
        //public double Weight
        //{
        //    get
        //    {
        //        return weight;
        //    }
        //    set
        //    {
        //        weight = value;
        //    }
        //}
        //public string StorageSpace
        //{
        //    get
        //    {
        //        return storageSpace;
        //    }
        //    set
        //    {
        //        storageSpace = value;
        //    }
        //}
        //public string BarcodeNumber
        //{
        //    get
        //    {
        //        return barcodeNumber;
        //    }
        //    set
        //    {
        //        barcodeNumber = value;
        //    }
        //}
        //public string ImageLocation
        //{
        //    get
        //    {
        //        return imageLocation;
        //    }
        //    set
        //    {
        //        imageLocation = value;
        //    }
        //}


        public int ProductId { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public double Weight { get; set; }
        public string StorageSpace { get; set; }
        public string BarcodeNumber { get; set; }
        public string ImageLocation { get; set; }
        public string LastInventory { get; set; }
    }
}

    