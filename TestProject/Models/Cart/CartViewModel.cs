using System.Collections.Generic;
using iTechArt.Shop.Entities.PresentationModels;

namespace iTechArt.Shop.Web.Models
{
    public class CartViewModel
    {
        public List<ProductInCart> Products;
        public double TotalPrice;
    }
}