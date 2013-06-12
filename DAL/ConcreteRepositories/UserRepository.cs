using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using iTechArt.Shop.Common.Repositories;
using iTechArt.Shop.Entities;
using iTechArt.Shop.Common.Services;


namespace iTechArt.Shop.DataAccess.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {

        public UserRepository(DbContext context) : base(context)
        {
            Debug.WriteLine("User repository get {0} context", context.GetHashCode());
        }


        public override void Update(User tiem)
        {

        
                User us = CurrentDbSet.FirstOrDefault(m => m.Id == tiem.Id);

                if (us != null)
                {
                    us = Mapper.Map(tiem, us);
                    Debug.WriteLine("Now try use context: "+Context.GetHashCode());

                    Context.SaveChanges();
                }


        }
    }
}
