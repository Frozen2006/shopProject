using System.Collections.Generic;
using iTechArt.Shop.Common.Enumerations;
using iTechArt.Shop.Entities.PresentationModels;
using iTechArt.Shop.Entities;

namespace iTechArt.Shop.Web.Models
{
    public class SearchPageModel
    {
        public List<CategoriesInSearch> Categories { get; set; }
        public List<Product> Products { get; set; }
        public string SearchRequest { get; set; }
        public SortType SortType { get; set; }
        public int PageSize { get; set; }
        public bool Reverse { get; set; }
        public int Page { get; set; }
        public int? CategoryId { get; set; }
        public int CountAll { get; set; }
    }
}