using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Repository.Model.DAL
{
    class OrderRowDAL
    {
        #region Fields

        private static string _connectionString;

        #endregion

        #region Constructor

        static OrderRowDAL() 
        {
            //Get connectionstring
            _connectionString = Repository.Properties.Settings.Default.AwareConnectionString;
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

        //SKICKA IN ORDERID ISTÄLLET FÖR ORDERROWID!!!!
        public OrderRow GetOrderRowById(int OrderRowId)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Create SqlCommand-objekt that execute stored procedure.
                    SqlCommand cmd = new SqlCommand("dbo.usp_GetOrderRows", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", OrderRowId);

                    //Open database connection.
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var OrderIdIndex = reader.GetOrdinal("OrderId");
                            var ProductIdIndex = reader.GetOrdinal("ProductId");
                            var AmountIndex = reader.GetOrdinal("Amount");

                            return new OrderRow
                            (
                                OrderRowId,
                                reader.GetInt32(OrderIdIndex),
                                reader.GetInt32(ProductIdIndex),
                                reader.GetInt32(AmountIndex)
                            );
                        }
                    }
                    return null;
                }
                catch
                {
                    //Throw Exception.
                    throw new ApplicationException("An error occurred while trying to retrieve the orderrow.");
                }
            }
        }

        public IEnumerable<OrderRow> GetOrderRow()
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    // Create List-objekt.
                    var OrderRows = new List<OrderRow>(1000);

                    // Create SqlCommand-objekt that execute stored procedure.
                    var cmd = new SqlCommand("dbo.usp_GetOrderRows", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Open database connection
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var OrderRowIdIndex = reader.GetOrdinal("Id");
                        var OrderIdIndex = reader.GetOrdinal("OrderId");
                        var ProductIdIndex = reader.GetOrdinal("ProductId");
                        var AmountIndex = reader.GetOrdinal("Amount");

                        while (reader.Read())
                        {
                            OrderRows.Add(new OrderRow
                            (
                                reader.GetInt32(OrderRowIdIndex),
                                reader.GetInt32(OrderIdIndex),
                                reader.GetInt32(ProductIdIndex),
                                reader.GetInt32(AmountIndex)
                            ));
                        }
                    }

                    //Trim orderRow list.
                    OrderRows.TrimExcess();

                    return OrderRows;
                }
                catch
                {
                    //Throw Exception.
                    throw new ApplicationException("An error occurred while trying to retrieve the orderrows.");
                }
            }
        }

        #endregion
    }
}
