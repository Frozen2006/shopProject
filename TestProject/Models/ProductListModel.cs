using BLL;
using Entities;
using System.Collections.Generic;

namespace TestProject.Models
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