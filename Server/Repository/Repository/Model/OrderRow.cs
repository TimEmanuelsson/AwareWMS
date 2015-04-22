using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public class OrderRow
    {
        //Kanske någon input validering

        #region Fields

        public int OrderRowId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }

        #endregion

        #region Constructor

        public OrderRow(int id, int orderId, int productId, int amount)
        {
            OrderRowId = id;
            OrderId = orderId;
            ProductId = productId;
            Amount = amount;
        }

        #endregion
    }
}
