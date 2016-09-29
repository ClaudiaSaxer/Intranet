using System;
using Intranet.Definition;
using Intranet.Definition.Logger;

namespace Intranet.Shell.Bll
{
    public class LoggingTest : LoggingBase
    {
        #region Properties

        public ILoggerFactory LoggerFactory { get; set; }

        #endregion

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