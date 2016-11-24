using System;
using System.Web.Mvc;
using System.Web.Routing;
using Intranet.Common;
using Intranet.Common.Role;
using Intranet.Labor.Model;
using Intranet.Labor.ViewModel;
using Intranet.Web.Filter;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing Labor Creator
    /// </summary>
    [CheckDisable(ModuleName = "Labor")]
    [Authorize(Roles = RoleSettings.LaborAdmin + "," + RoleSettings.LaborUser)]
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
        ///     Edit Creator with given Production order Number
        /// </summary>
        /// <param name="vm">the viewmodel with the chodsen production number</param>
        /// <returns>the action result for edit</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( [Bind( Include = "ChosenPo" )] LaborCreatorViewModel vm )
        {
            var testSheet = LaborCreatorService.GetTestSheetId( vm.ChosenPo );
            if ( testSheet == null )
                return Home( "Fertigungsnummer wurde nicht gefunden." );

            var controllerForType = testSheet.ArticleType == ArticleType.BabyDiaper
                ? "LaborCreatorBaby"
                : "LaborCreatorInko";

            return RedirectToAction( "Edit",
                                     new RouteValueDictionary(
                                         new
                                         {
                                             controller = controllerForType,
                                             action = "Edit",
                                             id = testSheet.TestSheetId
                                         } ) );
        }
        /// <summary>
        ///     Loads the index page of the LaborCreatorController
        /// </summary>
        /// <returns>The Index View filled with the viewModel</returns>
        public
            ActionResult Index()
            => View( "Index", LaborCreatorService.GetLaborCreatorViewModel() );

        /// <summary>
        ///     Loads the index page of the LaborCreatorController
        ///     with a user message
        /// </summary>
        /// <returns>The Index View filled with the viewModel</returns>
        public
          ActionResult Home( String message  = "")
        {
            var vm = LaborCreatorService.GetLaborCreatorViewModel();
            vm.Message = message;
            return View( "Index", vm );
        }
    }
}