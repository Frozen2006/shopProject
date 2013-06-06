﻿using System.Collections.Generic;
using BLL;
using Entities;
using Helpers;

namespace TestProject.Models
{
    public class SearchPageModel
    {
        public List<CategoriesInSearch> Categories;
        public List<Product> Products;
        public string SearchRequest;
        public Category Category;
        public SortType SortType;
        public int PageSize;
        public bool Reverse;
        public int Page;
        public int CountAll;
    }
}