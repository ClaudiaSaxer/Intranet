﻿#region Usings

using Intranet.Labor.ViewModel;

#endregion

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     Interaface representing Labor Dashboard Service
    /// </summary>
    public interface ILaborDashboardService
    {
        /// <summary>
        ///     Labor creator view model
        /// </summary>
        /// <returns>the LaborDashboardViewModel</returns>
        LaborDashboardViewModel GetLaborDashboardViewModel();
    }
}