#region Usings

using System;
using Intranet.Labor.Model;
using Intranet.Labor.ViewModel;

#endregion

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     The Interface for the BabyDiaperRewetService
    /// </summary>
    public interface IBabyDiaperRewetService
    {
        /// <summary>
        ///     deletes the testvalue
        /// </summary>
        /// <param name="testValueId">id of the testvalue</param>
        /// <returns>The deleted testvalue</returns>
        TestValue Delete( Int32 testValueId );

        /// <summary>
        ///     Gets a new BabyDiaperRewetEditViewModel
        /// </summary>
        /// <param name="rewetTestId">The Id of the Babydiaper rewet test which will be edited</param>
        /// <returns>The BabyDiaperRewetEditViewModel</returns>
        BabyDiaperRewetEditViewModel GetBabyDiaperRewetEditViewModel( Int32 rewetTestId );

        /// <summary>
        ///     Gets the BabyDiaperRewetEditViewModel for edit
        /// </summary>
        /// <param name="testSheetId">The Id of the test sheet where the Babydiaper rewet test is for</param>
        /// <returns>The BabyDiaperRewetEditViewModel</returns>
        BabyDiaperRewetEditViewModel GetNewBabyDiaperRewetEditViewModel( Int32 testSheetId );

        /// <summary>
        ///     Saves or updates the BabyDiaperRewetEditViewModel
        /// </summary>
        /// <param name="babyDiaperRewetEditViewModel">The viewmodel which will be saved or updated</param>
        /// <returns>The saved or updated TestValue</returns>
        TestValue Save( BabyDiaperRewetEditViewModel babyDiaperRewetEditViewModel );
    }
}