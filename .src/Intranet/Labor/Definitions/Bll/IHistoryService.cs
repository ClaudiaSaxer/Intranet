#region Usings

using System;
using Intranet.Labor.ViewModel;

#endregion

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     The Interface for the HistoryService
    /// </summary>
    public interface IHistoryService
    {
        /// <summary>
        ///     gets the HistoryViewModel
        /// </summary>
        /// <param name="faNr">the FaNr</param>
        /// <returns>The HistoryViewModel</returns>
        HistoryViewModel GetHistoryViewModel( String faNr );
    }
}