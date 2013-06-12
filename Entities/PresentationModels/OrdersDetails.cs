using System;
using System.Collections.Generic;

namespace iTechArt.Shop.Entities.PresentationModels
{
    public class OrdersDetails
    {
        public int Id { get; set; }
        public List<ProductInCart> Products { get; set; }
        public double TotalPrice { get; set; }
        public double PriceWithDiscount { get; set; }
        public DateTime StartDeliveryTime { get; set; }
        public DateTime EndErliveryTime { get; set; }
        public string Comments { get; set; }
        public string UserEmail { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
