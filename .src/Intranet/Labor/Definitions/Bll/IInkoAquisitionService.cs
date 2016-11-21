#region Usings

using System;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel.InkoEdit;

#endregion

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     The Interface for the InkoAquisitionService
    /// </summary>
    public interface IInkoAquisitionService
    {
        /// <summary>
        ///     deletes the testvalue
        /// </summary>
        /// <param name="testValueId">id of the testvalue</param>
        /// <returns>The deleted testvalue</returns>
        TestValue Delete( Int32 testValueId );

        /// <summary>
        ///     Gets a new InkoAquisitionEditViewModel
        /// </summary>
        /// <param name="rewetTestId">The Id of the Inko rewet test which will be edited</param>
        /// <returns>The InkoAquisitionEditViewModel</returns>
        InkoAquisitionEditViewModel GetInkoAquisitionEditViewModel( Int32 rewetTestId );

        /// <summary>
        ///     Gets the InkoAquisitionEditViewModel for edit
        /// </summary>
        /// <param name="testSheetId">The Id of the test sheet where the Inko rewet test is for</param>
        /// <returns>The InkoAquisitionEditViewModel</returns>
        InkoAquisitionEditViewModel GetNewInkoAquisitionEditViewModel( Int32 testSheetId );

        /// <summary>
        ///     Saves or updates the InkoAquisitionEditViewModel
        /// </summary>
        /// <param name="viewModel">The viewmodel which will be saved or updated</param>
        /// <returns>The saved or updated TestValue</returns>
        TestValue Save( InkoAquisitionEditViewModel viewModel );
    }
}