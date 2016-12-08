using System;
using Extend;
using Intranet.Common;
using Intranet.Labor.Definition;

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Labor Creator Service Helper
    /// </summary>
    public class LaborCreatorServiceHelper : ServiceBase, ILaborCreatorServiceHelper
    {
        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public LaborCreatorServiceHelper( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(LaborCreatorServiceHelper) ) )
        {
        }

        #endregion

        /// <summary>
        ///     Generates the Production Code for the Diaper
        /// </summary>
        /// <param name="machine">The machine Nr</param>
        /// <param name="year">the year of the production of the diaper</param>
        /// <param name="dayOfyear">the day of the year of the production of the diaper</param>
        /// <param name="time">the time od the production of the diaper</param>
        /// <returns>A Production code for a single diaper</returns>
        public String GenerateProdCode( String machine, Int32 year, Int32 dayOfyear, TimeSpan time )
            => "IT/" + machine.Substring( 1 ) + "/" + year.ToString( "0000" )
                                                          .SubstringRight( 2 ) + "/" + dayOfyear + "/" + time.Hours.ToString( "00" ) + ":" + time.Minutes.ToString( "00" );
    }
}