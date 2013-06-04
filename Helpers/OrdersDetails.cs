using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
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
