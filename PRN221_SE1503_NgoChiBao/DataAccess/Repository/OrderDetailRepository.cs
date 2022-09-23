using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public OrderDetail GetOrderDetailByOrderID(int orderId) => OrderDetailDAO.Instance.GetOrderDetailByOrderID(orderId);
        public void InsertOrderDetail(OrderDetail orderdetail) => OrderDetailDAO.Instance.AddNew(orderdetail);
        public void DeleteOrderDetail(OrderDetail orderdetail) => OrderDetailDAO.Instance.Remove(orderdetail);
        public void UpdateOrderDetail(OrderDetail orderdetail) => OrderDetailDAO.Instance.Update(orderdetail);
    }
}
