using System;
using System.Collections.Generic;

namespace iTechArt.Shop.Entities.PresentationModels
{
    public class OrdersDetails
    {
        public int Id;
        public List<ProductInCart> Products;
        public double TotalPrice;
        public double PriceWithDiscount;
        public DateTime startDeliveryTime;
        public DateTime endErliveryTime;
        public string Comments;
        public string userEmail;
        public OrderStatus OrderStatus;
        public DateTime CreationTime;
    }
}
