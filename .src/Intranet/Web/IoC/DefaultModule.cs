using System;
using Autofac;
using Autofac.Integration.Mvc;
using Intranet.Bll;
using Intranet.Common.Logging;
using Intranet.Dal;
using Intranet.Definition;

namespace Intranet.Web.IoC
{
    /// <summary>
    ///     Default composition module.
    /// </summary>
    public class DefaultModule : Module
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the name of the executing application assembly.
        /// </summary>
        /// <value>The name of the executing application assembly.</value>
        public String ApplicationAssemblyName { get; set; }

        /// <summary>
        ///     Gets or sets the default log level for exceptions.
        /// </summary>
        /// <value>The default log level for exceptions.</value>
        public String DefaultExceptionLogLevel { get; set; }

        #endregion

        /// <summary>
        ///     Adds registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be registered.</param>
        protected override void Load( ContainerBuilder builder )
        {
            RegisterLoggingComponents( builder );
            RegisterDataAccessComponents( builder );
            RegisterBllComponents( builder );
            RegisterMVC( builder );
        }

        /// <summary>
        ///     Registers the BLL components.
        /// </summary>
        /// <param name="builder">The builder through which components can be registered.</param>
        private void RegisterBllComponents( ContainerBuilder builder )

        {
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
        }

        /// <summary>
        ///     Register MVC 
        /// </summary>
        /// <param name="builder">The builder through which components can be registered.</param>
        private void RegisterMVC( ContainerBuilder builder )
        {
            // Register dependencies in controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register dependencies in filter attributes
            builder.RegisterFilterProvider();

            // Register dependencies in custom views
            builder.RegisterSource(new ViewRegistrationSource());
            
        }
           

        /// <summary>
        ///     Registers the data access components.
        /// </summary>
        /// <param name="builder">The builder through which components can be registered.</param>
        private void RegisterDataAccessComponents( ContainerBuilder builder )
        {
            builder.RegisterType<DbFactory<IntranetContext>>()
                   .As<IDatabaseFactory<IntranetContext>>()
                   .PropertiesAutowired()
                   .InstancePerLifetimeScope();

            builder.RegisterType<DbCommit<IntranetContext>>()
                   .As<IDbCommit<IntranetContext>>()
                   .PropertiesAutowired()
                   .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes( typeof(IntranetContext).Assembly )
                   .Where( t => t.Name.EndsWith( "Repository", StringComparison.Ordinal ) )
                   .AsImplementedInterfaces()
                   .PropertiesAutowired()
                   .InstancePerLifetimeScope();
        }

        /// <summary>
        ///     Registers the logging components.
        /// </summary>
        /// <param name="builder">The builder through which components can be registered.</param>
        private void RegisterLoggingComponents( ContainerBuilder builder )
            => builder.RegisterType<NLogLoggerFactory>()
                      .As<ILoggerFactory>()
                      .PropertiesAutowired()
                      .SingleInstance();


    }
}