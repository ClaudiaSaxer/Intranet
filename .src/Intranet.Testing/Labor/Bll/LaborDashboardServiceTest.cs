#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Intranet.Common;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.TestEnvironment;
using Intranet.Labor.ViewModel;
using Intranet.Labor.ViewModel.LaborDashboard;
using Intranet.Web.Areas.Labor.Controllers;
using Xunit;

#endregion

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Test Class for Labor Dashboard Service
    /// </summary>
    public class LaborDashboardServiceTest
    {
        /// <summary>
        ///     Normal Passing Test with no value
        /// </summary>
        [Fact]
        public void GetLaborDashboardViewModelEmptyTest()
        {
            var labordashboardbllmock =
                MockHelperBll.GetLaborDashboardBll(
                    new List<TestSheet> { new TestSheet { MachineNr = "M10" } }
                );
            var shifthelpermock =
                MockHelperBll.GetShiftHelper(
                    lastXShiftSchedules: new List<ShiftSchedule>
                    {
                        new ShiftSchedule(),
                        new ShiftSchedule(),
                        new ShiftSchedule(),
                        new ShiftSchedule()
                    }
                );
            var labordashboardhelpermock =
                MockHelperLaborDashboardHelper.GetLaborDashboardHelper(
                    new List<DashboardNote>(),
                    productionOrderItems: new List<ProductionOrderItem>()
                );

            var target = new LaborDashboardService( new NLogLoggerFactory() )
            {
                LaborDashboardBll = labordashboardbllmock,
                LaborDashboardHelper = labordashboardhelpermock,
                ShiftHelper = shifthelpermock
            };

            var actual = target.GetLaborDashboardViewModel();

            actual.DashboardItemM10.MachineName.Should()
                  .Be( "M10" );
            actual.DashboardItemM10.ShiftItemsCurrent.Count.Should()
                  .Be( 0 );
            actual.DashboardItemM10.ShiftItemsMinus1.Count.Should()
                  .Be( 0 );
            actual.DashboardItemM10.ShiftItemsMinus2.Count.Should()
                  .Be( 0 );
            actual.DashboardItemM10.ShiftItemsMinus3.Count.Should()
                  .Be( 0 );
            actual.DashboardItemM11.MachineName.Should()
                  .Be( "M11" );
            actual.DashboardItemM11.ShiftItemsCurrent.Count.Should()
                  .Be( 0 );
            actual.DashboardItemM11.ShiftItemsMinus1.Count.Should()
                  .Be( 0 );
            actual.DashboardItemM11.ShiftItemsMinus2.Count.Should()
                  .Be( 0 );
            actual.DashboardItemM11.ShiftItemsMinus3.Count.Should()
                  .Be( 0 );
            actual.DashboardItemM49.MachineName.Should()
                  .Be( "M49" );
            actual.DashboardItemM49.ShiftItemsCurrent.Count.Should()
                  .Be( 0 );
            actual.DashboardItemM49.ShiftItemsMinus1.Count.Should()
                  .Be( 0 );
            actual.DashboardItemM49.ShiftItemsMinus2.Count.Should()
                  .Be( 0 );
            actual.DashboardItemM49.ShiftItemsMinus3.Count.Should()
                  .Be( 0 );
        }

        /// <summary>
        ///     Normal Passing Test with one Item
        /// </summary>
        [Fact]
        public void GetLaborDashboardViewModelSingleTest()
        {
            var now = DateTime.Now;
            var note = new DashboardNote
            {
                ErrorMessage = "en fehler",
                Message = "chaibe siäch, eifach abgstellt hetter",
                Code = "666"
            };
            var info = new DashboardInfo
            {
                RwType = RwType.Better,
                InfoValue = "666",
                InfoKey = "the number of the beast"
            };
            var po = new ProductionOrderItem
            {
                RwType = RwType.Better,
                Action = "action",
                HasNotes = true,
                Notes = new List<DashboardNote> { note },
                ProductionOrderName = "666",
                Controller = "controller",
                SheetId = 12,
                DashboardInfos = new List<DashboardInfo> { info }
            };

            var labordashboardbllmock =
                MockHelperBll.GetLaborDashboardBll(
                    new List<TestSheet> { new TestSheet { MachineNr = "M10",CreatedDateTime = now} }
                );
            var labordashboardhelpermock =
                MockHelperLaborDashboardHelper.GetLaborDashboardHelper(
                    new List<DashboardNote> { note },
                    RwType.Better,
                    new List<DashboardInfo> { info },
                    po,
                    new List<ProductionOrderItem> { po }
                );
            var shifthelpermock =
                MockHelperBll.GetShiftHelper(
                    lastXShiftSchedules:
                    new List<ShiftSchedule>
                    {
                        new ShiftSchedule {ShiftType = ShiftType.Morning, StartDay = now.DayOfWeek, EndDay = now.DayOfWeek, StartTime = now.TimeOfDay, EndTime = now.TimeOfDay },
                        new ShiftSchedule(),
                        new ShiftSchedule(),
                        new ShiftSchedule()
                    }
                    ,shiftType: ShiftType.Morning
                    ,shiftScheduleCurrent: new ShiftSchedule { ShiftType = ShiftType.Morning, StartDay = now.DayOfWeek, EndDay = now.DayOfWeek, StartTime = now.TimeOfDay, EndTime = now.TimeOfDay }
                    , dateExistsInShifts:time => time.Equals( now )
                );
            var target = new LaborDashboardService( new NLogLoggerFactory() )
            {
                LaborDashboardBll = labordashboardbllmock,
                LaborDashboardHelper = labordashboardhelpermock,
                ShiftHelper = shifthelpermock
            };

            var actual = target.GetLaborDashboardViewModel();

            actual.DashboardItemM10.MachineName.Should()
                  .Be( "M10" );
            actual.DashboardItemM11.MachineName.Should()
                  .Be( "M11" );
            actual.DashboardItemM49.MachineName.Should()
                  .Be( "M49" );
            actual.DashboardItemM10.ShiftItemsCurrent.Count.Should()
                  .Be( 1 );
            actual.DashboardItemM10.ShiftItemsMinus1.Count.Should()
                  .Be( 0 );
            actual.DashboardItemM10.ShiftItemsMinus2.Count.Should()
                  .Be( 0 );
            actual.DashboardItemM10.ShiftItemsMinus3.Count.Should()
                  .Be( 0 );

            actual.DashboardItemM11.ShiftItemsCurrent.Count.Should()
                  .Be( 0 );
            actual.DashboardItemM11.ShiftItemsMinus1.Count.Should()
                  .Be( 0);
            actual.DashboardItemM11.ShiftItemsMinus2.Count.Should()
                  .Be( 0 );
            actual.DashboardItemM11.ShiftItemsMinus3.Count.Should()
                  .Be( 0 );

            actual.DashboardItemM49.ShiftItemsCurrent.Count.Should()
                  .Be( 0 );
            actual.DashboardItemM49.ShiftItemsMinus1.Count.Should()
                  .Be( 0 );
            actual.DashboardItemM49.ShiftItemsMinus2.Count.Should()
                  .Be( 0 );
            actual.DashboardItemM49.ShiftItemsMinus3.Count.Should()
                  .Be( 0 );

            actual.DashboardItemM10.ShiftItemsCurrent.ToList()[0].RwType.Should()
                  .Be( po.RwType );
            actual.DashboardItemM10.ShiftItemsCurrent.ToList()[0].DashboardInfos.Count.Should()
                  .Be( 1 );
            actual.DashboardItemM10.ShiftItemsCurrent.ToList()[0].DashboardInfos.ToList()[0].Should()
                  .Be( info );
            actual.DashboardItemM10.ShiftItemsCurrent.ToList()[0].SheetId.Should()
                  .Be( po.SheetId );
            actual.DashboardItemM10.ShiftItemsCurrent.ToList()[0].Action.Should()
                  .Be( po.Action );
            actual.DashboardItemM10.ShiftItemsCurrent.ToList()[0].Controller.Should()
                  .Be( po.Controller );
            actual.DashboardItemM10.ShiftItemsCurrent.ToList()[0].HasNotes.Should()
                  .Be( po.HasNotes );
            actual.DashboardItemM10.ShiftItemsCurrent.ToList()[0].ProductionOrderName.Should()
                  .Be( po.ProductionOrderName );
            actual.DashboardItemM10.ShiftItemsCurrent.ToList()[0].Notes.Count.Should()
                  .Be( 1 );
            actual.DashboardItemM10.ShiftItemsCurrent.ToList()[0].Notes.ToList()[0].Should()
                  .Be( note );
        }
    }
}