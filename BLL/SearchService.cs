using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using iTechArt.Shop.Common.Enumerations;
using iTechArt.Shop.DataAccess.Repositories;
using iTechArt.Shop.Entities;
using iTechArt.Shop.Common.Services;
using Ninject;
using iTechArt.Shop.Entities.PresentationModels;

namespace iTechArt.Shop.Logic
{
    public class SearchService : ISearchService
    {
        private readonly ProductRepository _productRepo;
        private readonly CategoryRepository _categoryRepository;

        [Inject]
        public SearchService(ProductRepository pr, CategoryRepository cr)
        {
            _productRepo = pr;
            _categoryRepository = cr;
        }

        //Method to search autocompleat
        public List<Product> GetTop10Results(string searchData)
        {
            var searchDara = _productRepo.ReadAll().Where(m => m.Name.Contains(searchData)).Take(10);

            return searchDara.ToList();
        }

        //Return list of categories, with contain product's in search results
        //This method need to correct displaying category list in search result's page
        public List<CategoriesInSearch> GetCategories(string searchData)
        {
            string serchString = searchData.ToLowerInvariant();
            var allCat = new List<CategoriesInSearch>();
            var categories = _categoryRepository.ReadAll().Where(m => m.Products.Count(q => q.Name.Contains(serchString)) > 0);

            foreach (var category in categories)
            {
                int countInCategory = category.Products.Count(m => m.Name.ToLowerInvariant().Contains(serchString));
                allCat.Add(new CategoriesInSearch{ Category = category, Count = countInCategory});
            }

            return allCat;
        }

        // Find data with padination
        public SearchResult GetResults(string searchData, int? categoryId, int page, int pageSize, SortType sort, bool reverse)
        {
            IEnumerable<Product> products;

            Expression<Func<Product, bool>> where;
            
            if (categoryId == null)
            {
                where = (m) => m.Name.Contains(searchData);
            }
            else
            {
                where = (m) => ((m.Category.Id == (int)categoryId) && (m.Name.Contains(searchData)));
            }

            switch (sort)
            {
                case SortType.Alphabetic:
                    products =
                        _productRepo.ReadAll()
                                    .Where(where)
                                    .OrderBy(m => m.Name)
                                    .Skip(pageSize*(page - 1))
                                    .Take(pageSize);
                    break;
                case SortType.Price:
                    products = _productRepo.ReadAll()
                                    .Where(where)
                                    .OrderBy(m => m.Price)
                                    .Skip(pageSize * (page - 1))
                                    .Take(pageSize);
                    break;
                default:
                    return null;
            }

            if (reverse)
                products = products.Reverse();


            int countOfAllProducts = 0;

            if (categoryId == null)
            {
                countOfAllProducts = _productRepo.ReadAll().Count(m => m.Name.Contains(searchData));
            }
            else
            {
                countOfAllProducts = _productRepo.ReadAll().Count(m => m.Name.Contains(searchData) && (m.Category.Id == (int) categoryId));
            }

            var pis = new SearchResult { Products = products.ToList(), AllCount = countOfAllProducts};

            return pis;
        }
    }
}
