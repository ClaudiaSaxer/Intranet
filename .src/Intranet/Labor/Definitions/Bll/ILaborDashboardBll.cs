using System;
using System.Collections.Generic;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;
using Intranet.Labor.ViewModel.LaborCreator;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Interaface representing Labor Dashboard Bll
    /// </summary>
    public interface ILaborDashboardBll 
    {
        /// <summary>
        /// Get the testsheets for the actual shift and the last 3 shifts
        /// </summary>
        /// <returns>the testsheets for the actual and last three shifts</returns>
         ICollection<TestSheet> GetTestSheetForActualAndLastThreeShifts();
    }
}