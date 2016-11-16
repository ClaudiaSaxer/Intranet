using System.Collections.Generic;

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the viewmodel for the labor dashboard
    /// </summary>
    public class LaborDashboardViewModel
    {
        #region Properties

        /// <summary>
        ///     Gets or sets a collection of Dashboard Items for the view model
        /// </summary>
        /// <value>doashboard items</value>
        public ICollection<DashboardItem> DashboardItem { get; set; }

        #endregion
    }
}