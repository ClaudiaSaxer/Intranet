using System;
using System.Collections.Generic;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
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
        ICollection<TestSheet> RunningTestSheets();

        /// <summary>
        /// The current existing testsheet for given fanr or null if not existing
        /// </summary>
        /// <param name="faNr">the production order number</param>
        /// <returns>the current testsheet</returns>
        TestSheet GetTestSheetForFaNr( String faNr );
        /// <summary>
        /// Initializes a new testsheet for the faNr and the current date
        /// </summary>
        /// <param name="faNr">the production order number</param>
        /// <returns>the initialized testsheet</returns>
        TestSheet InitTestSheetForFaNr( String faNr );
    }
}