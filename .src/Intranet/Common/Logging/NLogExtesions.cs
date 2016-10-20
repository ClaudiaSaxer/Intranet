using System;

namespace Intranet.Common.Logging
{
    /// <summary>
    ///     Class containing all NLog extensions
    /// </summary>
    public static class NLogExtensions
    {
        /// <summary>
        ///     Convert a <see cref="Definition.LogLevel" /> value to a <see cref="NLog.LogLevel" /> value
        /// </summary>
        /// <param name="level">The level to convert</param>
        /// <exception cref="ArgumentException">level is out of range.</exception>
        /// <returns>The converted NLog value</returns>
        public static LogLevel ToLogLevel( this Definition.LogLevel level )
        {
            switch ( level )
            {
                case Definition.LogLevel.Debug:
                    return LogLevel.Debug;

                case Definition.LogLevel.Error:
                    return LogLevel.Error;

                case Definition.LogLevel.Fatal:
                    return LogLevel.Fatal;

                case Definition.LogLevel.Info:
                    return LogLevel.Info;

                case Definition.LogLevel.Off:
                    return LogLevel.Off;

                case Definition.LogLevel.Trace:
                    return LogLevel.Trace;

                case Definition.LogLevel.Warn:
                    return LogLevel.Warn;

                default:
                    //Not testable with unit tests
                    throw new ArgumentOutOfRangeException( nameof( level ),
                                                           level,
                                                           $"{level} is not a supported NLog log level (you may have to add a transformation for it)" );
            }
        }
    }
}