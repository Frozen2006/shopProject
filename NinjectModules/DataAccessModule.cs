using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;
using Interfaces.Repositories;
using Ninject.Modules;
using System.Data.Entity;
using Ninject.Web.Common;

namespace NinjectModules
{
    public class DataAccessModule : NinjectModule
    {
        public override void Load()
        {

            Bind<DbContext>().To<Entities.ShopContext>().InRequestScope();

            Bind<ICategoryRepository>().To<DAL.Repositories.DbFirstRepository.CategoryRepository>();
            Bind<IRepository<Product>>().To<DAL.Repositories.DbFirstRepository.ProductRepository>(); 
            

            Bind<DAL.membership.UserRepository>().To<DAL.membership.UserRepository>();
            Bind<DAL.membership.RoleRepository>().To<DAL.membership.RoleRepository>();
            Bind<DAL.membership.SessionRepository>().To<DAL.membership.SessionRepository>();
            Bind<DAL.membership.ZipRepository>().To<DAL.membership.ZipRepository>();

            Bind<Interfaces.IZipCode>().To<BLL.membership.ZipCodeService>();
            Bind<Interfaces.ICart>().To<BLL.FakeCartService>();
            Bind<BLL.CategoryService>().ToSelf();
            Bind<BLL.membership.UsersService>().To<BLL.membership.UsersService>();

        }
    }
}
