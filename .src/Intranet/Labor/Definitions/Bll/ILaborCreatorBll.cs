using System;
using System.Linq;
using Intranet.Labor.Model.labor;

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     Interface representing the labor creator bll
    /// </summary>
    public interface ILaborCreatorBll
    {
        /// <summary>
        ///     Gets the testsheet for the given id from the database
        /// </summary>
        /// <param name="id">the unique identifier for the test sheet</param>
        /// <returns>a testsheet with the given data, or null if not found</returns>
        TestSheet getTestSheetForId( Int32 id );
    }
}