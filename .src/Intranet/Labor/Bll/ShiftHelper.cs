using System;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Dal.Repositories;
using Intranet.Labor.Model;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    /// Class representing a helper for shift
    /// </summary>
    public class ShiftHelper : ServiceBase, IShiftHelper

    {

        /// <summary>
        ///     Gets or sets the repository for the shift schuedule
        /// </summary>
        /// <value>the shift shedule repository</value>
        public IGenericRepository<ShiftSchedule> ShiftScheduleRepository { get; set; }


        /// <summary>
        ///     Gets the current shift
        /// </summary>
        /// <returns>the current shift</returns>
        public ShiftType? GetCurrentShift()
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
            return shift?[0].ShiftType;
        Logger.Error( "More than one or no shift found for " + now );
        return null;
    }
    }
}