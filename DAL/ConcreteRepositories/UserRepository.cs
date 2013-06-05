using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.DbFirstRepository;
using Entities;
using Interfaces;


namespace DAL.membership
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
                    us.email = tiem.email;
                    us.address = tiem.address;
                    us.address2 = tiem.address2;
                    us.city = tiem.city;
                    us.first_name = tiem.first_name;
                    us.last_name = tiem.last_name;
                    us.password = tiem.password;
                    us.phone = tiem.phone;
                    us.phone2 = tiem.phone2;
                    us.title = tiem.title;
                    us.zip = tiem.zip;

                    Debug.WriteLine("Now try use context: "+Context.GetHashCode());
                    lock(_lock)
                        Context.SaveChanges();
                }


        }
    }
}
