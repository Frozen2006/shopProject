﻿using System;
using System.Collections.Generic;
using System.Linq;
using iTechArt.Shop.Common.Enumerations;
using iTechArt.Shop.Common.Repositories;
using iTechArt.Shop.Entities;
using iTechArt.Shop.Common.Services;
using Ninject;

namespace iTechArt.Shop.Logic.Services
{
    public class CategoryService : ICategoryService
    {
        [Inject]
        public IRepository<Product> ProductRepository { get; set; }
        [Inject]
        public ICategoryRepository CategoryRepository { get; set; }
        
        public Category GetRootCategory()
        {
            return CategoryRepository.GetRootCategory();
        }
        
        public Category GetCategoryById(int id)
        {
            return CategoryRepository.Read(id);
        }

        public IEnumerable<Category> GetSubcategories(Category category)
        {
            return category.ChildCategories.Where(c => c.Id != category.Id);
        }

        public IEnumerable<Category> GetParentsList(Category category)
        {
            Category parent = category.ParentCategory;
            var parents = new List<Category>();

            while (category.ParentCategory.Id != category.Id)
            {
                parents.Add(category.ParentCategory);
                category = category.ParentCategory;
            }

            parents.Reverse();

            return parents;
        }

        public IEnumerable<Product> GetProducts(Category category, int page, int pageSize, SortType sort, bool reverse)
        {
            IEnumerable<Product> products = category.Products;

            switch (sort)
            {
                case SortType.Alphabetic:
                    products = products.OrderBy(prod => prod.Name);
                    break;
                case SortType.Price:
                    products = products.OrderBy(prod => prod.Price);
                    break;
                default:
                    goto case SortType.Alphabetic;
            }

            if (reverse)
            {
                products = products.Reverse();
            }

            return products.Skip(pageSize * (page - 1)).Take(pageSize);
        }

        public Product GetProduct(int productId)
        {
            return ProductRepository.Read(productId);
        }
    }
}
