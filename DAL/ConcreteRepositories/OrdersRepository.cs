using System.Data.Entity;
using System.Linq;
using AutoMapper;
using iTechArt.Shop.Common.Repositories;
using iTechArt.Shop.Entities;

namespace iTechArt.Shop.DataAccess.Repositories
{
    public class OrdersRepository : RepositoryBase<Order>, IOrdersRepository
    {
        public OrdersRepository(DbContext context)
            : base(context)
        {
        }


        public override void Update(Order tiem)
        {
            Order ord = CurrentDbSet.FirstOrDefault(m => m.Id == tiem.Id);

            if (ord != null)
            {
                ord = Mapper.Map(tiem, ord);
                Context.SaveChanges();
            }
        }
    }
}
