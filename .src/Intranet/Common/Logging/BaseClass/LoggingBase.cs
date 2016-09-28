using System;
using Extend;
using Intranet.Definition;

namespace Intranet.Common
{
    /// <summary>
    ///     Abstract base class providing logging features.
    /// </summary>
    public abstract class LoggingBase
    {
        #region Fields

        /// <summary>
        ///     The logger used by this class.
        /// </summary>
        protected readonly ILogger Logger;

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="LoggingBase" /> class.
        /// </summary>
        /// <exception cref="ArgumentNullException">loggerFactory can not be null</exception>
        /// <param name="logger">A <see cref="ILogger" />.</param>
        protected LoggingBase( ILogger logger )
        {
            logger.ThrowIfNull( nameof( logger ) );

            logger.Trace( "Enter Ctor - After next line." );
            Logger = logger;
        }

        #endregion
    }
}