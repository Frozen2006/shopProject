using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;
using BLL;

namespace TestProject.Models
{
    public class CategoryDetailsModel
    {
        public IEnumerable<Category> Parents { get; set; }
        public IEnumerable<Category> Subcategories { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public SortType SortType { get; set; }
        public bool Reverse { get; set; }
    }
}