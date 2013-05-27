using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.DbFirstRepository;
using Entities;
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

        //Return all products from category
        public List<Product> GetProductsFromCategory(string searchData, int categoryId)
        {
            List<Product> outData = new List<Product>();

            var searchDara = _productRepo.ReadAll().Where(m => (m.Name.Contains(searchData)) && (m.CategoryId == categoryId));

            foreach (var product in searchDara)
            {
                outData.Add(product);
            }

            return outData;
        }

        // Find data with padination
        public List<Product> GetResults(string searchData, int page, int pageSize, SortType sort, bool reverse)
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

            return products.ToList();
        }
    }
}
