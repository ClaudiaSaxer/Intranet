#region Usings

using System;
using System.Collections.Generic;
using Intranet.Labor.Model.labor;

#endregion

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     Interface representing the history bll
    /// </summary>
    public interface IHistoryBll
    {
        /// <summary>
        ///     Gets all testsheet with the given faNr from the database
        /// </summary>
        /// <param name="faNr">the faNr for the testsheets</param>
        /// <returns>a list of testsheets</returns>
        IEnumerable<TestSheet> GetTestSheets( String faNr );
    }
}