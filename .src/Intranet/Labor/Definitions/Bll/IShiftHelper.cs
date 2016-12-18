#region Usings

using System;
using System.Collections.Generic;
using Intranet.Labor.Model;

#endregion

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     Class representing a interface for shift helper
    /// </summary>
    public interface IShiftHelper
    {
        /// <summary>
        ///     Calculate if the acual date exists in the list of shifts
        /// </summary>
        /// <param name="date">the date to test</param>
        /// <param name="shift">the shift to test if the date exists in</param>
        /// <returns></returns>
        Boolean DateExistsInShift( DateTime date, ShiftSchedule shift );

        /// <summary>
        ///     Calculate if the acual date exists in the list of shifts
        /// </summary>
        /// <param name="date">the date to test</param>
        /// <param name="shifts">the shift to test if the date exists in</param>
        /// <returns></returns>
        Boolean DateExistsInShifts( DateTime date, List<ShiftSchedule> shifts );

        /// <summary>
        ///     Gets the current shift
        /// </summary>
        /// <returns>the current shift</returns>
        ShiftType? GetCurrentShift();

        /// <summary>
        ///     Gets the current shift shedule
        /// </summary>
        /// <returns>the current shift shedule</returns>
        ShiftSchedule GetCurrentShiftShedule();

        /// <summary>
        ///     Gets the current shift shedule
        /// </summary>
        /// <returns>the current shift shedule</returns>
        List<ShiftSchedule> GetLastXShiftSchedule( Int32 quantity );
    }
}