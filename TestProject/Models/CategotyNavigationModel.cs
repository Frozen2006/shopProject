using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestProject.Models
{
    public class CategotyNavigationModel
    {
        public IEnumerable<Category> Parents { get; set; }
        public Category Category { get; set; }
    }
}