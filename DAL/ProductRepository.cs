﻿using System.Linq;

namespace DAL.Repositories.DbFirstRepository
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(ShopContext context) : base(context)
        {
        }
        public override void Update(Product item)
        {
            var product = CurrentDbSet.Find(item.Id);
            if (product != null)
            {
                product.Name = item.Name;
                product.CategoryId = item.CategoryId;
                product.Price = item.Price;
                product.SellByWeight = item.SellByWeight;
                product.AverageWeight = item.AverageWeight;
                product.UnitOfMeasure = item.UnitOfMeasure;

                //Нужно тестировать.
                product.Category = item.Category;
            }
            Context.SaveChanges();
        }
    }
}
