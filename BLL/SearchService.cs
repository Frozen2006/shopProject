using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.DbFirstRepository;
using Entities;
using Helpers;
using Interfaces.Repositories;
using Ninject;

namespace BLL
{
    public class SearchService
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
            List<Product> outData = new List<Product>();

            var searchDara = _productRepo.ReadAll().Where(m => m.Name.Contains(searchData)).Take(10);

            foreach (var product in searchDara)
            {
                outData.Add(product);
            }

            return outData;
        }

        public List<CategoriesInSearch> GetCategories(string searchData)
        {
            List<CategoriesInSearch> allCat = new List<CategoriesInSearch>();
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
        public ProductInSearch GetProductsFromCategory(string searchData, string categoryName)
        {
            var searchedData = _productRepo.ReadAll().Where(m => (m.Name.Contains(searchData)) && (m.Category.Name == categoryName));


            ProductInSearch pis = new ProductInSearch()
                {
                    AllCount = searchedData.Count(),
                    Products = searchedData.ToList()
                };

            return pis;
        }

        // Find data with padination
        public ProductInSearch GetResults(string searchData, int page, int pageSize, SortType sort, bool reverse)
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



            int countOfAllProducts = _productRepo.ReadAll().Where(m => m.Name.Contains(searchData)).Count();



            ProductInSearch pis = new ProductInSearch() { Products = products.ToList(), AllCount = countOfAllProducts};

            return pis;
        }
    }
}
