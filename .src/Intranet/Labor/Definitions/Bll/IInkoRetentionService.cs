#region Usings

using System;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;

#endregion

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     The Interface for the InkoRetentionService
    /// </summary>
    public interface IInkoRetentionService
    {
        /// <summary>
        ///     deletes the testvalue
        /// </summary>
        /// <param name="testValueId">id of the testvalue</param>
        /// <returns>The deleted testvalue</returns>
        TestValue Delete( Int32 testValueId );

        /// <summary>
        ///     Gets a new InkoRetentionEditViewModel
        /// </summary>
        /// <param name="rewetTestId">The Id of the Inko rewet test which will be edited</param>
        /// <returns>The InkoRewetEditViewModel</returns>
        InkoRetentionEditViewModel GetInkoRetentionEditViewModel( Int32 rewetTestId );

        /// <summary>
        ///     Gets the InkoRetentionEditViewModel for edit
        /// </summary>
        /// <param name="testSheetId">The Id of the test sheet where the Inko rewet test is for</param>
        /// <returns>The InkoRewetEditViewModel</returns>
        InkoRetentionEditViewModel GetNewInkoRetentionEditViewModel( Int32 testSheetId );

        /// <summary>
        ///     Saves or updates the InkoRetentionEditViewModel
        /// </summary>
        /// <param name="viewModel">The viewmodel which will be saved or updated</param>
        /// <returns>The saved or updated TestValue</returns>
        TestValue Save( InkoRetentionEditViewModel viewModel );
    }
}