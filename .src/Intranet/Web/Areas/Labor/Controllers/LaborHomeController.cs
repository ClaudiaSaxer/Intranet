﻿#region Usings

using System.Web.Mvc;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Web.Filter;

#endregion

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing Labor Home Controller
    /// </summary>
    [CheckDisable( ModuleName = "Labor" )]
    [Authorize( Roles =
         RoleSettings.LaborAdmin + "," +
         RoleSettings.LaborUser + "," +
         RoleSettings.LaborViewer )]
    public class LaborHomeController : BaseController
    {
        #region Properties

        /// <summary>
        ///     Gets or sets a <see cref="ILaborHomeService" />
        /// </summary>
        /// <value>
        ///     <see cref="ILaborHomeService" />
        /// </value>
        public ILaborHomeService LaborHomeService { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="LaborHomeController" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public LaborHomeController( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(LaborHomeController) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        /// <summary>
        ///     Loads the index page of the LaborHomeController
        /// </summary>
        /// <returns>The Index View filled with the viewModel</returns>
        public ActionResult Index() => View( "Index", LaborHomeService.GetLaborHomeViewModel() );
    }
}