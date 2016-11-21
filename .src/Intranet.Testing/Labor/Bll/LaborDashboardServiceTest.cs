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
                    new List<TestSheet>()
                );
            var labordashboardhelpermock =
                MockHelperLaborDashboardHelper.GetLaborDashboardHelper(
                    new List<DashboardNote>()
                );

            var target = new LaborDashboardService( new NLogLoggerFactory() )
            {
                LaborDashboardBll = labordashboardbllmock,
                LaborDashboardHelper = labordashboardhelpermock
            };

            var actual = target.GetLaborDashboardViewModel();

            actual.DashboardItems.Count.Should()
                  .Be( 0 );
        }

        /// <summary>
        ///     Normal Passing Test with one Item
        /// </summary>
        [Fact]
        public void GetLaborDashboardViewModelSingleTest()
        {
            var now = DateTime.Now;
            var testsheet = new TestSheet
            {
                ShiftType = ShiftType.Late,
                ArticleType = ArticleType.BabyDiaper,
                TestValues = new List<TestValue>(),
                FaNr = "666",
                CreatedDateTime = now,
                MachineNr = "M12",
                SizeName = "big",
                TestSheetId = 1,
                DayInYear = 123,
                ProductName = "awesome",
                SAPType = "W43",
                SAPNr = "asdf"
            };
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
            var labordashboardbllmock =
                MockHelperBll.GetLaborDashboardBll(
                    new List<TestSheet> { testsheet }
                );
            var labordashboardhelpermock =
                MockHelperLaborDashboardHelper.GetLaborDashboardHelper(
                    new List<DashboardNote> { note },
                    RwType.Better,
                    new List<DashboardInfo> { info }
                );

            var target = new LaborDashboardService( new NLogLoggerFactory() )
            {
                LaborDashboardBll = labordashboardbllmock,
                LaborDashboardHelper = labordashboardhelpermock
            };

            var actual = target.GetLaborDashboardViewModel();

            actual.DashboardItems.Count.Should()
                  .Be( 1 );
            var actualItem = actual.DashboardItems.ToList()[0];
            actualItem.MachineName.Should()
                      .Be( testsheet.MachineNr );
            actualItem.ShiftItems.Count.Should()
                      .Be( 4 );
          
        }
    }
}