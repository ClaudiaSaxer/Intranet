using System;
using System.Collections.Generic;
using Intranet.Definition;
using Intranet.Definition.Logger;
using NLog;
using ILogger = Intranet.Definition.Logger.ILogger;

namespace Intranet.Common.Logging
{
    /// <summary>
    ///     A NLog loggerFactory factory.
    /// </summary>
    public class NLogLoggerFactory : ILoggerFactory
    {
        #region Implementation of ILoggerFactory

        /// <summary>
        ///     Gets a loggerFactory with the specified name.
        /// </summary>
        /// <param name="loggerName">The name of the loggerFactory.</param>
        /// <returns>A loggerFactory with specified name.</returns>
        public ILogger CreateLogger( String loggerName )
        {
            return new NLogLogger( LogManager.GetLogger( loggerName ) );
        }

        /// <summary>
        ///     Gets a loggerFactory for the specified type.
        /// </summary>
        /// <param name="t">The type of the logging class.</param>
        /// <returns>A loggerFactory for the specified type.</returns>
        public ILogger CreateLogger( Type t )
        {
            return new NLogLogger( LogManager.GetLogger( t.Name ) );
        }

        /// <summary>
        ///     Adds the given parameter to the current context of the logging framework.
        /// </summary>
        /// <remarks>
        ///     Use this feature to add dynamic parameters to your log.
        ///     You can access the parameters using the following syntax: ${gdc:item=myVariableName}
        /// </remarks>
        /// <param name="parameters">The parameters to add.</param>
        public void SetDynamicParameters( IEnumerable<KeyValuePair<String, String>> parameters )
        {
            foreach ( var parameter in parameters )
                GlobalDiagnosticsContext.Set( parameter.Key, parameter.Value );
        }

        #endregion
    }
}