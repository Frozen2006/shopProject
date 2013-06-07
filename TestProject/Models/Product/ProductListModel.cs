using Interfaces;
using iTechArt.Shop.Logic.Services;
using iTechArt.Shop.Entities;
using System.Collections.Generic;
using iTechArt.Shop.Entities;

namespace iTechArt.Shop.Web.Models
{
    public class ProductListModel
    {
        public Category Category { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public SortType SortType { get; set; }
        public bool Reverse { get; set; }
    }
}