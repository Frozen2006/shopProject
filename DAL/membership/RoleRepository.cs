using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.DbFirstRepository;

namespace DAL.membership
{
    public class RoleRepository : RepositoryBase<Role>
    {
        public RoleRepository(ShopContext context)
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
