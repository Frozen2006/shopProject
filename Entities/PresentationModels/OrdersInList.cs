using System;

namespace iTechArt.Shop.Entities.PresentationModels
{
    public class OrdersInList
    {
        public int Id { get; set; }
        public DateTime StartOrderTime { get; set; }
        public DateTime EndOrderTime { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public double Price { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
