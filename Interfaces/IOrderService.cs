using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;

namespace Interfaces
{
    public interface IOrderService
    {
        int CreateOrder(string userEmail, int timeSlotId, string comments);
        List<OrdersInList> GetUserOrders(string userEmail);
        OrdersDetails GetOrderDetails(int id);
        void UpdateOrder(int id, OrderStatus orderStatus);
    }
}
