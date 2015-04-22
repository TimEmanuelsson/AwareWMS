using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    class Order
    {
        //Kanske någon input validering

        #region Fields

        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int OrderStatus { get; set; }
        public DateTime Date { get; set; }
        public DateTime LastUpdate { get; set; }
        public int PaymentStatus { get; set; }

        #endregion

        #region Constructor

        public Order(int id, int orderId, int customerId, int orderStatus, DateTime date, DateTime lastUpdate, int paymentStatus)
        {
            Id = id;
            OrderId = orderId;
            CustomerId = customerId;
            OrderStatus = orderStatus;
            Date = date;
            LastUpdate = lastUpdate;
            PaymentStatus = paymentStatus;
        }

        #endregion
    }
}
