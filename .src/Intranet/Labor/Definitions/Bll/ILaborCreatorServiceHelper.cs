using System;

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     Interface representing Labor Creator Service Helper
    /// </summary>
    public interface ILaborCreatorServiceHelper
    {
        /// <summary>
        ///     Generates the Production Code for the Diaper
        /// </summary>
        /// <param name="machine">The machine Nr</param>
        /// <param name="year">the year of the production of the diaper</param>
        /// <param name="dayOfyear">the day of the year of the production of the diaper</param>
        /// <param name="time">the time od the production of the diaper</param>
        /// <returns>A Production code for a single diaper</returns>
        String GenerateProdCode( String machine, Int32 year, Int32 dayOfyear, TimeSpan time );
    }
}