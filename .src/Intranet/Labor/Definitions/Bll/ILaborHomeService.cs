using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.Labor.Definition.Bll
{
    /// <summary>
    ///     The Interface for the LaborHomeService
    /// </summary>
    interface ILaborHomeService
    {
        /// <summary>
        ///     Gets the LaborHomeViewModel
        /// </summary>
        /// <returns>The LaborHomeViewModel</returns>
        LaborHomeViewModel GetLaborHomeViewModel();
    }
}
