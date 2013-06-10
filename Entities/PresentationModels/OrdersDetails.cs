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
        public DateTime startDeliveryTime { get; set; }
        public DateTime endErliveryTime { get; set; }
        public string Comments { get; set; }
        public string userEmail { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
