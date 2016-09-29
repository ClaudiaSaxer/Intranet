namespace Intranet.Definition.Logger
{
    /// <summary>
    ///     Enumeration of all available log levels.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        ///     Trace log level.
        /// </summary>
        /// <remarks>
        ///     Very detailed log messages, potentially of a high frequency and volume.
        /// </remarks>
        Trace = 0,

        /// <summary>
        ///     Debug log level.
        /// </summary>
        /// <remarks>
        ///     Less detailed and/or less frequent debugging messages.
        /// </remarks>
        Debug = 1,

        /// <summary>
        ///     Info log level.
        /// </summary>
        /// <remarks>
        ///     Informational messages.
        /// </remarks>
        Info = 2,

        /// <summary>
        ///     Warn log level.
        /// </summary>
        /// <remarks>
        ///     Warnings which don't appear to the user of the application.
        /// </remarks>
        Warn = 3,

        /// <summary>
        ///     Error log level.
        /// </summary>
        /// <remarks>
        ///     Error messages.
        /// </remarks>
        Error = 4,

        /// <summary>
        ///     Fatal log level.
        /// </summary>
        /// <remarks>
        ///     Fatal error messages. After a fatal error, the application usually terminates.
        /// </remarks>
        Fatal = 5,

        /// <summary>
        ///     Logging is disabled.
        /// </summary>
        /// <remarks>
        ///     Logging is not enabled for any level.
        /// </remarks>
        Off = 6
    }
}