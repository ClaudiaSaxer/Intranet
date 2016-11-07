using Intranet.Labor.ViewModel;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Interaface representing Labor Creator Service
    /// </summary>
    public interface ILaborCreatorService 
    {
        /// <summary>
        ///     Labor creator view model
        /// </summary>
        /// <returns>the LaborCreatorViewModel</returns>
        LaborCreatorViewModel GetLaborCreatorViewModel();
    }
}