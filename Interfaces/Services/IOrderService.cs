using System.Collections.Generic;
using Helpers;

namespace iTechArt.Shop.Common.Services
{
    public interface IOrderService
    {
        int CreateOrder(string userEmail, int timeSlotId, string comments);
        List<OrdersInList> GetUserOrders(string userEmail);
        OrdersDetails GetOrderDetails(int id);
        void UpdateOrder(int id, OrderStatus orderStatus);
        OrdersDetails GetOrderForPayment(int orderId);
    }
}
