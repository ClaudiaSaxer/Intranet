#region Usings

using System;
using Intranet.Labor.Model;
using Intranet.Labor.ViewModel;

#endregion

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     The Interface for the incontinence acquisition service helper
    /// </summary>
    public interface IInkoAquisitionServiceHelper
    {
        /// <summary>
        ///     Creates an new TestValue from the view model
        /// </summary>
        /// <param name="viewModel">the data from the view</param>
        /// <returns>The created test value</returns>
        TestValue SaveNewAquisitionTest( InkoAquisitionEditViewModel viewModel );

        /// <summary>
        ///     Updates the Average and standard deviation values of the testsheet for aquisition values
        /// </summary>
        /// <param name="testSheetId">id of the test sheet</param>
        /// <returns>the updated test sheet</returns>
        TestSheet UpdateAquisitionAverageAndStv( Int32 testSheetId );

        /// <summary>
        ///     Updates an given Testvalue from the view model
        /// </summary>
        /// <param name="viewModel">the data from the view</param>
        /// <returns>the updated test value</returns>
        TestValue UpdateAquisitionTest( InkoAquisitionEditViewModel viewModel );
    }
}