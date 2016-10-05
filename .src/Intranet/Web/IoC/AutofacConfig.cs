using Autofac;

namespace Intranet.Web.IoC
{
    public static class AutofacConfig
    {
        #region Properties

        public static IContainer Container { get; set; }

        #endregion

        public static void ConfigureContainer()
        {
            var bootstrapper = new Bootstrapper();
            Container = bootstrapper.Run();
        }
    }
}