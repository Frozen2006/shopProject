using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using TestProject;
using iTechArt.Shop.Entities;
using Helpers;
using TestProject.Models;
using iTechArt.Shop.Entities;

namespace iTechArt.Shop.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Mapper.CreateMap<Product, ProductInCart>();
            Mapper.CreateMap<RegisterModel, User>();
            Mapper.CreateMap<User, UserDetails>();
            Mapper.CreateMap<DeliverySpot, BookinSlot>();
            Mapper.CreateMap<Order, OrdersDetails>();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}