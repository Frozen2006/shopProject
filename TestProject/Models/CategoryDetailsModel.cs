using System.Collections.Generic;
using iTechArt.Shop.Entities;
using iTechArt.Shop.Entities;

namespace TestProject.Models
{
    public class CategoryDetailsModel
    {
        public IEnumerable<Category> Subcategories { get; set; }
        public Category Category { get; set; }
    }
}