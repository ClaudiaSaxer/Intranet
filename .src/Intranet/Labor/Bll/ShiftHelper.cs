using System;
using System.Collections.Generic;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Definition.Bll;
using Intranet.Labor.Model;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing a helper for shift
    /// </summary>
    public class ShiftHelper : ServiceBase, IShiftHelper

    {
        #region Properties

        /// <summary>
        ///     Gets or sets the repository for the shift schuedule
        /// </summary>
        /// <value>the shift shedule repository</value>
        public IGenericRepository<ShiftSchedule> ShiftScheduleRepository { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public ShiftHelper( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(ShiftHelper) ) )
        {
        }

        #endregion

        /// <summary>
        ///     Calculate if the acual date exists in the list of shifts
        /// </summary>
        /// <param name="date">the date to test</param>
        /// <param name="shift">the shift to test if the date exists in</param>
        /// <returns></returns>
        public Boolean DateExistsInShift( DateTime date, ShiftSchedule shift )
        {
            var dayInWeekNow = date.DayOfWeek;
            return
                ( ( shift.StartDay == dayInWeekNow ) || ( shift.EndDay == dayInWeekNow ) ) && ( shift.StartTime.Hours <= date.Hour )
                && ( shift.StartTime.Minutes <= date.Minute ) && ( shift.EndTime.Hours >= date.Hour )
                && ( shift.EndTime.Minutes >= date.Minute );
        }

        /// <summary>
        ///     Calculate if the acual date exists in the list of shifts
        /// </summary>
        /// <param name="date">the date to test</param>
        /// <param name="shifts">the shift to test if the date exists in</param>
        /// <returns></returns>
        public Boolean DateExistsInShifts( DateTime date, List<ShiftSchedule> shifts ) => shifts.Exists(
            schedule => DateExistsInShift( date, schedule ));

        /// <summary>
        ///     Gets the current shift
        /// </summary>
        /// <returns>the current shift</returns>
        public ShiftType? GetCurrentShift() => GetCurrentShiftShedule()
            .ShiftType;

        /// <summary>
        ///     Gets the current shift shedule
        /// </summary>
        /// <returns>the current shift shedule</returns>
        public ShiftSchedule GetCurrentShiftShedule()
        {
            var now = DateTime.Now;
            var dayInWeekNow = now.DayOfWeek;
            var shift = ShiftScheduleRepository.GetAll()
                                               .Where(
                                                   schedule =>
                                                       ( ( schedule.StartDay == dayInWeekNow ) || ( schedule.EndDay == dayInWeekNow ) ) && ( schedule.StartTime.Hours <= now.Hour )
                                                       && ( schedule.StartTime.Minutes <= now.Minute ) && ( schedule.EndTime.Hours >= now.Hour )
                                                       && ( schedule.EndTime.Minutes >= now.Minute ) )
                                               ?.ToList();
            if ( shift?.Count == 1 )
                return shift[0];
            Logger.Error( "More than one or no shift found for " + now );
            return null;
        }

        /// <summary>
        ///     Gets the current shift shedule
        /// </summary>
        /// <returns>the current shift shedule</returns>
        public List<ShiftSchedule> GetLastXShiftSchedule( Int32 quantity )
        {
            var now = DateTime.Now.TimeOfDay;
            var dayInWeekNow = DateTime.Now.DayOfWeek;
            var shift = ShiftScheduleRepository.GetAll()
                                               .Where(
                                                   schedule =>
                                                       ( ( schedule.EndDay <= dayInWeekNow ) && ( schedule.StartTime <= now ) )
                                                       || ( schedule.StartDay < dayInWeekNow )
                                               )
                                               .OrderByDescending( schedule => schedule.StartDay )
                                               .ThenByDescending( schedule => schedule.StartTime )
                                               .ToList();

            if ( shift.Count < quantity )
            {
                var shiftafter = ShiftScheduleRepository.GetAll()
                                                        .Where(
                                                            schedule =>
                                                                ( schedule.StartDay > dayInWeekNow )
                                                                || ( ( schedule.StartDay == dayInWeekNow ) && ( schedule.StartTime > now ) )
                                                        )
                                                        .OrderByDescending( schedule => schedule.StartDay )
                                                        .ThenByDescending( schedule => schedule.StartTime )
                                                        ?.ToList();

                shift.AddRange( shiftafter );
            }

            var count = quantity > shift.Count ? shift.Count : quantity;
            return shift.GetRange( 0, count );
        }
    }
}