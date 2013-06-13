using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using iTechArt.Shop.Common.Repositories;
using iTechArt.Shop.DataAccess.Base;
using iTechArt.Shop.Entities;

namespace iTechArt.Shop.DataAccess.ConcreteRepositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {

        public UserRepository(DbContext context) : base(context)
        {
            Debug.WriteLine("User repository get {0} context", context.GetHashCode());
        }


        public override void Update(User item)
        {

        
                User us = CurrentDbSet.FirstOrDefault(m => m.Id == item.Id);

                if (us != null)
                {
                    us = Mapper.Map(item, us);
                    Debug.WriteLine("Now try use context: "+Context.GetHashCode());

                    Context.SaveChanges();
                }


        }
    }
}
