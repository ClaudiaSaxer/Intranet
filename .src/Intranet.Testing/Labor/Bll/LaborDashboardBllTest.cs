using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Intranet.Common;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.TestEnvironment;
using Intranet.Web.Areas.Labor.Controllers;
using Xunit;

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Test Class for Labor Dashboard Bll Test
    /// </summary>
    public class LaborDashboardBllTest
    {
        /// <summary>
        ///     Normal Passing Test with one matching module and role
        /// </summary>
        [Fact]
        public void GetTestSheetForActualAndLastThreeShiftsFoundTest()
        {
            var now = DateTime.Now;

            var testsheet = new TestSheet
            {
                FaNr = "666",
                ShiftType = ShiftType.Late,
                DayInYear = now.DayOfYear,
                CreatedDateTime = now
            };

            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Late,
                EndTime = new TimeSpan( now.Hour + 2, now.Minute + 10, now.Second ),
                StartTime = new TimeSpan( now.Hour - 2, now.Minute -10 , now.Second ),
                StartDay = now.DayOfWeek,
                EndDay = now.AddDays( 1 )
                            .DayOfWeek
            };

            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift,shift
            };
            var testSheetListQuery = new List<TestSheet>
            {
                testsheet
            };

            var testSheetRepository = MockHelperBll.TestSheetRepository( testSheetListQuery.AsQueryable() );
            var shifthelper = MockHelperBll.GetShiftHelper( ShiftType.Late, shift, lastXShiftSchedules: shiftSheduleListQuery  );

            var target = new LaborDashboardBll( new NLogLoggerFactory() )
            {
                ShiftHelper = shifthelper,
                TestSheets = testSheetRepository
            };

            var actual = target.GetTestSheetForActualAndLastThreeShifts();
            actual.Count.Should().Be( 1 );
        }

        /// <summary>
        ///     Normal Passing Test with one matching module and role but 2 existing in different weeks
        /// </summary>
        [Fact]
        public void GetTestSheetForActualAndLastThreeShiftsFoundTest2()
        {
            var now = DateTime.Now;

            var testsheet = new TestSheet
            {
                FaNr = "666",
                ShiftType = ShiftType.Late,
                DayInYear = now.DayOfYear,
                CreatedDateTime = now
            };


            var testsheet2 = new TestSheet
            {
                FaNr = "666",
                ShiftType = ShiftType.Late,
                DayInYear = now.DayOfYear,
                CreatedDateTime = now.AddDays( -7 )
            };

            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Late,
                EndTime = new TimeSpan(now.Hour + 2, now.Minute + 10, now.Second),
                StartTime = new TimeSpan(now.Hour - 2, now.Minute - 10, now.Second),
                StartDay = now.DayOfWeek,
                EndDay = now.AddDays(1)
                            .DayOfWeek
            };

            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift,shift
            };
            var testSheetListQuery = new List<TestSheet>
            {
                testsheet,testsheet2
            };

            var testSheetRepository = MockHelperBll.TestSheetRepository(testSheetListQuery.AsQueryable());
            var shifthelper = MockHelperBll.GetShiftHelper(ShiftType.Late, shift, lastXShiftSchedules: shiftSheduleListQuery);

            var target = new LaborDashboardBll(new NLogLoggerFactory())
            {
                ShiftHelper = shifthelper,
                TestSheets = testSheetRepository
            };

            var actual = target.GetTestSheetForActualAndLastThreeShifts();
            actual.Count.Should().Be(1);
        }



        /// <summary>
        ///     Normal Passing Test with one matching module and role
        /// </summary>
        [Fact]
        public void GetTestSheetForActualAndLastThreeShiftsNotFoundTest()
        {
            var now = DateTime.Now;

            var testsheet = new TestSheet
            {
                FaNr = "666",
                ShiftType = ShiftType.Late,
                DayInYear = now.DayOfYear,
                CreatedDateTime = now.AddDays( 2 )
            };

            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Late,
                EndTime = new TimeSpan(now.Hour + 2, now.Minute + 10, now.Second),
                StartTime = new TimeSpan(now.Hour - 2, now.Minute - 10, now.Second),
                StartDay = now.DayOfWeek,
                EndDay = now.AddDays(1)
                            .DayOfWeek
            };

            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift,shift
            };
            var testSheetListQuery = new List<TestSheet>
            {
                testsheet
            };

            var testSheetRepository = MockHelperBll.TestSheetRepository(testSheetListQuery.AsQueryable());
            var shifthelper = MockHelperBll.GetShiftHelper(ShiftType.Late, shift, lastXShiftSchedules: shiftSheduleListQuery);

            var target = new LaborDashboardBll(new NLogLoggerFactory())
            {
                ShiftHelper = shifthelper,
                TestSheets = testSheetRepository
            };

            var actual = target.GetTestSheetForActualAndLastThreeShifts();
            actual.Count.Should().Be(0);
        }
    }
}