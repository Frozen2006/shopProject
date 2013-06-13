using iTechArt.Shop.Common;
using iTechArt.Shop.Common.Repositories;
using iTechArt.Shop.DataAccess.ConcreteRepositories;
using iTechArt.Shop.Logic;
using iTechArt.Shop.Logic.Services;
using Entities;
using iTechArt.Shop.Entities;
using iTechArt.Shop.Common.Services;
using Ninject.Modules;
using Ninject.Web.Common;
using System.Data.Entity;
using iTechArt.Shop.Logic.Membership;
using iTechArt.Shop.Web.Helpers;

namespace NinjectModules
{
    public class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<ShopContext>().InRequestScope();

            Bind<ICategoryRepository>().To<CategoryRepository>();
            Bind<IRepository<Product>>().To<ProductRepository>();
            Bind<ISessionContext>().To<SessionContextMapper>();

            Bind<IUserRepository>().To<UserRepository>();
            Bind<ISessionRepository>().To<SessionRepository>();
            Bind<IZipRepository>().To<ZipRepository>();
            Bind<TimeSlotsRepository>().To<TimeSlotsRepository>();
            Bind<IOrdersRepository>().To <OrdersRepository>();

            Bind<IZipCodeService>().To<ZipCodeService>();
            Bind<ICategoryService>().To<CategoryService>();
            Bind<IUserService>().To<UsersService>();
            Bind<IOrderService>().To<OrderService>();
            Bind<IErrorService>().To<ErrorService>();

            //Cart
            Bind<ICartService>().To<CartService>().InRequestScope();

            Bind<ISearchService>().To<SearchService>();

            Bind<ITimeSlotsService>().To<TimeSlotsService>();

        }
    }
}
