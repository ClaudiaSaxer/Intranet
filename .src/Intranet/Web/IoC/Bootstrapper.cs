using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Intranet.Definition;

namespace Intranet.Web.IoC
{
    /// <summary>
    ///     Class initializing the IoC container for all project.
    /// </summary>
    public class Bootstrapper
    {
        #region Public Methods

        /// <summary>
        ///     Runs the bootstrapper.
        /// </summary>
        public IContainer Run()
            => ConfigurateAutofacContainer();

        #endregion

        #region Private Methods

        /// <summary>
        ///     Configures autofac.
        /// </summary>
        /// <remarks>
        ///     - Best Practices:
        ///     <a href="https://code.google.com/p/autofac/wiki/BestPractices#Use_Constructor_Injection_whenever_Possible"></a>
        ///     - Lifetime Primer: <a href="http://nblumhardt.com/2011/01/an-autofac-lifetime-primer/"></a>
        /// </remarks>
        private IContainer ConfigurateAutofacContainer()
        {
            var builder = new ContainerBuilder();

            addModule( builder );
            var container = builder.Build();

            Initialize( container );
            DependencyResolver.SetResolver( new AutofacDependencyResolver( container ) );

            return container;
        }

        /// <summary>
        ///     Initialize some components.
        /// </summary>
        private void Initialize( IContainer container )
        {
            var loggerFactory = container.Resolve<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger( typeof(Bootstrapper) );
            logger.Info( "IoC finished" );
        }

        /// <summary>
        ///     Reads the XML configuration file (Autofac.xml) and adds the defined registrations to the builder.
        /// </summary>
        /// <param name="builder">A <see cref="ContainerBuilder" /> used to register the types.</param>
        private void addModule( ContainerBuilder builder )
            => builder.RegisterModule( new DefaultModule() );

        #endregion
    }
}