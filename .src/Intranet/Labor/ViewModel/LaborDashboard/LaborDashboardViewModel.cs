using System.Collections.Generic;

namespace Intranet.Labor.ViewModel.LaborDashboard
{
    /// <summary>
    ///     Class representing the viewmodel for the labor dashboard
    /// </summary>
    public class LaborDashboardViewModel
    {
        /// <summary>
        /// Gets or sets a collection of Dashboard Items for the view model
        /// </summary>
        /// <value>doashboard items</value>
        public ICollection<DashboardItem> DashboardItem { get; set; }
    }


}