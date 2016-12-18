#region Usings

using Extend;

#endregion

namespace Intranet.Common
{
    /// <summary>
    ///     Class representing the base class for all services
    /// </summary>
    public class ServiceBase : LoggingBase
    {
        #region Properties

        /// <summary>
        ///     The Roles for the current user
        /// </summary>
        public IRoles Roles { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="logger">A <see cref="ILogger" />.</param>
        public ServiceBase( ILogger logger )
            : base( logger )
        {
            logger.ThrowIfNull( nameof( logger ) );
        }

        #endregion
    }
}