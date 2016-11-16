using System;
using System.Collections.Generic;

namespace Intranet.Common
{
    /// <summary>
    ///     Creates a <see cref="ILogger" /> Roles.GetRolesForUser()
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        ///     Gets a loggerFactory with the specified name.
        /// </summary>
        /// <param name="loggerName">The name of the loggerFactory.</param>
        /// <returns>A loggerFactory with specified name.</returns>
        ILogger CreateLogger( String loggerName );

        /// <summary>
        ///     Gets a loggerFactory for the specified type.
        /// </summary>
        /// <param name="t">The type of the logging class.</param>
        /// <returns>A loggerFactory for the specified type.</returns>
        ILogger CreateLogger( Type t );

        /// <summary>
        ///     Adds the given parameter to the current context of the logging framework.
        /// </summary>
        /// <remarks>
        ///     Use this feature to add dynamic parameters to your log.
        /// </remarks>
        /// <param name="parameters">The parameters to add.</param>
        void SetDynamicParameters( IEnumerable<KeyValuePair<String, String>> parameters );
    }
}