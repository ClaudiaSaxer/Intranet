#region Usings

using System;
using System.Linq;
using Autofac;
using Autofac.Integration.Mvc;
using Intranet.Common.Db;
using Intranet.Common.Logging;
using Intranet.Dal;
using Intranet.Dal.Repositories;
using Intranet.Definition;

#endregion

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
            RegisterMvc( builder );
        }

        /// <summary>
        ///     Registers the BLL components.
        /// </summary>
        /// <param name="builder">The builder through which components can be registered.</param>
        private static void RegisterBllComponents( ContainerBuilder builder ) => builder.RegisterAssemblyTypes( AppDomain.CurrentDomain.GetAssemblies() )
                                                                                        .Where(
                                                                                            t =>
                                                                                                ( t.Namespace != null ) && t.IsClass && t.Namespace.Contains( "Bll" )
                                                                                                && ( t.GetInterfaces()
                                                                                                      .Length != 0 ) )
                                                                                        .As( t => t.GetInterfaces()
                                                                                                   .Single( i => i.Name == "I" + t.Name ) )
                                                                                        .PropertiesAutowired()
                                                                                        .InstancePerRequest();

        /// <summary>
        ///     Registers the data access components.
        /// </summary>
        /// <param name="builder">The builder through which components can be registered.</param>
        private static void RegisterDataAccessComponents( ContainerBuilder builder )
        {
            builder.RegisterType<DbFactory<IntranetContext>>()
                   .As<IDatabaseFactory<IntranetContext>>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<DbCommit<IntranetContext>>()
                   .As<IDbCommit<IntranetContext>>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterAssemblyTypes( typeof(MainModuleRepository).Assembly )
                   .Where( t => t.Name.EndsWith( "Repository", StringComparison.Ordinal ) )
                   .AsImplementedInterfaces()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterAssemblyTypes( typeof(SubModuleRepository).Assembly )
                   .Where( t => t.Name.EndsWith( "Repository", StringComparison.Ordinal ) )
                   .AsImplementedInterfaces()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterAssemblyTypes( typeof(RoleRepository).Assembly )
                   .Where( t => t.Name.EndsWith( "Repository", StringComparison.Ordinal ) )
                   .AsImplementedInterfaces()
                   .PropertiesAutowired()
                   .InstancePerRequest();

        }

        /// <summary>
        ///     Registers the logging components.
        /// </summary>
        /// <param name="builder">The builder through which components can be registered.</param>
        private static void RegisterLoggingComponents( ContainerBuilder builder )
            => builder.RegisterType<NLogLoggerFactory>()
                      .As<ILoggerFactory>()
                      .PropertiesAutowired()
                      .SingleInstance();

        /// <summary>
        ///     Register MVC
        /// </summary>
        /// <param name="builder">The builder through which components can be registered.</param>
        private static void RegisterMvc( ContainerBuilder builder )
        {
            // Register dependencies in controllers
            builder.RegisterControllers( typeof(MvcApplication).Assembly )
                   .PropertiesAutowired();

            // Register dependencies in filter attributes
            builder.RegisterFilterProvider();

            // Register dependencies in custom views
            //builder.RegisterSource( new ViewRegistrationSource() );
        }
    }
}