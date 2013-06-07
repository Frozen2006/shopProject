using iTechArt.Shop.Entities;
using System.Collections.Generic;
using iTechArt.Shop.Entities;

namespace TestProject.Models
{
    public class CategotyNavigationModel
    {
        public IEnumerable<Category> Parents { get; set; }
        public Category Category { get; set; }
    }
}