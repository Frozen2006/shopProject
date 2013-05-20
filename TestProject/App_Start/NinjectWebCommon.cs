using System.Data.Entity;

[assembly: WebActivator.PreApplicationStartMethod(typeof(TestProject.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(TestProject.App_Start.NinjectWebCommon), "Stop")]

namespace TestProject.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

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
            //DAL Bindings

            kernel.Bind<DbContext>().To<DAL.ShopContext>().InRequestScope();
            kernel.Bind<DAL.Repositories.ICategoryRepository>()
                  .To<DAL.Repositories.DbFirstRepository.CategoryRepository>();
            kernel.Bind<DAL.Repositories.IRepository<DAL.Product>>()
                  .To<DAL.Repositories.DbFirstRepository.ProductRepository>();

            Kernel = kernel;
        }        
    }
}
