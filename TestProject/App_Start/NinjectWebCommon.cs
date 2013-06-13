using System.Data.Entity;
using System.IO;
using System.Reflection;
using iTechArt.Shop.Logic.Services;
using iTechArt.Shop.Common.Services;
using Ninject.Modules;


[assembly: WebActivator.PreApplicationStartMethod(typeof(iTechArt.Shop.Web.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(iTechArt.Shop.Web.NinjectWebCommon), "Stop")]

namespace iTechArt.Shop.Web
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using iTechArt.Shop.Entities;

    public static class NinjectWebCommon 
    {
        public static IKernel Kernel { get; set; }

        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //kernel.Load(new DataAccessModule());
            string path = AppDomain.CurrentDomain.BaseDirectory + "bin\\NinjectModules.dll";
            Assembly asem = Assembly.LoadFile(path);

            Type dam = asem.GetType("NinjectModules.DataAccessModule");
            NinjectModule nm = (NinjectModule) Activator.CreateInstance(dam);

            kernel.Load(nm);

            var s = kernel.Get<ICategoryService>();

            Kernel = kernel;
        }        
    }
}
