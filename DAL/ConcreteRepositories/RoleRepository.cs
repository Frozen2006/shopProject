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
    public class RoleRepository : RepositoryBase<Role>
    {
        public RoleRepository(DbContext context)
            : base(context)
        {
        }


        public override void Update(Role tiem)
        {
            Role role = CurrentDbSet.FirstOrDefault(m => m.Id == tiem.Id);

            if (role != null)
            {
                role.name = tiem.name;

                Context.SaveChanges();
            }

        }
    }
}
