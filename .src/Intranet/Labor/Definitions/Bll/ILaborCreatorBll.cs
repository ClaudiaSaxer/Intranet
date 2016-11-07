using System.Collections.Generic;
using Intranet.Labor.ViewModel;
using Intranet.Labor.ViewModel.LaborCreator;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Interaface representing Labor Creator Bll
    /// </summary>
    public interface ILaborCreatorBll 
    {
        /// <summary>
        /// The Production Orders which are running at the moment
        /// </summary>
        /// <returns>the running production orders</returns>
        ICollection<RunningProductionOrder> RunningProductionOrders();
    }
}