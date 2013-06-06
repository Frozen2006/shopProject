using Entities;
using System.Collections.Generic;

namespace TestProject.Models
{
    public class CategotyNavigationModel
    {
        public IEnumerable<Category> Parents { get; set; }
        public Category Category { get; set; }
    }
}