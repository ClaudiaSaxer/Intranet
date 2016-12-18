#region Usings

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace Intranet.Labor.Model
{
    /// <summary>
    ///     Class representing the shift schedule
    /// </summary>
    public class ShiftSchedule
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the id of the shift shedule
        /// </summary>
        /// <value>the id of the shift shedule</value>
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public Int32 ShiftScheduleId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the shift shedule
        /// </summary>
        /// <value>the name of the shift shedule</value>
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public String Name { get; set; }

        /// <summary>
        ///     Gets or sets the start day of the shift shedule <see cref="DayOfWeek" />
        /// </summary>
        /// <value>the start day</value>
        public DayOfWeek StartDay { get; set; }

        /// <summary>
        ///     Gets or sets the end day of the shift shedule <see cref="DayOfWeek" />
        /// </summary>
        /// <value>the end day</value>
        public DayOfWeek EndDay { get; set; }

        /// <summary>
        ///     Gets or sets the start time of the shift shedule
        /// </summary>
        /// <value>the start time</value>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        ///     Gets or sets the end time of the shift shedule
        /// </summary>
        /// <value>the end time</value>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        ///     Gets or sets the type of the shift <see cref="ShiftType" />
        /// </summary>
        /// <value>the type of the shift</value>
        public ShiftType ShiftType { get; set; }

        #endregion
    }
}