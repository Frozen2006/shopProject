using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;
using Helpers;

namespace TestProject.Models
{
    public class SearchPageModel
    {
        public List<CategoriesInSearch> Categories;
        public List<Product> Products;
    }
}