#region Usings

using System;
using Intranet.Labor.ViewModel;

#endregion

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     The Interface for the BabyDiapersRetentionService
    /// </summary>
    public interface IBabyDiapersRetentionService
    {
        /// <summary>
        ///     Gets a new BabyDiapersRetentionEditViewModel
        /// </summary>
        /// <param name="retentionTestId">The Id of the Babydiaper retention test which will be edited</param>
        /// <returns>The BabyDiapersRetentionEditViewModel</returns>
        BabyDiapersRetentionEditViewModel GetBabyDiapersRetentionEditViewModel( Int32 retentionTestId );

        /// <summary>
        ///     Gets the BabyDiapersRetentionEditViewModel for edit
        /// </summary>
        /// <param name="testSheetId">The Id of the test sheet where the Babydiaper retention test is for</param>
        /// <returns>The BabyDiapersRetentionEditViewModel</returns>
        BabyDiapersRetentionEditViewModel GetNewBabyDiapersRetentionEditViewModel( Int32 testSheetId );

        /// <summary>
        ///     Saves or updates the BabyDiapersRetentionEditViewModel
        /// </summary>
        /// <param name="babyDiapersRetentionEditViewModel">The viewmodel which will be saved or updated</param>
        /// <returns>The saved or updated BabyDiapersRetentionEditViewModel</returns>
        BabyDiapersRetentionEditViewModel Save( BabyDiapersRetentionEditViewModel babyDiapersRetentionEditViewModel );
    }
}