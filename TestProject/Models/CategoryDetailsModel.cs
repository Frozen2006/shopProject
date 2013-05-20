using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

namespace TestProject.Models
{
    public class CategoryDetailsModel
    {
        public IEnumerable<Category> Parents { get; set; }
        public IEnumerable<Category> Subcategories { get; set; }
        public Category Category { get; set; }
    }
}