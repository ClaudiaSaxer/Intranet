#region Usings

using Intranet.Web.IoC;

#endregion

namespace Intranet.Web
{
    /// <summary>
    /// Autofac IoC configuration.
    /// </summary>
    public class AutofacConfig
    {
        /// <summary>
        /// Starts the IoC bootstrapper.
        /// </summary>
        public static void ConfigureContainer() 
            => Bootstrapper.Run();
    }
}