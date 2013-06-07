using System.Collections.Generic;
using Interfaces;
using iTechArt.Shop.Logic.Services;
using iTechArt.Shop.Entities;
using Helpers;
using iTechArt.Shop.Entities;

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