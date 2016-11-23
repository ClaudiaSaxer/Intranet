using Intranet.Labor.ViewModel;

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