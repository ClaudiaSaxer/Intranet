using Autofac;

namespace Intranet.Web.IoC
{
    /// <summary>
    ///     Configuration File for Autofac
    /// </summary>
    public static class AutofacConfig
    {
        #region Properties

        /// <summary>
        ///     Gets and sets the container for autofac
        /// </summary>
        public static IContainer Container { get; set; }

        #endregion

        /// <summary>
        ///     Configurates the Autofac Container
        /// </summary>
        public static void ConfigureContainer()
        {
            var bootstrapper = new Bootstrapper();
            Container = bootstrapper.Run();
        }
    }
}