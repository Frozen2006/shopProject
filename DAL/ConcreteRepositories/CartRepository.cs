using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.DbFirstRepository;
using Entities;

namespace DAL.membership
{
    public class CartRepository : RepositoryBase<Cart>
    {
        public CartRepository(DbContext context)
            : base(context)
        {
        }


        public override void Update(Cart tiem)
        {
            

            Cart cart = CurrentDbSet.FirstOrDefault(m => m.Product_Id == tiem.Product_Id);

            if (cart != null)
            {
                cart.Count = tiem.Count;
                cart.Product = cart.Product;
                Context.SaveChanges();
            }

        }
    }
}
