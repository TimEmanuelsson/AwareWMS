using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    class OrderStatus
    {
        //Kanske någon input validering
        #region Fields

        public int OrderStatusId { get; set; }
        public string Status { get; set; }

        #endregion

        #region Constructor

        public OrderStatus(int id, string status)
        {
            OrderStatusId = id;
            Status = status;
        }

        #endregion
    }  
}
