using System.Collections.Generic;
using iTechArt.Shop.Entities;

namespace iTechArt.Shop.Web.Models
{
    public class CategoryDetailsModel
    {
        public IEnumerable<Category> Subcategories { get; set; }
        public Category Category { get; set; }
    }
}