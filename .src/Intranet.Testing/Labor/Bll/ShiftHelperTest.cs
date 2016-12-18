#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Intranet.Common;
using Intranet.Labor.Model;
using Intranet.Labor.TestEnvironment;
using Xunit;

#endregion

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Class representing test class for shift helper
    /// </summary>
    public class ShiftHelperTest
    {
        /// <summary>
        ///     Normal Passing Test for GetCurrentShift
        /// </summary>
        [Fact]
        public void GetCurrentShiftNormalTest()
        {
            var now = DateTime.Now;

            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Morning,
                EndTime = new TimeSpan( now.Hour + 2, now.Minute, now.Second ),
                StartTime = new TimeSpan( now.Hour - 2, now.Minute, now.Second ),
                StartDay = now.DayOfWeek,
                EndDay = now.DayOfWeek
            };

            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift
            };

            var shiftSheduleRepository = MockHelperBll.GetAllShiftSchedules( shiftSheduleListQuery.AsQueryable() );

            var target = new ShiftHelper( new NLogLoggerFactory() )
            {
                ShiftScheduleRepository = shiftSheduleRepository
            };

            var actual = target.GetCurrentShift()
                               .Should()
                               .Be( ShiftType.Morning );
        }

        /// <summary>
        ///     Not Found Test for GetCurrentShift1
        /// </summary>
        [Fact]
        public void GetCurrentShiftTestNotFound1()
        {
            var now = DateTime.Now;

            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Late,
                EndTime = new TimeSpan( now.Hour - 1, now.Minute, now.Second ),
                StartTime = new TimeSpan( now.Hour + 2, now.Minute, now.Second ),
                StartDay = now.DayOfWeek,
                EndDay = now.AddDays( 1 )
                            .DayOfWeek
            };

            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift
            };

            var shiftSheduleRepository = MockHelperBll.GetAllShiftSchedules( shiftSheduleListQuery.AsQueryable() );

            var target = new ShiftHelper( new NLogLoggerFactory() )
            {
                ShiftScheduleRepository = shiftSheduleRepository
            };

            var actual = target.GetCurrentShift()
                               .Should()
                               .BeNull( "because not existing" );
        }

        /// <summary>
        ///     Not Found Test for GetCurrentShift2
        /// </summary>
        [Fact]
        public void GetCurrentShiftTestNotFound2()
        {
            var now = DateTime.Now;

            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Late,
                EndTime = new TimeSpan( now.Hour, now.Minute - 30, now.Second ),
                StartTime = new TimeSpan( now.Hour - 2, now.Minute, now.Second ),
                StartDay = now.DayOfWeek,
                EndDay = now.AddDays( 1 )
                            .DayOfWeek
            };

            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift
            };

            var shiftSheduleRepository = MockHelperBll.GetAllShiftSchedules( shiftSheduleListQuery.AsQueryable() );

            var target = new ShiftHelper( new NLogLoggerFactory() )
            {
                ShiftScheduleRepository = shiftSheduleRepository
            };

            var actual = target.GetCurrentShift()
                               .Should()
                               .BeNull( "because not existing" );
        }

        /// <summary>
        ///     Not Found Test for GetCurrentShift3
        /// </summary>
        [Fact]
        public void GetCurrentShiftTestNotFound3()
        {
            var now = DateTime.Now;

            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Late,
                EndTime = new TimeSpan( now.Hour, now.Minute, now.Second ),
                StartTime = new TimeSpan( now.Hour, now.Minute + 30, now.Second ),
                StartDay = now.DayOfWeek,
                EndDay = now.AddDays( 1 )
                            .DayOfWeek
            };

            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift
            };

            var shiftSheduleRepository = MockHelperBll.GetAllShiftSchedules( shiftSheduleListQuery.AsQueryable() );

            var target = new ShiftHelper( new NLogLoggerFactory() )
            {
                ShiftScheduleRepository = shiftSheduleRepository
            };

            var actual = target.GetCurrentShift()
                               .Should()
                               .BeNull( "because not existing" );
        }

        /// <summary>
        ///     Test for GetCurrentShift To Many Found
        /// </summary>
        [Fact]
        public void GetCurrentShiftTestToMany()
        {
            var now = DateTime.Now;

            var shift = new ShiftSchedule
            {
                Name = "The One",
                ShiftType = ShiftType.Late,
                EndTime = new TimeSpan( now.Hour + 2, now.Minute, now.Second ),
                StartTime = new TimeSpan( now.Hour - 2, now.Minute, now.Second ),
                StartDay = now.DayOfWeek,
                EndDay = now.DayOfWeek
            };

            var shiftSheduleListQuery = new List<ShiftSchedule>
            {
                shift,
                shift
            };

            var shiftSheduleRepository = MockHelperBll.GetAllShiftSchedules( shiftSheduleListQuery.AsQueryable() );

            var target = new ShiftHelper( new NLogLoggerFactory() )
            {
                ShiftScheduleRepository = shiftSheduleRepository
            };

            target.GetCurrentShift()
                  .Should()
                  .BeNull( "because more than one existing" );
        }
    }
}