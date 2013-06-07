using iTechArt.Shop.Entities;
using System.Collections.Generic;

namespace iTechArt.Shop.Web.Models
{
    public class CategotyNavigationModel
    {
        public IEnumerable<Category> Parents { get; set; }
        public Category Category { get; set; }
    }
}