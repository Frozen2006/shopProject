using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using iTechArt.Shop.Web.App_Start;
using iTechArt.Shop.Common;

namespace iTechArt.Shop.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            MapperInit.Init();
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            HttpContext ctx = HttpContext.Current;
            ShopExceptoion ex = ctx.Server.GetLastError() as ShopExceptoion;
            ctx.Response.Clear();

            if (ex == null)
            {
                Response.Redirect("/Error");
            }
            else
            {
                Response.Redirect("/Error/Error?Code=" + ex.Code);
            }

            //Really good method is at http://hystrix.com.ua/2011/01/23/error-handling-for-all-asp-net-mvc3-application/
        }
    }

}