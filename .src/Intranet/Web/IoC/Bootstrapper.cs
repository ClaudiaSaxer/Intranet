using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web.Mvc;
using Autofac;
using Autofac.Configuration;
using Autofac.Integration.Mvc;
using Intranet.Definition;
using Microsoft.Extensions.Configuration;

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

        /// <summary>
        ///     Loads and composes the DLL-Assemblies from the Controller-Modules
        /// </summary>
        /// <param name="pluginPaths">Paths where the DLL-Assemblies form the Modules are</param>
        public static void Compose( IEnumerable<String> pluginPaths )
        {
            if ( _isLoaded )
                return;
            
            var catalog = new AggregateCatalog();

            foreach ( var directoryCatalog in pluginPaths.Select( plugin => new DirectoryCatalog(plugin) ) )
                catalog.Catalogs.Add(directoryCatalog);
            _compositionContainer = new CompositionContainer(catalog);
            _compositionContainer.ComposeParts();

            _isLoaded = true;
        }

        /// <summary>
        ///     Returns the Controller Instance of the Controller
        /// </summary>
        /// <param name="contractName">Name of the Controller + Module (e.g. Module1.Test)</param>
        /// <typeparam name="T">IController</typeparam>
        /// <returns></returns>
        public static T GetInstance<T>( String contractName = null )
        {
            var type = default(T);
            if (_compositionContainer == null) return type;

            if (!string.IsNullOrWhiteSpace(contractName))
                try
                {
                    type = _compositionContainer.GetExportedValue<T>(contractName);
                }
                catch
                {
                    return default(T);
                }
            else
                type = _compositionContainer.GetExportedValue<T>();

            return type;
        }

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

            ReadJsonConfig( builder );
            var container = builder.Build();
            DependencyResolver.SetResolver( new AutofacDependencyResolver( container ) );

            Initialize( container );

            return container;
        }

        /// <summary>
        ///     Initialize some components.
        /// </summary>
        /// <param name="container">todo: describe container parameter on Initialize</param>
        private void Initialize( IContainer container )
        {
            var loggerFactory = container.Resolve<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger( typeof(Bootstrapper) );
            logger.Info( "IoC finished" );
        }

        /// <summary>
        ///     Reads the Json configuration file (Autofac.json) and adds the defined registrations to the builder.
        /// </summary>
        /// <param name="builder">A <see cref="ContainerBuilder" /> used to register the types.</param>
        private void ReadJsonConfig( ContainerBuilder builder )
        {
            // Add the configuration to the ConfigurationBuilder.
            var config = new ConfigurationBuilder();
            config.AddJsonFile( "autofac.json" );

            // Register the ConfigurationModule with Autofac.
            var module = new ConfigurationModule( config.Build() );
            builder.RegisterModule( module );
        }

        #endregion

        #region Fields

        private static CompositionContainer _compositionContainer;
        private static Boolean _isLoaded = false;

        #endregion

    }
}