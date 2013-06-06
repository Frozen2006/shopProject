using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using BLL.membership;
using DAL;
using DAL.Repositories.DbFirstRepository;
using DAL.membership;
using Entities;
using Helpers;
using Interfaces;
using Interfaces.Repositories;
using Ninject.Infrastructure;
using Ninject.Modules;
using Ninject.Web.Common;
using System.Data.Entity;
using TestProject.Helpers;

namespace NinjectModules
{
    public class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<ShopContext>().InRequestScope();
          //  Bind<DbContext>().To<Entities.ShopContext>().InScope( ctx => StandardScopeCallbacks.Request ?? StandardScopeCallbacks.Thread)
            Bind<ICategoryRepository>().To<CategoryRepository>();
            Bind<IRepository<Product>>().To<ProductRepository>();
            Bind<ISessionContext>().To<SessionContextMapper>();

            Bind<IUserRepository>().To<UserRepository>();
            Bind<ISessionRepository>().To<SessionRepository>();
            Bind<IZipRepository>().To<ZipRepository>();
            Bind<TimeSlotsRepository>().To<TimeSlotsRepository>();
            Bind<IOrdersRepository>().To <OrdersRepository>();

            Bind<IZipCode>().To<ZipCodeService>();
            Bind<ICategoryService>().To<CategoryService>();
            Bind<IUserService>().To<UsersService>();
            Bind<IOrderService>().To<OrderService>();

            //Cart
            Bind<ICartService>().To<CartService>().InRequestScope();

            Bind<ISearchService>().To<SearchService>();

            Bind<ITimeSlotsService>().To<TimeSlotsService>();

        }
    }
}
