using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Helpers;

namespace TestProject.Models
{
    public class CartViewModel
    {
        public List<ProductInCart> Products;
        public double TotalPrice;
    }
}