﻿using System;
using System.Web.Mvc;
using Extend;
using Intranet.Common;
using Intranet.Labor.Definition;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing the controller for the labor creator
    /// </summary>
    public class LaborCreatorController : BaseController
    {
        #region Properties

        /// <summary>
        ///     Gets or sets a <see cref="ILaborHomeService" />
        /// </summary>
        /// <value>
        ///     <see cref="ILaborHomeService" />
        /// </value>
        public ILaborCreatorService LaborCreatorService { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="LaborCreatorController" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public LaborCreatorController( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(LaborCreatorController) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        /// <summary>
        ///     GET: Labor/LaborCreator/5
        /// </summary>
        /// <param name="id">the id of the testsheet</param>
        /// <returns>the actionresult to edit a existing testsheet</returns>
        public ActionResult Edit( Int32 id = 0)
        {
            if(id.IsNull())
                return HttpNotFound();

            var laborCreatorView = LaborCreatorService.GetLaborCreatorViewModel( id );

            if ( laborCreatorView == null )
                return HttpNotFound();
            return View( laborCreatorView );
        }
    }
}