#region Usings

using System;
using System.Collections.Generic;
using Intranet.Labor.Model;

#endregion

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     Interaface representing Labor Creator Bll
    /// </summary>
    public interface ILaborCreatorBll
    {
        /// <summary>
        ///     The current existing testsheet for given fanr or null if not existing
        /// </summary>
        /// <param name="faNr">the production order number</param>
        /// <returns>the current testsheet</returns>
        TestSheet GetTestSheetForFaNr( String faNr );

        /// <summary>
        ///     Initializes a new testsheet for the faNr and the current date
        /// </summary>
        /// <param name="faNr">the production order number</param>
        /// <returns>the initialized testsheet</returns>
        TestSheet InitTestSheetForFaNr( String faNr );

        /// <summary>
        ///     The Production Orders which are running at the moment
        /// </summary>
        /// <returns>the running production orders</returns>
        ICollection<TestSheet> RunningTestSheets();
    }
}