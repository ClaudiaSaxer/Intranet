using Autofac;
using Intranet.Bll;

namespace Intranet.Definition.Autofac
{
    internal class Registration
    {
        public void Register()
        {
            // Create the builder with which components/services are registered.
            var builder = new ContainerBuilder();

            // Register types that expose interfaces...
            builder.RegisterType<TestAutofac>()
                   .As<ITestAutofac>()
                   .PropertiesAutowired();

            builder.RegisterType<TestUseAutofac>()
                   .AsSelf()
                   .PropertiesAutowired();

            /*
            // Register instances of objects you create...
            var output = new StringWriter();
            builder.RegisterInstance( output )
                   .As<TextWriter>();

            // Register expressions that execute to create objects...
            builder.Register( c => new ConfigReader( "mysection" ) )
                   .As<IConfigReader>();

            // Build the container to finalize registrations
            // and prepare for object resolution.
            var container = builder.Build();

            // Now you can resolve services using Autofac. For example,
            // this line will execute the lambda expression registered
            // to the IConfigReader service.
            using ( var scope = container.BeginLifetimeScope() )
            {
                var reader = container.Resolve<IConfigReader>();
            }
            */
        }
    }
}