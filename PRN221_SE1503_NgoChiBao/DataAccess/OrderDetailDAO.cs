using BusinessObject.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderDetailDAO() { }
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public OrderDetail GetOrderDetailByOrderID(int orderID)
        {
            OrderDetail orderdetail = null;
            try
            {
                var fStoreDB = new FStoreDBContext();
                orderdetail = fStoreDB.OrderDetails.SingleOrDefault(orderdetail => orderdetail.OrderId == orderID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderdetail;
        }

        public void AddNew(OrderDetail orderdetail)
        {
            try
            {
                OrderDetail _orderdetail = GetOrderDetailByOrderID(orderdetail.OrderId);
                if (_orderdetail == null)
                {
                    var fStoreDB = new FStoreDBContext();
                    fStoreDB.OrderDetails.Add(orderdetail);
                    fStoreDB.SaveChanges();
                }
                else
                {
                    throw new Exception("Order is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(OrderDetail orderdetail)
        {
            try
            {
                OrderDetail ord = GetOrderDetailByOrderID(orderdetail.OrderId);
                if (ord != null)
                {
                    var fStoreDB = new FStoreDBContext();
                    fStoreDB.Entry<OrderDetail>(orderdetail).State = EntityState.Modified;
                    fStoreDB.SaveChanges();
                }
                else
                {
                    throw new Exception("Order does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(OrderDetail orderdetail)
        {
            try
            {
                OrderDetail _orderdetail = GetOrderDetailByOrderID(orderdetail.OrderId);
                if (_orderdetail != null)
                {
                    var fStoreDB = new FStoreDBContext();
                    fStoreDB.OrderDetails.Remove(orderdetail);
                    fStoreDB.SaveChanges();
                }
                else
                {
                    throw new Exception("Order detail does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
