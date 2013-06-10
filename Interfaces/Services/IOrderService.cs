using System.Collections.Generic;
using iTechArt.Shop.Common.Enumerations;
using iTechArt.Shop.Entities.PresentationModels;

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
