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
                            var EANIndex = reader.GetOrdinal("EAN");
                            var ImageIndex = reader.GetOrdinal("ImageLocation");
                            var LastInventoryIndex = reader.GetOrdinal("LastInventory");

                            return new Product
                            (
                                ProductId,
                                reader.GetString(NameIndex),
                                reader.GetString(SKUIndex),
                                reader.GetInt32(QuantityIndex),
                                reader.GetDecimal(WeightIndex),
                                reader.GetString(SpaceIndex),
                                reader.GetString(EANIndex),
                                reader.GetString(ImageIndex),
                                reader.GetDateTime(LastInventoryIndex)
                            );
                        }
                        else
                        {
                            throw new NullReferenceException("There is no product with this ID.");
                        }
                    }
                }
                catch (NullReferenceException e)
                {
                    Service service = new Service();
                    ExceptionLog log = new ExceptionLog(0, e.GetType().ToString(), e.Message, e.Source, e.StackTrace);
                    service.InsertException(log);
                    return null;
                }

                catch
                {
                    //Throw Exception.
                    throw new ApplicationException("An error occurred while trying to retrieve the product.");
                }
            }
        }

        public Product GetProductByEAN(int ean)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Create SqlCommand-objekt that execute stored procedure.
                    SqlCommand cmd = new SqlCommand("dbo.usp_GetProductByEAN", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Ean", ean);

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
                            var EANIndex = reader.GetOrdinal("EAN");
                            var ImageIndex = reader.GetOrdinal("ImageLocation");
                            var LastInventoryIndex = reader.GetOrdinal("LastInventory");

                            return new Product(
                                reader.GetInt32(ProductIdIndex),
                                reader.GetString(NameIndex),
                                reader.GetString(SKUIndex),
                                reader.GetInt32(QuantityIndex),
                                reader.GetDecimal(WeightIndex),
                                reader.GetString(SpaceIndex),
                                reader.GetString(EANIndex),
                                reader.GetString(ImageIndex),
                                reader.GetDateTime(LastInventoryIndex)
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
                            var EANIndex = reader.GetOrdinal("EAN");
                            var ImageIndex = reader.GetOrdinal("ImageLocation");
                            var LastInventoryIndex = reader.GetOrdinal("LastInventory");

                            return new Product(
                                reader.GetInt32(ProductIdIndex),
                                reader.GetString(NameIndex),
                                reader.GetString(SKUIndex),
                                reader.GetInt32(QuantityIndex),
                                reader.GetDecimal(WeightIndex),
                                reader.GetString(SpaceIndex),
                                reader.GetString(EANIndex),
                                reader.GetString(ImageIndex),
                                reader.GetDateTime(LastInventoryIndex)

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

        public int GetProductCount()
        {
            IEnumerable<Product> products = GetProducts();
            return products.Count();
        }


        public int CheckIfProductBusy(int productId)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    IEnumerable<Product> products = new List<Product>(1000);
                    // Create SqlCommand-objekt that execute stored procedure.
                    SqlCommand cmd = new SqlCommand("dbo.usp_CheckIfProductBusy", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ProductId", productId);

                    //Open database connection.
                    conn.Open();
                    //int status = (int)cmd.ExecuteScalar();
                    //return status;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
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
                        var EANIndex = reader.GetOrdinal("EAN");
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
                                    reader.GetString(EANIndex),
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
                                    reader.GetString(EANIndex),
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
                    cmd.Parameters.Add("@LastInventory", SqlDbType.DateTime).Value = DateTime.Now;

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
                    cmd.Parameters.Add("@EAN", SqlDbType.VarChar, 30).Value = product.EAN;
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
                    cmd.Parameters.Add("@EAN", SqlDbType.VarChar, 30).Value = product.EAN;
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
