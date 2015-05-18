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
    class CustomerDAL
    {
        #region Fields

        private static string _connectionString;

        #endregion

        #region Constructor

        static CustomerDAL() 
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

        public Customer GetCustomerById(int CustomerId)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Create SqlCommand-objekt that execute stored procedure.
                    SqlCommand cmd = new SqlCommand("dbo.usp_GetCustomer", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", CustomerId);

                    //Open database connection.
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var CustomerIdIndex = reader.GetOrdinal("CustomerId");
                            var FirstNameIndex = reader.GetOrdinal("FirstName");
                            var LastNameIndex = reader.GetOrdinal("LastName");
                            var AddressLine1Index = reader.GetOrdinal("AddressLine1");
                            var AddressLine2Index = reader.GetOrdinal("AddressLine2");
                            var CityIndex = reader.GetOrdinal("City");
                            var RegionIndex = reader.GetOrdinal("Region");
                            var ZipCodeIndex = reader.GetOrdinal("ZipCode");
                            var CountryIndex = reader.GetOrdinal("Country");
                            var PhoneNumberIndex = reader.GetOrdinal("PhoneNumber");
                            var EmailIndex = reader.GetOrdinal("Email");

                            return new Customer
                            (
                                CustomerId,
                                reader.GetInt32(CustomerIdIndex),
                                reader.GetString(FirstNameIndex),
                                reader.GetString(LastNameIndex),
                                reader.GetString(AddressLine1Index),
                                reader.GetString(AddressLine2Index),
                                reader.GetString(CityIndex),
                                reader.GetString(RegionIndex),
                                reader.GetString(ZipCodeIndex),
                                reader.GetString(CountryIndex),
                                reader.GetString(PhoneNumberIndex),
                                reader.GetString(EmailIndex)
                            );
                        }
                        else
                        {
                            throw new NullReferenceException("There is no customer with this ID.");
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
                    throw new ApplicationException("An error occurred while trying to retrieve the customer.");
                }
            }
        }

        public void InsertAndUpdateCustomer(Customer customer)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Create SqlCommand-objekt that execute stored procedure.
                    SqlCommand cmd = new SqlCommand("dbo.usp_InsertAndUpdateCustomer", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = customer.Id;
                    cmd.Parameters.Add("@CustomerId", SqlDbType.Int, 4).Value = customer.CustomerId;
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = customer.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = customer.LastName;
                    cmd.Parameters.Add("@AddressLine1", SqlDbType.VarChar, 50).Value = customer.AddressLine1;
                    cmd.Parameters.Add("@City", SqlDbType.VarChar, 50).Value = customer.City;
                    cmd.Parameters.Add("@Region", SqlDbType.VarChar, 50).Value = customer.Region;
                    cmd.Parameters.Add("@ZipCode", SqlDbType.VarChar, 50).Value = customer.ZipCode;
                    cmd.Parameters.Add("@Country", SqlDbType.VarChar, 50).Value = customer.Country;
                    cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 20).Value = customer.PhoneNumber;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = customer.Email;

                    //Open database connection.
                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    //Throw Exception
                    throw new ApplicationException("An error occurred while trying to update or insert the customer.");
                }
            }
        }

        #endregion

    }
}
