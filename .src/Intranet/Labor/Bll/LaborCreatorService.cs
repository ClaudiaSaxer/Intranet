#region Usings

using System;
using System.Collections.Generic;
using Extend;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model;
using Intranet.Labor.ViewModel;

#endregion

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class representing labor creator service
    /// </summary>
    public class LaborCreatorService : ServiceBase, ILaborCreatorService
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the bll for the labor creator service
        /// </summary>
        /// <value>the bll</value>
        public ILaborCreatorBll LaborCreatorBll { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public LaborCreatorService( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(LaborCreatorService) ) )
        {
        }

        #endregion

        /// <summary>
        ///     Labor creator view model
        /// </summary>
        /// <returns>the LaborCreatorViewModel</returns>
        public LaborCreatorViewModel GetLaborCreatorViewModel()
        {
            var productionOrders = new List<RunningProductionOrder>();

            var runningTestSheets = LaborCreatorBll.RunningTestSheets();

            if ( runningTestSheets != null )
                LaborCreatorBll
                    .RunningTestSheets()
                    .ForEach( x => productionOrders.Add(
                                  new RunningProductionOrder
                                  {
                                      PoId = x.TestSheetId,
                                      PoName = x.FaNr,
                                      Description = x.ArticleType.ToString(),
                                      AreaName = "Labor",
                                      ControllerName =
                                          x.ArticleType == ArticleType.BabyDiaper
                                              ? "LaborCreatorBaby"
                                              : "LaborCreatorInko",
                                      ActionName = "Edit"
                                  } ) );

            return new LaborCreatorViewModel
            {
                ProductionOrders = productionOrders
            };
        }

        /// <summary>
        ///     Gets the test sheet id for the fa nr
        /// </summary>
        /// <param name="faNr">the production order number</param>
        /// <returns>the id of the testsheet or null if no exists</returns>
        public TestSheet GetTestSheetId( String faNr )
        {
            var testsheet = LaborCreatorBll.GetTestSheetForFaNr( faNr ) ?? LaborCreatorBll.InitTestSheetForFaNr( faNr );

            return testsheet;
        }
    }
}