#region Usings

using System.Collections.Generic;
using System.Web.Mvc;
using Intranet.Common;
using Intranet.Common.Role;
using Intranet.Labor.Definition;
using Intranet.Labor.ViewModel;
using Intranet.Web.Filter;

#endregion

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing the InkoAquisitionController
    /// </summary>
    [CheckDisable( ModuleName = "Labor" )]
    [Authorize( Roles =
         RoleSettings.LaborAdmin + "," +
         RoleSettings.LaborUser + "," +
         RoleSettings.LaborViewer )]
    public class HistoryController : BaseController
    {
        #region Properties

        /// <summary>
        ///     Gets or sets a <see cref="IHistoryService" />
        /// </summary>
        /// <value>
        ///     <see cref="IHistoryService" />
        /// </value>
        public IHistoryService HistoryService { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="HistoryController" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public HistoryController( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(HistoryController) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        /// <summary>
        ///     Gets the History Site to search for older TestSheets
        /// </summary>
        /// <returns>history view</returns>
        public ActionResult Index()
        {
            var viewModel = new HistoryViewModel
            {
                Sheets = new List<HistoryItem>()
            };
            return View( "Search", viewModel );
        }

        /// <summary>
        ///     Search for all Testsheets with the FaNr and returns them
        /// </summary>
        /// <param name="viewModel">the viewmodel which contains the FaNr</param>
        /// <returns>View Listed with a history list</returns>
        [HttpPost]
        public ActionResult Search( HistoryViewModel viewModel )
        {
            viewModel = HistoryService.GetHistoryViewModel( viewModel.FaNr );
            return View( "Search", viewModel );
        }
    }
}