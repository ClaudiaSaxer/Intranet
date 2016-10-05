using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Intranet.Bll;
using Intranet.Definition;

namespace Intranet.Web.IoC
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            // Create the builder with which components/services are registered.
            var builder = new ContainerBuilder();

            // ConfigureContainer types that expose interfaces...
            builder.RegisterType<TestAutofac>()
                   .As<ITestAutofac>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<VMHelper>()
                   .As<IVMHelper>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<TestUseAutofac>()
                   .AsSelf()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            // ConfigureContainer your MVC controllers.
            builder.RegisterControllers( typeof(MvcApplication).Assembly );

            // OPTIONAL: ConfigureContainer model binders that require DI.
            builder.RegisterModelBinders( Assembly.GetExecutingAssembly() );
            builder.RegisterModelBinderProvider();

            // OPTIONAL: ConfigureContainer web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource( new ViewRegistrationSource() );

            // OPTIONAL: Enable property injection into action filters.
            //builder.RegisterFilterProvider();

            var container  = builder.Build();
            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver( new AutofacDependencyResolver( container ) );
            


        }
    }
}