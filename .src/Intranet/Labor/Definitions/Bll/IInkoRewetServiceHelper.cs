using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;

namespace Intranet.Labor.Definition.Bll
{
    /// <summary>
    ///     The Interface for the incontinence rewet service helper
    /// </summary>
    public interface IInkoRewetServiceHelper
    {
        /// <summary>
        ///     Creates an new TestValue from the view model
        /// </summary>
        /// <param name="viewModel">the data from the view</param>
        /// <returns>The created test value</returns>
        TestValue SaveNewRewetTest(InkoRewetEditViewModel viewModel);

        /// <summary>
        ///     Updates the Average and standard deviation values of the testsheet for retention values
        /// </summary>
        /// <param name="testSheetId">id of the test sheet</param>
        /// <returns>the updated test sheet</returns>
        TestSheet UpdateRewetAverageAndStv(Int32 testSheetId);

        /// <summary>
        ///     Updates an given Testvalue from the view model
        /// </summary>
        /// <param name="viewModel">the data from the view</param>
        /// <returns>the updated test value</returns>
        TestValue UpdateRewetTest(InkoRewetEditViewModel viewModel);
    }
}
