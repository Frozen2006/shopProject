using System;
using System.Collections.Generic;
using System.Linq;
using iTechArt.Shop.DataAccess.Repositories;
using iTechArt.Shop.Entities;
using Helpers;
using iTechArt.Shop.Common.Services;
using Ninject;
using iTechArt.Shop.Web.Common;

namespace iTechArt.Shop.Logic.Services
{
    public class SearchService : ISearchService
    {
        private readonly ProductRepository _productRepo;

        [Inject]
        public SearchService(ProductRepository pr)
        {
            _productRepo = pr;
        }


        //Method to search autocompleat
        public List<Product> GetTop10Results(string searchData)
        {
            var searchDara = _productRepo.ReadAll().Where(m => m.Name.Contains(searchData)).Take(10);

            return searchDara.ToList();
        }

        public List<CategoriesInSearch> GetCategories(string searchData)
        {
            var allCat = new List<CategoriesInSearch>();
            var q = _productRepo.ReadAll().Where(m => m.Name.Contains(searchData));

            foreach (var product in q)
            {
                CategoriesInSearch qq = allCat.FirstOrDefault(m => m.Category == product.Category);
                if (qq == null)
                {
                    allCat.Add(new CategoriesInSearch{ Category = product.Category, Count = 1});
                }
                else
                {
                    qq.Count++;
                }
            }

            return allCat;
        }

        //Return all products from category
        public SearchResult GetProductsFromCategory(string searchData, int categoryId)
        {
            var searchedData = _productRepo.ReadAll().Where(m => (m.Name.Contains(searchData)) && (m.Category.Id == categoryId));


            var pis = new SearchResult
                {
                    AllCount = searchedData.Count(),
                    Products = searchedData.ToList()
                };

            return pis;
        }

        // Find data with padination
        public SearchResult GetResults(string searchData, int page, int pageSize, SortType sort, bool reverse)
        {
            IEnumerable<Product> products;

            switch (sort)
            {
                case SortType.Alphabetic:
                    products =
                        _productRepo.ReadAll()
                                    .Where(m => m.Name.Contains(searchData))
                                    .OrderBy(m => m.Name)
                                    .Skip(pageSize*(page - 1))
                                    .Take(pageSize);
                    break;
                case SortType.Price:
                    products = _productRepo.ReadAll()
                                    .Where(m => m.Name.Contains(searchData))
                                    .OrderBy(m => m.Price)
                                    .Skip(pageSize * (page - 1))
                                    .Take(pageSize);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("sort");
            }
            //!
            if (reverse)
                products = products.Reverse();



            int countOfAllProducts = _productRepo.ReadAll().Count(m => m.Name.Contains(searchData));



            var pis = new SearchResult { Products = products.ToList(), AllCount = countOfAllProducts};

            return pis;
        }
    }
}
