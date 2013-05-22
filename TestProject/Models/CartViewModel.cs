using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestProject.Models
{
    public class CartViewModel
    {
        public List<Helpers.ProductInCart> Products;
        public double TotalPrice;
    }
}