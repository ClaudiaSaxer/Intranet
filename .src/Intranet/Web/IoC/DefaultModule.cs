﻿#region Usings

using System;
using Autofac;
using Autofac.Integration.Mvc;
using Intranet.Bll;
using Intranet.Common;
using Intranet.Dal;
using Intranet.Definition;
using Intranet.Labor.Bll;
using Intranet.Labor.Dal;
using Intranet.Labor.Definition;

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
            RegisterRolesComponents( builder );
            RegisterDataAccessComponents( builder );
            RegisterBllComponents( builder );
            RegisterMvc( builder );
        }

        /// <summary>
        ///     Registers the BLL components.
        /// </summary>
        /// <param name="builder">The builder through which components can be registered.</param>
        private static void RegisterBllComponents( ContainerBuilder builder )
        {
            /* => builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                      .Where(
                          t =>
                              (t.Namespace != null) && t.IsClass && t.Namespace.Contains("Bll")
                              && (t.GetInterfaces()
                                    .Length != 0))
                      .As(t => t.GetInterfaces()
                                .Single(i => i.Name == "I" + t.Name))
                      .PropertiesAutowired()
                      .InstancePerRequest();*/

            builder.RegisterType<HomeBll>()
                   .As<IHomeBll>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<HomeService>()
                   .As<IHomeService>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<SettingsBll>()
                   .As<ISettingsBll>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<SettingsService>()
                   .As<ISettingsService>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<NavigationBll>()
                   .As<INavigationBll>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<NavigationService>()
                   .As<INavigationService>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<BabyDiaperLaborHomeBll>()
                   .As<ILaborHome>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<LaborHomeService>()
                   .As<ILaborHomeService>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<BabyDiaperRetentionService>()
                   .As<IBabyDiaperRetentionService>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<TestBll>()
                   .As<ITestBll>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<BabyDiaperLaborCreatorBll>()
                   .As<IBabyDiaperLaborCreatorBll>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<BabyDiaperLaborCreatorService>()
                   .As<IBabyDiaperLaborCreatorService>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<BabyDiaperLaborCreatorServiceHelper>()
                   .As<IBabyDiaperLaborCreatorServiceHelper>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<TestServiceHelper>()
                   .As<ITestServiceHelper>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<LaborCreatorService>()
                   .As<ILaborCreatorService>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<LaborCreatorBll>()
                   .As<ILaborCreatorBll>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<BabyDiaperRewetService>()
                   .As<IBabyDiaperRewetService>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<BabyDiaperRetentionServiceHelper>()
                   .As<IBabyDiaperRetentionServiceHelper>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<BabyDiaperRewetServiceHelper>()
                   .As<IBabyDiaperRewetServiceHelper>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<IncontinencePadLaborCreatorBll>()
                   .As<IIncontinencePadLaborCreatorBll>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<IncontinencePadLaborCreatorService>()
                   .As<IIncontinencePadLaborCreatorService>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<IncontinencePadLaborCreatorServiceHelper>()
                   .As<IIncontinencePadLaborCreatorServiceHelper>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<InkoRewetService>()
                   .As<IInkoRewetService>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<InkoRewetServiceHelper>()
                   .As<IInkoRewetServiceHelper>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<InkoRetentionService>()
                   .As<IInkoRetentionService>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<InkoRetentionServiceHelper>()
                   .As<IInkoRetentionServiceHelper>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<LaborCreatorServiceHelper>()
                   .As<ILaborCreatorServiceHelper>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<InkoAquisitionService>()
                   .As<IInkoAquisitionService>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<InkoAquisitionServiceHelper>()
                   .As<IInkoAquisitionServiceHelper>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<CheckDisableService>()
                   .As<ICheckDisableService>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<CheckDisableBll>()
                   .As<ICheckDisableBll>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<LaborDashboardService>()
                   .As<ILaborDashboardService>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<LaborDashboardBll>()
                   .As<ILaborDashboardBll>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<LaborDashboardHelper>()
                   .As<ILaborDashboardHelper>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<ShiftHelper>()
                   .As<IShiftHelper>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<HistoryBll>()
                   .As<IHistoryBll>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<HistoryService>()
                   .As<IHistoryService>()
                   .PropertiesAutowired()
                   .InstancePerRequest();
        }

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

            builder.RegisterType<DbFactory<LaborContext>>()
                   .As<IDatabaseFactory<LaborContext>>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterType<DbCommit<LaborContext>>()
                   .As<IDbCommit<LaborContext>>()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterAssemblyTypes( typeof(ModuleRepository).Assembly )
                   .Where( t => t.Name.EndsWith( "Repository", StringComparison.Ordinal ) )
                   .AsImplementedInterfaces()
                   .PropertiesAutowired()
                   .InstancePerRequest();

            builder.RegisterAssemblyTypes( typeof(IncontinencePadTestValueRepository).Assembly )
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

        /// <summary>
        ///     Registers the roles components.
        /// </summary>
        /// <param name="builder">The builder through which components can be registered.</param>
        private static void RegisterRolesComponents( ContainerBuilder builder )
            => builder.RegisterType<SystemWebSecurityRoles>()
                      .As<IRoles>()
                      .PropertiesAutowired()
                      .InstancePerRequest();
    }
}