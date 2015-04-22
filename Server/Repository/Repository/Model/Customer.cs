using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public class Customer
    {
        //Kanske någon input validering

        #region Fields

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        #endregion

        #region Constructor

        public Customer(int id, int customerId, string firstName, string lastName, string addressLine1, string addressLine2,
            string city, string region, string zipCode, string country, string phoneNumber, string email)
        {
            Id = id;
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            City = city;
            Region = region;
            ZipCode = zipCode;
            Country = country;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        #endregion
    }
}
