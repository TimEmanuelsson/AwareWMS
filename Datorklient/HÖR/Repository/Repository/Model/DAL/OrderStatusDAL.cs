﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Repository.Model.DAL
{
    class OrderStatusDAL
    {
        #region Fields

        private static string _connectionString;

        #endregion

        #region Constructor

        static OrderStatusDAL() 
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

        public OrderStatus GetOrderStatusById(int OrderStatusId)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Create SqlCommand-objekt that execute stored procedure.
                    SqlCommand cmd = new SqlCommand("dbo.usp_GetOrderStatus", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", OrderStatusId);

                    //Open database connection.
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var StatusIndex = reader.GetOrdinal("Status");

                            return new OrderStatus
                            (
                                OrderStatusId,
                                reader.GetString(StatusIndex)
                            );
                        }
                    }
                    return null;
                }
                catch
                {
                    //Throw Exception.
                    throw new ApplicationException("An error occurred while trying to retrieve the orderstatus.");
                }
            }
        }

        #endregion
    }
}
