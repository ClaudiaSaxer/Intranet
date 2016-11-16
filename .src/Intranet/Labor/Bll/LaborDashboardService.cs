using System.Collections.Generic;
using Intranet.Common;
using Intranet.Labor.Model.labor;
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
            //TODO implement and remove test data
            return new LaborDashboardViewModel
                {
                    DashboardItem = new List<DashboardItem>
                    {
                        new DashboardItem
                        {
                            MachineName = "M10",
                            ShiftItems =
                                new List<ShiftItem>
                                {
                                    new ShiftItem
                                    {
                                        ProductionOrderItems =
                                            new List<ProductionOrderItem>
                                            {
                                                new ProductionOrderItem { HasNotes = true, RwType = RwType.Ok, ProductionOrderName = "FA1234" },
                                                new ProductionOrderItem { HasNotes = false, RwType = RwType.Worse, ProductionOrderName = "FA45" },
                                                new ProductionOrderItem { HasNotes = false, RwType = RwType.Better, ProductionOrderName = "FA122" },
                                                new ProductionOrderItem { HasNotes = true, RwType = RwType.SomethingWorse, ProductionOrderName = "FA666" }
                                            }
                                    },
                                    new ShiftItem
                                    {
                                        ProductionOrderItems =
                                            new List<ProductionOrderItem>
                                            {
                                                new ProductionOrderItem { HasNotes = false, RwType = RwType.Ok, ProductionOrderName = "FA1234" },
                                                new ProductionOrderItem { HasNotes = true, RwType = RwType.Ok, ProductionOrderName = "FA122" }
                                            }
                                    },
                                    new ShiftItem
                                    {
                                        ProductionOrderItems =
                                            new List<ProductionOrderItem>
                                            {
                                                new ProductionOrderItem { HasNotes = false, RwType = RwType.Ok, ProductionOrderName = "FA1234" },
                                                new ProductionOrderItem { HasNotes = true, RwType = RwType.Ok, ProductionOrderName = "FA122" }
                                            }
                                    },
                                    new ShiftItem
                                    {
                                        ProductionOrderItems =
                                            new List<ProductionOrderItem>
                                            {
                                                new ProductionOrderItem
                                                {
                                                    HasNotes = true,
                                                    RwType = RwType.Ok,
                                                    ProductionOrderName = "FA122"
                                                }
                                            }
                                    }
                                }
                        },
                        new DashboardItem
                        {
                            MachineName = "M11",
                            ShiftItems =
                                new List<ShiftItem>
                                {
                                    new ShiftItem { ProductionOrderItems = new List<ProductionOrderItem>() },
                                    new ShiftItem { ProductionOrderItems = new List<ProductionOrderItem>() },
                                    new ShiftItem { ProductionOrderItems = new List<ProductionOrderItem>() },
                                    new ShiftItem { ProductionOrderItems = new List<ProductionOrderItem>() }
                                }
                        },
                        new DashboardItem
                        {
                            MachineName = "M45",
                            ShiftItems =
                                new List<ShiftItem>
                                {
                                    new ShiftItem { ProductionOrderItems = new List<ProductionOrderItem>() },
                                    new ShiftItem { ProductionOrderItems = new List<ProductionOrderItem>() },
                                    new ShiftItem { ProductionOrderItems = new List<ProductionOrderItem>() },
                                    new ShiftItem { ProductionOrderItems = new List<ProductionOrderItem>() }
                                }
                        }
                    }
                }
                ;

            #endregion
        }
    }
}