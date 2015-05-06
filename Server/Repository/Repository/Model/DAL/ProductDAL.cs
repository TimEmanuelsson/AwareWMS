using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using AwareClassLibrary;

namespace Repository.Model.DAL
{
    class ProductDAL
    {
        #region Fields

        private static string _connectionString;

        #endregion

        #region Constructor

        static ProductDAL() 
        {
            //Get connectionstring
            _connectionString = Repository.Properties.Settings.Default.temp;
        }

        #endregion

        #region Hjälpmetoder

        //Create connectionstring
        private static SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        #endregion

        #region CRUD Functions

        public Product GetProductById(int ProductId)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Create SqlCommand-objekt that execute stored procedure.
                    SqlCommand cmd = new SqlCommand("dbo.usp_GetProduct", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", ProductId);

                    //Open database connection.
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var NameIndex = reader.GetOrdinal("Name");
                            var SKUIndex = reader.GetOrdinal("SKU");
                            var QuantityIndex = reader.GetOrdinal("Quantity");
                            var WeightIndex = reader.GetOrdinal("Weight");
                            var SpaceIndex = reader.GetOrdinal("StorageSpace");
                            var BarcodeNumberIndex = reader.GetOrdinal("BarcodeNumber");
                            var ImageIndex = reader.GetOrdinal("ImageLocation");

                            return new Product
                            (
                                ProductId,
                                reader.GetString(NameIndex),
                                reader.GetString(SKUIndex),
                                reader.GetInt32(QuantityIndex),
                                reader.GetDecimal(WeightIndex),
                                reader.GetString(SpaceIndex),
                                reader.GetString(BarcodeNumberIndex),
                                reader.GetString(ImageIndex)
                            );
                        }
                    }
                    return null;
                }
                catch
                {
                    //Throw Exception.
                    throw new ApplicationException("An error occurred while trying to retrieve the product.");
                }
            }
        }

        public Product GetProductByBarcodeNumber(int BarcodeNumber)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Create SqlCommand-objekt that execute stored procedure.
                    SqlCommand cmd = new SqlCommand("dbo.usp_GetProductByBarcodeNumber", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BarcodeNumber", BarcodeNumber);

                    //Open database connection.
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var ProductIdIndex = reader.GetOrdinal("Id");
                            var NameIndex = reader.GetOrdinal("Name");
                            var SKUIndex = reader.GetOrdinal("SKU");
                            var QuantityIndex = reader.GetOrdinal("Quantity");
                            var WeightIndex = reader.GetOrdinal("Weight");
                            var SpaceIndex = reader.GetOrdinal("StorageSpace");
                            var BarcodeNumberIndex = reader.GetOrdinal("BarcodeNumber");
                            var ImageIndex = reader.GetOrdinal("ImageLocation");

                            return new Product(
                                reader.GetInt32(ProductIdIndex),
                                reader.GetString(NameIndex),
                                reader.GetString(SKUIndex),
                                reader.GetInt32(QuantityIndex),
                                reader.GetDecimal(WeightIndex),
                                reader.GetString(SpaceIndex),
                                reader.GetString(BarcodeNumberIndex),
                                reader.GetString(ImageIndex)
                            );
                            }
                        }
                    return null;
                }
                catch
                {
                    //Throw Exception.
                    throw new ApplicationException("An error occurred while trying to retrieve the product.");
                }
            }
        }

        public Product GetProductBySKU(int sku)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Create SqlCommand-objekt that execute stored procedure.
                    SqlCommand cmd = new SqlCommand("dbo.usp_GetProductBySKU", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SKU", sku);

                    //Open database connection.
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var ProductIdIndex = reader.GetOrdinal("Id");
                            var NameIndex = reader.GetOrdinal("Name");
                            var SKUIndex = reader.GetOrdinal("SKU");
                            var QuantityIndex = reader.GetOrdinal("Quantity");
                            var WeightIndex = reader.GetOrdinal("Weight");
                            var SpaceIndex = reader.GetOrdinal("StorageSpace");
                            var BarcodeNumberIndex = reader.GetOrdinal("BarcodeNumber");
                            var ImageIndex = reader.GetOrdinal("ImageLocation");

                            return new Product(
                                reader.GetInt32(ProductIdIndex),
                                reader.GetString(NameIndex),
                                reader.GetString(SKUIndex),
                                reader.GetInt32(QuantityIndex),
                                reader.GetDecimal(WeightIndex),
                                reader.GetString(SpaceIndex),
                                reader.GetString(BarcodeNumberIndex),
                                reader.GetString(ImageIndex)
                            );
                        }
                    }
                    return null;
                }
                catch
                {
                    //Throw Exception.
                    throw new ApplicationException("An error occurred while trying to retrieve the product.");
                }
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    // Create List-objekt.
                    var Products = new List<Product>(1000);

                    // Create SqlCommand-objekt that execute stored procedure.
                    var cmd = new SqlCommand("dbo.usp_GetProduct", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Open database connection.
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var ProductIdIndex = reader.GetOrdinal("Id");
                        var NameIndex = reader.GetOrdinal("Name");
                        var SKUIndex = reader.GetOrdinal("SKU");
                        var QuantityIndex = reader.GetOrdinal("Quantity");
                        var WeightIndex = reader.GetOrdinal("Weight");
                        var SpaceIndex = reader.GetOrdinal("StorageSpace");
                        var BarcodeNumberIndex = reader.GetOrdinal("BarcodeNumber");
                        var ImageIndex = reader.GetOrdinal("ImageLocation");
                        var LastInventoryIndex = reader.GetOrdinal("LastInventory");

                        while (reader.Read())
                        {
                            if (reader.GetDateTime(LastInventoryIndex) != DateTime.ParseExact("1900-01-01 00:00:00,000", "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture))
                            {

                                Products.Add(new Product
                                (
                                    reader.GetInt32(ProductIdIndex),
                                    reader.GetString(NameIndex),
                                    reader.GetString(SKUIndex),
                                    reader.GetInt32(QuantityIndex),
                                    reader.GetDecimal(WeightIndex),
                                    reader.GetString(SpaceIndex),
                                    reader.GetString(BarcodeNumberIndex),
                                    reader.GetString(ImageIndex),
                                    reader.GetDateTime(LastInventoryIndex)
                                ));
                            }
                            else
                            {
                                Products.Add(new Product
                                (
                                    reader.GetInt32(ProductIdIndex),
                                    reader.GetString(NameIndex),
                                    reader.GetString(SKUIndex),
                                    reader.GetInt32(QuantityIndex),
                                    reader.GetDecimal(WeightIndex),
                                    reader.GetString(SpaceIndex),
                                    reader.GetString(BarcodeNumberIndex),
                                    reader.GetString(ImageIndex)
                                ));
                            }
                        }
                    }

                    //Trim product list.
                    Products.TrimExcess();

                    return Products;
                }
                catch
                {
                    //Throw Exception.
                    throw new ApplicationException("An error occurred while trying to retrieve the products.");
                }
            }
        }

        public void ProductInventory(Product product)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Create SqlCommand-objekt that execute stored procedure.
                    SqlCommand cmd = new SqlCommand("dbo.usp_ProductInventory", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = product.ProductId;
                    cmd.Parameters.Add("@Quantity", SqlDbType.Int, 6).Value = product.Quantity;
                    cmd.Parameters.Add("@LastInventory", SqlDbType.DateTime).Value = product.LastInventory;

                    //Open database connection.
                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    //Throw Exception
                    throw new ApplicationException("An error occurred while trying to update the product.");
                }
            }
        }

        public void UpdateProduct(Product product)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Create SqlCommand-objekt that execute stored procedure.
                    SqlCommand cmd = new SqlCommand("dbo.usp_UpdateProduct", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = product.ProductId;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = product.Name;
                    cmd.Parameters.Add("@SKU", SqlDbType.VarChar, 50).Value = product.SKU;
                    cmd.Parameters.Add("@Quantity", SqlDbType.Int, 6).Value = product.Quantity;
                    cmd.Parameters.Add("@Weight", SqlDbType.Decimal, 10).Value = product.Weight;
                    cmd.Parameters.Add("@Space", SqlDbType.VarChar, 10).Value = product.StorageSpace;
                    cmd.Parameters.Add("@BarcodeNumber", SqlDbType.VarChar, 30).Value = product.BarcodeNumber;
                    cmd.Parameters.Add("@Image", SqlDbType.VarChar, 128).Value = product.ImageLocation;
          //          cmd.Parameters.Add("@LastInventory", SqlDbType.DateTime).Value = product.LastInventory;

                    //Open database connection.
                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    //Throw Exception
                    throw new ApplicationException("An error occurred while trying to update the product.");
                }
            }
        }

        public void InsertAndUpdateProduct(Product product)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Create SqlCommand-objekt that execute stored procedure.
                    SqlCommand cmd = new SqlCommand("dbo.usp_InsertAndUpdateProduct", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = product.ProductId;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = product.Name;
                    cmd.Parameters.Add("@SKU", SqlDbType.VarChar, 50).Value = product.SKU;
                    cmd.Parameters.Add("@Quantity", SqlDbType.Int, 6).Value = product.Quantity;
                    cmd.Parameters.Add("@Weight", SqlDbType.Decimal, 10).Value = product.Weight;
                    cmd.Parameters.Add("@Space", SqlDbType.VarChar, 10).Value = product.StorageSpace;
                    cmd.Parameters.Add("@BarcodeNumber", SqlDbType.VarChar, 30).Value = product.BarcodeNumber;
                    cmd.Parameters.Add("@Image", SqlDbType.VarChar, 128).Value = product.ImageLocation;

                    //Open database connection.
                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    //Throw Exception
                    throw new ApplicationException("An error occurred while trying to update or insert the product.");
                }
            }
        }

        #endregion    
    }
}
