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
    ///     Test Class for LaborCreatorBll
    /// </summary>
    public class LaborCreatorBllTest
    {
        /// <summary>
        ///     Normal Passing Test for GetCurrentShift 
        /// </summary>
        [Fact]
        public void GetCurrentShiftNormal()
        {
            var now = DateTime.Now;

            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Morning,
                EndTime = new TimeSpan( now.Hour + 2, now.Minute, now.Second ),
                StartTime = new TimeSpan( now.Hour-2, now.Minute, now.Second ),
                StartDay = now.DayOfWeek,
                EndDay = now.DayOfWeek

            };
          
            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift
            };

         
            var shiftSheduleRepository = MockHelperBll.GetAllShiftSchedules( shiftSheduleListQuery.AsQueryable() );
        

            var target = new LaborCreatorBll( new NLogLoggerFactory() )
            {
                ProductionOrderRepository = null,
                ShiftScheduleRepository = shiftSheduleRepository,
                TestSheetRepository = null
            };

            var actual = target.GetCurrentShift()
                               .Should()
                               .Be( ShiftType.Morning );

        }
        /// <summary>
        ///    Test for GetCurrentShift To Many Found
        /// </summary>
        [Fact]
        public void GetCurrentShiftToMany()
        {
            var now = DateTime.Now;

            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Late,
                EndTime = new TimeSpan(now.Hour + 2, now.Minute, now.Second),
                StartTime = new TimeSpan(now.Hour - 2, now.Minute, now.Second),
                StartDay = now.DayOfWeek,
                EndDay = now.DayOfWeek

            };

            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift,shift
            };


            var shiftSheduleRepository = MockHelperBll.GetAllShiftSchedules(shiftSheduleListQuery.AsQueryable());


            var target = new LaborCreatorBll(new NLogLoggerFactory())
            {
                ProductionOrderRepository = null,
                ShiftScheduleRepository = shiftSheduleRepository,
                TestSheetRepository = null
            };

            target.GetCurrentShift()
                  .Should()
                  .BeNull( "because more than one existing" );
        }
        /// <summary>
        ///     Not Found Test for GetCurrentShift
        /// </summary>
        [Fact]
        public void GetCurrentShiftNotFound()
        {
            var now = DateTime.Now;

            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Late,
                EndTime = new TimeSpan(now.Hour  -1, now.Minute, now.Second),
                StartTime = new TimeSpan(now.Hour +2, now.Minute, now.Second),
                StartDay = now.DayOfWeek,
                EndDay = now.AddDays(1).DayOfWeek,

            };

            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift
            };


            var shiftSheduleRepository = MockHelperBll.GetAllShiftSchedules(shiftSheduleListQuery.AsQueryable());


            var target = new LaborCreatorBll(new NLogLoggerFactory())
            {
                ProductionOrderRepository = null,
                ShiftScheduleRepository = shiftSheduleRepository,
                TestSheetRepository = null
            };

            var actual = target.GetCurrentShift()
                               .Should()
                               .BeNull( "because not existing" );

        }
    }
}