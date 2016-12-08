using System;
using System.Collections.Generic;
using Intranet.Labor.Model;

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     Interaface representing Labor Dashboard Bll
    /// </summary>
    public interface ILaborDashboardBll
    {
        /// <summary>
        ///     Get the testsheets for the actual shift and the last 3 shifts
        /// </summary>
        /// <returns>the testsheets for the actual and last three shifts</returns>
        ICollection<TestSheet> GetTestSheetForActualAndLastThreeShifts();

        /// <summary>
        ///     Get the testsheets for the last shift
        /// </summary>
        /// <returns>the testsheets for the last shift</returns>
        ICollection<TestSheet> GetTestSheetForMinusXShiftPerMachineNr( Int32 lastCounter, String machineNr );

        /// <summary>
        ///     Get the testsheets for the given shift
        /// </summary>
        /// <returns>the testsheets for the given shift</returns>
        ICollection<TestSheet> GetTestSheetForShifts( List<ShiftSchedule> shifts );
    }
}