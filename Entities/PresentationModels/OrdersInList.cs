using System;

namespace iTechArt.Shop.Entities.PresentationModels
{
    public class OrdersInList
    {
        public int Id;
        public DateTime startOrderTime;
        public DateTime endOrderTime;
        public OrderStatus OrderStatus;
        public double price;
        public DateTime CreationTime;
    }
}
