using Intranet.Web.IoC;

namespace Intranet.Web
{
    /// <summary>
    /// </summary>
    public class AutofacConfig
    {
        /// <summary>
        /// </summary>
        public static void ConfigureContainer()
        {
            var b = new Bootstrapper();
            Bootstrapper.Run();
        }
    }
}