using System.Collections.Generic;
using iTechArt.Shop.Common.Enumerations;
using iTechArt.Shop.Entities.PresentationModels;
using iTechArt.Shop.Entities;

namespace iTechArt.Shop.Web.Models
{
    public class SearchPageModel
    {
        public List<CategoriesInSearch> Categories;
        public List<Product> Products;
        public string SearchRequest;
        public SortType SortType;
        public int PageSize;
        public bool Reverse;
        public int Page;
        public int? CategoryId;
        public int CountAll;
    }
}