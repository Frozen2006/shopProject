using System;

namespace iTechArt.Shop.Entities.PresentationModels
{
    public class OrdersInList
    {
        public int Id { get; set; }
        public DateTime startOrderTime { get; set; }
        public DateTime endOrderTime { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public double price { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
