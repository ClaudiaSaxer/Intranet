using System;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;
using Intranet.Labor.ViewModel.LaborDashboard;

namespace Intranet.Web.Areas.Labor.Controllers
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