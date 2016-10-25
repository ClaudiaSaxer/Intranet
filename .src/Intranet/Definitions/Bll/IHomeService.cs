using Intranet.ViewModel;

namespace Intranet.Definition
{
    /// <summary>
    ///     The Interface for the HomeService
    /// </summary>
    public interface IHomeService 
    {
        /// <summary>
        ///     Gets the HomeViewModel
        /// </summary>
        /// <returns>The HomeViewModel</returns>
        HomeViewModel GetHomeViewModel();
    }
}