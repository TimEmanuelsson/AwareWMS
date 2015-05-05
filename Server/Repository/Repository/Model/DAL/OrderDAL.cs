using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using AwareClassLibrary;

namespace Repository.Model.DAL
{
    class OrderDAL
    {
        #region Fields

        private static string _connectionString;

        #endregion

        #region Constructor

        static OrderDAL() 
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

        public Order GetOrderById(int OrderId)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Create SqlCommand-objekt that execute stored procedure.
                    SqlCommand cmd = new SqlCommand("dbo.usp_GetOrder", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", OrderId);

                    //Open database connection
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var OrderIdIndex = reader.GetOrdinal("OrderId");
                            var CustomerIdIndex = reader.GetOrdinal("CustomerId");
                            var OrderStatusIndex = reader.GetOrdinal("OrderStatus");
                            var DateIndex = reader.GetOrdinal("Date");
                            var LastUpdateIndex = reader.GetOrdinal("LastUpdate");
                            var PaymentStatusIndex = reader.GetOrdinal("PaymentStatus");

                            return new Order
                            (
                                OrderId,
                                reader.GetInt32(OrderIdIndex),
                                reader.GetInt32(CustomerIdIndex),
                                reader.GetInt32(OrderStatusIndex),
                                reader.GetDateTime(DateIndex),
                                reader.GetDateTime(LastUpdateIndex),
                                reader.GetInt32(PaymentStatusIndex)
                            );
                        }
                    }
                    return null;
                }
                catch
                {
                    //Throw Exception.
                    throw new ApplicationException("An error occurred while trying to retrieve the order.");
                }
            }
        }

        public IEnumerable<Order> GetOrders()
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    //Create list-objekt.
                    var Orders = new List<Order>(1000);

                    // Create SqlCommand-objekt that execute stored procedure.
                    var cmd = new SqlCommand("dbo.usp_GetOrder", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Open database connection
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var IdIndex = reader.GetOrdinal("Id");
                        var OrderIdIndex = reader.GetOrdinal("OrderId");
                        var CustomerIdIndex = reader.GetOrdinal("CustomerId");
                        var OrderStatusIndex = reader.GetOrdinal("OrderStatus");
                        var DateIndex = reader.GetOrdinal("Date");
                        var LastUpdateIndex = reader.GetOrdinal("LastUpdate");
                        var PaymentStatusIndex = reader.GetOrdinal("PaymentStatus");

                        while (reader.Read())
                        {
                            Orders.Add(new Order
                            (
                                reader.GetInt32(IdIndex),
                                reader.GetInt32(OrderIdIndex),
                                reader.GetInt32(CustomerIdIndex),
                                reader.GetInt32(OrderStatusIndex),
                                reader.GetDateTime(DateIndex),
                                reader.GetDateTime(LastUpdateIndex),
                                reader.GetInt32(PaymentStatusIndex)
                            ));
                        }
                    }

                    //Trim product list.
                    Orders.TrimExcess();

                    return Orders;
                }
                catch
                {
                    //Throw Exception.
                    throw new ApplicationException("An error occurred while trying to retrieve the orders.");
                }
            }
        }

        public void UpdateOrder(Order order)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Create SqlCommand-objekt that execute stored procedure.
                    SqlCommand cmd = new SqlCommand("dbo.usp_UpdateOrder", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = order.Id;
                  //  cmd.Parameters.Add("@OrderId", SqlDbType.Int, 4).Value = order.OrderId;
                    cmd.Parameters.Add("@CustomerId", SqlDbType.Int, 4).Value = order.CustomerId;
                    cmd.Parameters.Add("@OrderStatus", SqlDbType.Int, 4).Value = order.OrderStatus;
                    cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = order.Date;
                    cmd.Parameters.Add("@LastUpdate", SqlDbType.DateTime).Value = order.LastUpdate;
                    cmd.Parameters.Add("@PaymentStatus", SqlDbType.Int, 4).Value = order.PaymentStatus;

                    //Open database connection.
                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    //Throw Exception
                    throw new ApplicationException("An error occurred while trying to update the order.");
                }
            }
        }

        public void InsertAndUpdateOrder(Order order)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Create SqlCommand-objekt that execute stored procedure.
                    SqlCommand cmd = new SqlCommand("dbo.usp_InsertAndUpdateOrder", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = order.Id;
                    cmd.Parameters.Add("@OrderId", SqlDbType.Int, 4).Value = order.OrderId;
                    cmd.Parameters.Add("@CustomerId", SqlDbType.Int, 4).Value = order.CustomerId;
                    cmd.Parameters.Add("@OrderStatus", SqlDbType.Int, 4).Value = order.OrderStatus;
                    cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = order.Date;
                    cmd.Parameters.Add("@LastUpdate", SqlDbType.DateTime).Value = order.LastUpdate;
                    cmd.Parameters.Add("@PaymentStatus", SqlDbType.Int, 4).Value = order.PaymentStatus;

                    //Open database connection.
                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    //Throw Exception
                    throw new ApplicationException("An error occurred while trying to update or insert the order.");
                }
            }
        }

        #endregion      
    }
}
