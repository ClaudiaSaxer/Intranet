#region Usings

using Intranet.ViewModel;

#endregion

namespace Intranet.Common
{
    /// <summary>
    ///     Interface representing the service for the navigation
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        ///     All main models that the current User is allowed to see.
        /// </summary>
        /// <returns>The ViewModel for the navigation</returns>
        NavigationViewModel GetNavigationViewModel();
    }
}