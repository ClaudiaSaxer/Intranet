#region Usings

using System;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;

#endregion

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     The Interface for the BabyDiaperRetentionService
    /// </summary>
    public interface IBabyDiaperRetentionService
    {
        /// <summary>
        ///     Gets a new BabyDiaperRetentionEditViewModel
        /// </summary>
        /// <param name="retentionTestId">The Id of the Babydiaper retention test which will be edited</param>
        /// <returns>The BabyDiaperRetentionEditViewModel</returns>
        BabyDiaperRetentionEditViewModel GetBabyDiapersRetentionEditViewModel( Int32 retentionTestId );

        /// <summary>
        ///     Gets the BabyDiaperRetentionEditViewModel for edit
        /// </summary>
        /// <param name="testSheetId">The Id of the test sheet where the Babydiaper retention test is for</param>
        /// <returns>The BabyDiaperRetentionEditViewModel</returns>
        BabyDiaperRetentionEditViewModel GetNewBabyDiapersRetentionEditViewModel( Int32 testSheetId );

        /// <summary>
        ///     Saves or updates the BabyDiaperRetentionEditViewModel
        /// </summary>
        /// <param name="babyDiaperRetentionEditViewModel">The viewmodel which will be saved or updated</param>
        /// <returns>The saved or updated TestValue</returns>
        TestValue Save( BabyDiaperRetentionEditViewModel babyDiaperRetentionEditViewModel );

        /// <summary>
        ///     deletes the testvalue
        /// </summary>
        /// <param name="testValueId">id of the testvalue</param>
        /// <returns>The deleted testvalue</returns>
        TestValue Delete(Int32 testValueId);
    }
}