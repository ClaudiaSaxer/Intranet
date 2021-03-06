﻿#region Usings

using System;
using Intranet.Labor.ViewModel;

#endregion

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     Interface representing the inko labor creator service
    /// </summary>
    public interface IIncontinencePadLaborCreatorService
    {
        /// <summary>
        ///     Get the labor creator view model for a specific id.
        /// </summary>
        /// <returns>the labor creator view model</returns>
        IncontinencePadLaborCreatorViewModel GetLaborCreatorViewModel( Int32 testSheetId );
    }
}