using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;
using Interfaces.Repositories;
using Ninject.Infrastructure;
using Ninject.Modules;
using Ninject.Web.Common;
using System.Data.Entity;

namespace NinjectModules
{
    public class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<Entities.ShopContext>().InRequestScope();
          //  Bind<DbContext>().To<Entities.ShopContext>().InScope( ctx => StandardScopeCallbacks.Request ?? StandardScopeCallbacks.Thread)
            Bind<ICategoryRepository>().To<DAL.Repositories.DbFirstRepository.CategoryRepository>();
            Bind<IRepository<Product>>().To<DAL.Repositories.DbFirstRepository.ProductRepository>(); 
            

            Bind<DAL.membership.UserRepository>().To<DAL.membership.UserRepository>();
            Bind<DAL.membership.RoleRepository>().To<DAL.membership.RoleRepository>();
            Bind<DAL.membership.SessionRepository>().To<DAL.membership.SessionRepository>();
            Bind<DAL.membership.ZipRepository>().To<DAL.membership.ZipRepository>();
            Bind<DAL.membership.TimeSlotsRepository>().ToSelf();
            Bind<DAL.membership.OrdersRepository>().ToSelf();

            Bind<Interfaces.IZipCode>().To<BLL.membership.ZipCodeService>();
            Bind<BLL.CategoryService>().ToSelf();
            Bind<BLL.membership.UsersService>().To<BLL.membership.UsersService>();

            //Cart
            Bind<Interfaces.ICart>().To<BLL.CartService>().InRequestScope();

            Bind<BLL.SearchService>().To<BLL.SearchService>();

            Bind<BLL.TimeSlotsService>().ToSelf();

        }
    }
}
