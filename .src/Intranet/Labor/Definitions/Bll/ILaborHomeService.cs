using Intranet.Labor.ViewModel;

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     The Interface for the LaborHomeService
    /// </summary>
    public interface ILaborHomeService
    {
        /// <summary>
        ///     Gets the LaborHomeViewModel
        /// </summary>
        /// <returns>The LaborHomeViewModel</returns>
        LaborHomeViewModel GetLaborHomeViewModel();
    }
}