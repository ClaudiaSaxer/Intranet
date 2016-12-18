#region Usings

using Intranet.Web.IoC;

#endregion

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