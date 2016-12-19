#region Usings

using System;

#endregion

namespace Intranet.Common
{
    /// <summary>
    ///     Class containing all NLog extensions
    /// </summary>
    public static class NLogExtensions
    {
        /// <summary>
        ///     Convert a <see cref="LogLevel" /> value to a <see cref="NLog.LogLevel" /> value
        /// </summary>
        /// <param name="level">The level to convert</param>
        /// <exception cref="ArgumentException">level is out of range.</exception>
        /// <returns>The converted NLog value</returns>
        public static NLog.LogLevel ToLogLevel( this LogLevel level )
        {
            switch ( level )
            {
                case LogLevel.Debug:
                    return NLog.LogLevel.Debug;

                case LogLevel.Error:
                    return NLog.LogLevel.Error;

                case LogLevel.Fatal:
                    return NLog.LogLevel.Fatal;

                case LogLevel.Info:
                    return NLog.LogLevel.Info;

                case LogLevel.Off:
                    return NLog.LogLevel.Off;

                case LogLevel.Trace:
                    return NLog.LogLevel.Trace;

                case LogLevel.Warn:
                    return NLog.LogLevel.Warn;

                default:
                    //Not testable with unit tests
                    throw new ArgumentOutOfRangeException( nameof( level ),
                                                           level,
                                                           $"{level} is not a supported NLog log level (you may have to add a transformation for it)" );
            }
        }
    }
}