using System.Web.Mvc;
using Intranet.Common;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing Labor Creator
    /// </summary>
    public class LaborCreatorController : BaseController
    {
        #region Properties

        /// <summary>
        ///     Gets or sets a <see cref="ILaborCreatorService" />
        /// </summary>
        /// <value>
        ///     <see cref="ILaborCreatorService" />
        /// </value>
        public ILaborCreatorService LaborCreatorService { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="BaseController" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public LaborCreatorController( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(LaborCreatorController) ) )
        {
        }

        #endregion

        /// <summary>
        ///     Loads the index page of the LaborCreatorController
        /// </summary>
        /// <returns>The Index View filled with the viewModel</returns>
        public ActionResult Index() => View( "Index", LaborCreatorService.GetLaborCreatorViewModel() );
    }
}