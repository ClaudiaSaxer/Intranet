using System;
using System.Web.Mvc;
using System.Web.Routing;
using Extend;
using Intranet.Common;
using Intranet.Labor.Definition;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing the controller for the labor creator
    /// </summary>
    public class LaborCreatorBabyController : BaseController
    {
        #region Properties

        /// <summary>
        ///     Gets or sets a <see cref="IBabyDiaperLaborCreatorService" />
        /// </summary>
        /// <value>
        ///     <see cref="IBabyDiaperLaborCreatorService" />
        /// </value>
        public IBabyDiaperLaborCreatorService BabyDiaperLaborCreatorService { get; set; }

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
        ///     Initialize a new instance of the <see cref="LaborCreatorBabyController" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public LaborCreatorBabyController( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(LaborCreatorBabyController) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        /// <summary>
        ///     GET: Labor/LaborCreatorBaby/5
        /// </summary>
        /// <param name="id">the id of the testsheet</param>
        /// <returns>the actionresult to edit a existing testsheet</returns>
        public ActionResult Edit( Int32 id = 0 )
        {
            if ( id.IsNull() )
                return RedirectToAction("Index", "LaborCreator", new { area = "Labor"});


            try
            {
                var laborCreatorView = BabyDiaperLaborCreatorService.GetLaborCreatorViewModel( id );

                if ( laborCreatorView == null )
                    return RedirectToAction("Index", "LaborCreator", new { area = "Labor" });

         
                return View( laborCreatorView );
            }
            catch ( Exception e )
            {
                Logger.Error( e.StackTrace );

                return new HttpNotFoundResult( "Bitte wenden Sie sich an den Administrator." );
            }
        }
    }
}