using System;
using Intranet.Definition;

namespace Intranet.Shell.Bll
{
    /// <summary>
    /// </summary>
    public class LoggingTest : LoggingBase
    {
        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="LoggingBase" /> class.
        /// </summary>
        /// <exception cref="ArgumentNullException">loggerFactory can not be null</exception>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public LoggingTest( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(LoggingTest) ) )
        {
            Logger.Debug( "hi" );
        }

        #endregion
    }
}