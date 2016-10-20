#region Usings

using System;

#endregion

namespace Intranet.Common
{
    /// <summary>
    ///     Interface representing a loggerFactory
    /// </summary>
    public interface ILogger
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the default log level
        /// </summary>
        /// <value>The default log level</value>
        LogLevel DefaultLevel { get; set; }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the Debug level.
        /// </summary>
        /// <value>A value of true if logging is enabled for the Debug level, otherwise it returns false.</value>
        Boolean IsDebugEnabled { get; }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the Error level.
        /// </summary>
        /// <value>A value of true if logging is enabled for the Error level, otherwise it returns false.</value>
        Boolean IsErrorEnabled { get; }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the Fatal level.
        /// </summary>
        /// <value>A value of true if logging is enabled for the Fatal level, otherwise it returns false.</value>
        Boolean IsFatalEnabled { get; }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the Info level.
        /// </summary>
        /// <value>A value of true if logging is enabled for the Info level, otherwise it returns false.</value>
        Boolean IsInfoEnabled { get; }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the Warn level.
        /// </summary>
        /// <value>A value of true if logging is enabled for the Warn level, otherwise it returns false.</value>
        Boolean IsWarnEnabled { get; }

        #endregion

        #region Log

        /// <summary>
        ///     Writes the diagnostic message at the default level.
        /// </summary>
        /// <param name="messageFunc">
        ///     A function returning message to be written. Function is not evaluated if logging is not
        ///     enabled.
        /// </param>
        void Log( Func<String> messageFunc );

        /// <summary>
        ///     Writes the diagnostic message at the default level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void Log( String message );

        /// <summary>
        ///     Writes the diagnostic message at the specified level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="level">The log level.</param>
        void Log( String message, LogLevel level );

        #endregion

        #region Trace

        /// <summary>
        ///     Writes the diagnostic message at the Trace level.
        /// </summary>
        /// <param name="messageFunc">
        ///     A function returning message to be written. Function is not evaluated if logging is not
        ///     enabled.
        /// </param>
        void Trace( Func<String> messageFunc );

        /// <summary>
        ///     Writes the diagnostic message at the Trace level.
        /// </summary>
        /// <param name="message">The message to log</param>
        void Trace( String message );

        #endregion Debug

        #region Debug

        /// <summary>
        ///     Writes the diagnostic message at the Debug level.
        /// </summary>
        /// <param name="messageFunc">
        ///     A function returning message to be written. Function is not evaluated if logging is not
        ///     enabled.
        /// </param>
        void Debug( Func<String> messageFunc );

        /// <summary>
        ///     Writes the diagnostic message at the Debug level.
        /// </summary>
        /// <param name="message">The message to log</param>
        void Debug( String message );

        #endregion Debug

        #region Info

        /// <summary>
        ///     Writes the diagnostic message at the Info level.
        /// </summary>
        /// <param name="messageFunc">
        ///     A function returning message to be written. Function is not evaluated if logging is not
        ///     enabled.
        /// </param>
        void Info( Func<String> messageFunc );

        /// <summary>
        ///     Writes the diagnostic message at the Info level.
        /// </summary>
        /// <param name="message">The message to log</param>
        void Info( String message );

        #endregion Info

        #region Warn

        /// <summary>
        ///     Writes the diagnostic message at the Warn level.
        /// </summary>
        /// <param name="messageFunc">
        ///     A function returning message to be written. Function is not evaluated if logging is not
        ///     enabled.
        /// </param>
        void Warn( Func<String> messageFunc );

        /// <summary>
        ///     Writes the diagnostic message at the Warn level.
        /// </summary>
        /// <param name="message">The message to log</param>
        void Warn( String message );

        #endregion Warn

        #region Error

        /// <summary>
        ///     Writes the diagnostic message at the Error level.
        /// </summary>
        /// <param name="messageFunc">
        ///     A function returning message to be written. Function is not evaluated if logging is not
        ///     enabled.
        /// </param>
        void Error( Func<String> messageFunc );

        /// <summary>
        ///     Writes the diagnostic message at the Error level.
        /// </summary>
        /// <param name="message">The message to log</param>
        void Error( String message );

        #endregion Error

        #region Fatal

        /// <summary>
        ///     Writes the diagnostic message at the Fatal level.
        /// </summary>
        /// <param name="messageFunc">
        ///     A function returning message to be written. Function is not evaluated if logging is not
        ///     enabled.
        /// </param>
        void Fatal( Func<String> messageFunc );

        /// <summary>
        ///     Writes the diagnostic message at the Fatal level.
        /// </summary>
        /// <param name="message">The message to log</param>
        void Fatal( String message );

        #endregion Fatal
    }
}