using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AwareClassLibrary;

namespace Repository.Model
{
    class ExceptionLogDAL
    {
        #region Fields

        private static string _connectionString;

        #endregion

        #region Constructor

        static ExceptionLogDAL() 
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

        public void InsertException(ExceptionLog exception)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Create SqlCommand-objekt that execute stored procedure.
                    SqlCommand cmd = new SqlCommand("dbo.usp_InsertException", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = exception.Id;
                    cmd.Parameters.Add("@Exception_Type", SqlDbType.VarChar, 30).Value = exception.Exception_Type;
                    cmd.Parameters.Add("@Message", SqlDbType.VarChar, 1024).Value = exception.Message;
                    cmd.Parameters.Add("@Source", SqlDbType.VarChar, 128).Value = exception.Source;
                    cmd.Parameters.Add("@Stacktrace", SqlDbType.VarChar, 2048).Value = exception.Stacktrace;

                    //Open database connection.
                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    //Throw Exception
                    throw new ApplicationException("An error occurred while trying to insert the exception.");
                }
            }
        }

        #endregion
    }
}
