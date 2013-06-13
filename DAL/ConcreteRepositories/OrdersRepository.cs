using System.Data.Entity;
using System.Linq;
using AutoMapper;
using iTechArt.Shop.Common.Repositories;
using iTechArt.Shop.DataAccess.Base;
using iTechArt.Shop.Entities;

namespace iTechArt.Shop.DataAccess.ConcreteRepositories
{
    public class OrdersRepository : RepositoryBase<Order>, IOrdersRepository
    {
        public OrdersRepository(DbContext context)
            : base(context)
        {
        }


        public override void Update(Order item)
        {
            Order ord = CurrentDbSet.FirstOrDefault(m => m.Id == item.Id);

            if (ord != null)
            {
                ord = Mapper.Map(item, ord);
                Context.SaveChanges();
            }
        }
    }
}
