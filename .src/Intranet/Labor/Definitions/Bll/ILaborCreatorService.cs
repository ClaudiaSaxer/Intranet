using System;
using Intranet.Labor.Model.labor;
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

        /// <summary>
        ///     Gets the test sheet id for the fa nr
        /// </summary>
        /// <param name="faNr">the production order number</param>
        /// <returns>the id of the testsheet or null if no exists</returns>
        TestSheet GetTestSheetId( String faNr );

     
    }
}