using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Model.DAL;
using AwareClassLibrary;

namespace Repository.Model
{
    public class Service
    {
        #region Product methods

        private ProductDAL _productDAL;

        private ProductDAL ProductDAL 
        {
            get { return _productDAL ?? (_productDAL = new ProductDAL()); }
        }

        public Product GetProductById(int Id)
        {
            return ProductDAL.GetProductById(Id);
        }

        public Product GetProductByEAN(int ean)
        {
            return ProductDAL.GetProductByEAN(ean);
        }

        public Product GetProductBySKU(int sku)
        {
            return ProductDAL.GetProductBySKU(sku);
        }

        public IEnumerable<Product> GetProducts() 
        {
            return ProductDAL.GetProducts();
        }

        public int GetProductCount()
        {
            return ProductDAL.GetProductCount();
        }

        public int GetProductsQuantitySum()
        {
            return ProductDAL.GetProductsQuantitySum();
        }

        public int CheckIfProductBusy(int productId)
        {
            return ProductDAL.CheckIfProductBusy(productId);
        }

        public void UpdateProduct(Product product) 
        {
            //Kanske någon validering här!
            ProductDAL.UpdateProduct(product);
        }

        public void ProductInventory(Product product)
        {
            ProductDAL.ProductInventory(product);
        }

        public void InsertAndUpdateProduct(Product product)
        {
            //Kanske någon validering här!
            ProductDAL.InsertAndUpdateProduct(product);
        }

        public void InsertAndUpdateProductList(List<Product> products)
        {
            foreach (Product product in products)
            {
                InsertAndUpdateProduct(product);
            }
        }

        #endregion

        #region Order methods

        private OrderDAL _orderDAL;

        private OrderDAL OrderDAL
        {
            get { return _orderDAL ?? (_orderDAL = new OrderDAL()); }
        }

        public Order GetOrderById(int Id)
        {
            return OrderDAL.GetOrderById(Id);
        }

        public IEnumerable<Order> GetOrders()
        {
            return OrderDAL.GetOrders();
        }

        public void UpdateOrder(Order order)
        {
            //Kanske någon validering här!
            OrderDAL.UpdateOrder(order);
        }

        public void UpdateOrderStatus(int orderId, int statusId)
        {
            OrderDAL.UpdateOrderStatus(orderId, statusId);
        }

        public void InsertAndUpdateOrder(Order order)
        {
            //Kanske någon validering här!
            OrderDAL.InsertAndUpdateOrder(order);
        }

        #endregion

        #region Customer methods

        private CustomerDAL _customerDAL;

        private CustomerDAL CustomerDAL
        {
            get { return _customerDAL ?? (_customerDAL = new CustomerDAL()); }
        }

        //Testa så att det går att hämta ut en kund när de finns några i databasen.
        public Customer GetCustomerById(int Id)
        {
            return CustomerDAL.GetCustomerById(Id);
        }

        public void InsertAndUpdateCustomer(Customer customer)
        {
            CustomerDAL.InsertAndUpdateCustomer(customer);
        }

        #endregion

        #region OrderRow methods

        private OrderRowDAL _orderRowDAL;

        private OrderRowDAL OrderRowDAL
        {
            get { return _orderRowDAL ?? (_orderRowDAL = new OrderRowDAL()); }
        }

        public OrderRow GetOrderRowById(int id)
        {
            return OrderRowDAL.GetOrderRowById(id);
        }

        public IEnumerable<OrderRow> GetOrderRows()
        {
            return OrderRowDAL.GetOrderRows();
        }

        public IEnumerable<OrderRow> GetOrderRowsByOrderId(int Id)
        {
            return OrderRowDAL.GetOrderRowsByOrderId(Id);
        }

        #endregion

        #region OrderStatus methods

        private OrderStatusDAL _orderStatusDAL;

        private OrderStatusDAL OrderStatusDAL
        {
            get { return _orderStatusDAL ?? (_orderStatusDAL = new OrderStatusDAL()); }
        }

        public OrderStatus GetOrderStatusById(int Id)
        {
            return OrderStatusDAL.GetOrderStatusById(Id);
        }

        public OrderStatus GetOrderStatusByOrderId(int id)
        {
            int statusId = OrderStatusDAL.GetOrderStatusByOrderId(id);
            return GetOrderStatusById(statusId);
        }

        #endregion

        #region Exception methods

        private ExceptionLogDAL _exceptionLogDAL;

        private ExceptionLogDAL ExceptionLogDAL
        {
            get { return _exceptionLogDAL ?? (_exceptionLogDAL = new ExceptionLogDAL()); }
        }

        public void InsertException(ExceptionLog exception)
        {
            //Kanske någon validering här!
            ExceptionLogDAL.InsertException(exception);
        }

        #endregion
    }
}
