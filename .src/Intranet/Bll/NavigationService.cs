using System;
using Intranet.Definition;

namespace Intranet.Bll
{
    /// <summary>
    ///     Service for the nav
    /// </summary>
    public class NavigationService : LoggingBase

    {
        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="LoggingBase" /> class.
        /// </summary>
        /// <exception cref="ArgumentNullException">loggerFactory can not be null</exception>
        /// <param name="logger">A <see cref="ILogger" />.</param>
        public NavigationService( ILogger logger )
            : base( logger )
        {
        }

        #endregion
    }
}