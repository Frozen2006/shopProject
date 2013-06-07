using System.Collections.Generic;
using Helpers;

namespace iTechArt.Shop.Web.Models
{
    public class CartViewModel
    {
        public List<ProductInCart> Products;
        public double TotalPrice;
    }
}