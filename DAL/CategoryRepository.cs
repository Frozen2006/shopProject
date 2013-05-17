﻿using System.Linq;

namespace DAL.Repositories.DbFirstRepository
{
    public class CategoryRepository : RepositoryBase<Category>
    {
        public CategoryRepository(ShopContext context)
            : base(context)
        {
        }
        public override void Update(Category item)
        {
            var categoty = CurrentDbSet.Find(item.Id);
            if (categoty != null)
            {

                categoty.Parent = item.Parent;
                categoty.Name = item.Name;

                //Не ясно нужно это или нет. Тестировать.
                categoty.Categories1 = item.Categories1;
                categoty.Category1 = item.Category1;
                categoty.Products = item.Products;
            }
            Context.SaveChanges();
        }
    }
}
