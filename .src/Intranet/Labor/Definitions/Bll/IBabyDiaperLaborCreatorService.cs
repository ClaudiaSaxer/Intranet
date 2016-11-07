using System;
using Intranet.Labor.ViewModel;

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     Interface representing the labor creator service
    /// </summary>
    public interface IBabyDiaperLaborCreatorService
    {
        /// <summary>
        ///     Get the labor creator view model for a specific id.
        /// </summary>
        /// <returns>the labor creator view model</returns>
        BabyDiaperLaborCreatorViewModel GetLaborCreatorViewModel( Int32 testSheetId );
    }
}