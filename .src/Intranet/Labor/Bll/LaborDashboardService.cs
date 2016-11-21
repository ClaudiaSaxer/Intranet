using System.Collections.Generic;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Model;
using Intranet.Labor.ViewModel;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing labor dashboard service
    /// </summary>
    public class LaborDashboardService : ServiceBase, ILaborDashboardService
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the bll for the labor dashboard service
        /// </summary>
        /// <value>the bll</value>
        public ILaborDashboardBll LaborDashboardBll { get; set; }

        /// <summary>
        ///     Gets or sets the helper for the labor dashboard
        /// </summary>
        /// <value>the labor dashboard helper</value>
        public ILaborDashboardHelper LaborDashboardHelper { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public LaborDashboardService( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(LaborDashboardService) ) )
        {
        }

        #endregion

        #region Implementation of ILaborDashboardService

        /// <summary>
        ///     Labor creator view model
        /// </summary>
        /// <returns>the LaborDashboardViewModel</returns>
        public LaborDashboardViewModel GetLaborDashboardViewModel()
        {
            var testsheets = LaborDashboardBll.GetTestSheetForActualAndLastThreeShifts();

            var items = new List<DashboardItem>();

            foreach ( var testSheet in testsheets )
            {
                var sheet = items.Find( item => item.MachineName.Equals( testSheet.MachineNr ) );
                if ( sheet == null )
                {
                    sheet = new DashboardItem
                    {
                        MachineName = testSheet.MachineNr,
                        ShiftItems =
                            new List<ShiftItem>
                            {
                                new ShiftItem { ProductionOrderItems = new List<ProductionOrderItem>() },
                                new ShiftItem { ProductionOrderItems = new List<ProductionOrderItem>() },
                                new ShiftItem { ProductionOrderItems = new List<ProductionOrderItem>() },
                                new ShiftItem { ProductionOrderItems = new List<ProductionOrderItem>() }
                            }
                    };
                    items.Add( sheet );
                }
                sheet.ShiftItems.ToList()[0].ProductionOrderItems.Add( new ProductionOrderItem
                                                                       {
                                                                           SheetId = testSheet.TestSheetId,
                                                                           HasNotes = testSheet.TestValues.ToList()
                                                                                               .Exists( value => value.TestValueNote.Count > 0 ),
                                                                           Notes = LaborDashboardHelper.ToDashboardNote( testSheet.TestValues ),
                                                                           DashboardInfos = LaborDashboardHelper.ToDashboardInfos( testSheet.TestValues ),
                                                                           ProductionOrderName = testSheet.FaNr,
                                                                           RwType = LaborDashboardHelper.ToRwTypeAll( testSheet.TestValues.ToList() ),
                                                                           Action = "Edit",
                                                                           Controller = testSheet.ArticleType == ArticleType.BabyDiaper ? "LaborCreatorBaby" : "LaborCreatorInko"
                                                                       } );
            }
            return new LaborDashboardViewModel { DashboardItem = items };

            #endregion
        }
    }
}