using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Dal.Repositories;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing labor creator bll
    /// </summary>
    public class LaborCreatorBll : ServiceBase, ILaborCreatorBll
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the repository for the production order
        /// </summary>
        /// <value>the production order repository</value>
        public IGenericRepository<TestSheet> TestSheetRepository { get; set; }

        /// <summary>
        /// Gets or sets the repository for the shift schuedule
        /// </summary>
        /// <value>the shift shedule repository</value>
        public IGenericRepository<ShiftSchedule> ShiftTypeRepository { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public LaborCreatorBll( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(LaborCreatorBll) ) )
        {
        }

        #endregion

        #region Implementation of ILaborCreatorBll

        /// <summary>
        ///     The TestSheets which are running at the moment
        /// </summary>
        /// <returns>the running production orders</returns>
        public ICollection<TestSheet> RunningTestSheets()
        {
            var today = DateTime.Today;
            var shift = GetCurrentShift();
            if ( shift == null )
                return null;
            return TestSheetRepository.Where(sheet => sheet.DayInYear.Equals( today.DayOfYear ) && sheet.ShiftType == shift                                                 )
                                            .ToList();
        }

        /// <summary>
        /// Gets the current shift
        /// </summary>
        /// <returns>the current shift</returns>
        public ShiftType? GetCurrentShift()
        {
            var now = DateTime.Now;
            var dayInWeekNow = now.DayOfWeek;
            var shift = ShiftTypeRepository.Where(
                                               schedule =>
                                                   ( schedule.StartDay == dayInWeekNow || schedule.EndDay == dayInWeekNow ) && schedule.StartTime.Hours <= now.Hour
                                                   && schedule.StartTime.Minutes <= now.Minute && schedule.EndTime.Hours >= now.Hour && schedule.EndTime.Minutes >= now.Minute )
                                           ?.ToList();
            if ( shift == null || shift.Count == 0 )
                Logger.Error( "No Shift found for: " + now );
            
            if(shift?.Count > 1)
                Logger.Error( "More than one shift found for "+now );

            return shift?[0].ShiftType;
        }
        #endregion

        #region Implementation of ILaborCreatorBll

        #endregion
    }
}