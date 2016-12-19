#region Usings

using System;
using Intranet.Labor.Model;

#endregion

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     Interface representing the inko labor creator bll
    /// </summary>
    public interface IIncontinencePadLaborCreatorBll
    {
        /// <summary>
        ///     Gets the testsheet for the given id from the database
        /// </summary>
        /// <param name="id">the unique identifier for the test sheet</param>
        /// <returns>a testsheet with the given data, or null if not found</returns>
        TestSheet GetTestSheetForId( Int32 id );
    }
}