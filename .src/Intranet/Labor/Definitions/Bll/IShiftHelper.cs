using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.Labor.Model;

namespace Intranet.Labor.Definition.Bll
{
    /// <summary>
    /// Class representing a interface for shift helper
    /// </summary>
    public interface IShiftHelper
    {
        /// <summary>
        ///     Gets the current shift
        /// </summary>
        /// <returns>the current shift</returns>
        ShiftType? GetCurrentShift();
    }
}
