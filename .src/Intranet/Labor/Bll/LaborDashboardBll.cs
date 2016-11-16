using Intranet.Common;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing labor dashboard bll
    /// </summary>
    public class LaborDashboardBll : ServiceBase, ILaborDashboardBll
    {
        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public LaborDashboardBll( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(LaborDashboardBll) ) )
        {
        }

        #endregion
    }
}