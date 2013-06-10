﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;
using BLL;

namespace TestProject.Models
{
    public class CategoryDetailsModel
    {
        public IEnumerable<Category> Subcategories { get; set; }
        public Category Category { get; set; }
    }
}