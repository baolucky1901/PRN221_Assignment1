using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderDetailRepository
    {
        OrderDetail GetOrderDetailByOrderID(int orderId);
        void InsertOrderDetail(OrderDetail orderdetail);
        void DeleteOrderDetail(OrderDetail orderdetail);
        void UpdateOrderDetail(OrderDetail orderdetail);
    }
}
