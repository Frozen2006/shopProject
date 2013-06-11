using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using iTechArt.Shop.Common.Repositories;
using iTechArt.Shop.Entities;
using iTechArt.Shop.Common.Services;


namespace iTechArt.Shop.DataAccess.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {

        private static object _lock = new object();

        public UserRepository(DbContext context) : base(context)
        {
            Debug.WriteLine("User repository get {0} context", context.GetHashCode());
        }


        public override void Update(User tiem)
        {

        
                User us = CurrentDbSet.FirstOrDefault(m => m.Id == tiem.Id);

                if (us != null)
                {
                    us.Email = tiem.Email;
                    us.Address = tiem.Address;
                    us.Address2 = tiem.Address2;
                    us.City = tiem.City;
                    us.FirstName = tiem.FirstName;
                    us.LastName = tiem.LastName;
                    us.Password = tiem.Password;
                    us.Phone = tiem.Phone;
                    us.Phone2 = tiem.Phone2;
                    us.Title = tiem.Title;
                    us.Zip = tiem.Zip;
                    Debug.WriteLine("Now try use context: "+Context.GetHashCode());
                    lock(_lock)
                        Context.SaveChanges();
                }


        }
    }
}
