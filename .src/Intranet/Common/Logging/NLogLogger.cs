using System;
using NLog;

namespace Intranet.Common
{
    /// <summary>
    ///     A NLog loggerFactory.
    /// </summary>
    public class NLogLogger : ILogger
    {
        #region Fields

        /// <summary>
        ///     loggerFactory internally used by this class.
        /// </summary>
        private readonly Logger _internalLogger;

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new <see cref="NLogLogger" />
        /// </summary>
        /// <param name="logger">A NLog loggerFactory</param>
        public NLogLogger( Logger logger )
        {
            _internalLogger = logger;
            DefaultLevel = LogLevel.Debug;
        }

        #endregion

        /// <summary>
        ///     Gets or sets the default log level
        /// </summary>
        /// <value>The default log level</value>
        public LogLevel DefaultLevel { get; set; }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the Debug level.
        /// </summary>
        /// <value>A value of true if logging is enabled for the Debug level, otherwise it returns false.</value>
        public Boolean IsDebugEnabled
        {
            get { return _internalLogger.IsDebugEnabled; }
        }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the Error level.
        /// </summary>
        /// <value>A value of true if logging is enabled for the Error level, otherwise it returns false.</value>
        public Boolean IsErrorEnabled
        {
            get { return _internalLogger.IsDebugEnabled; }
        }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the Fatal level.
        /// </summary>
        /// <value>A value of true if logging is enabled for the Fatal level, otherwise it returns false.</value>
        public Boolean IsFatalEnabled
        {
            get { return _internalLogger.IsDebugEnabled; }
        }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the Info level.
        /// </summary>
        /// <value>A value of true if logging is enabled for the Info level, otherwise it returns false.</value>
        public Boolean IsInfoEnabled
        {
            get { return _internalLogger.IsDebugEnabled; }
        }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the Warn level.
        /// </summary>
        /// <value>A value of true if logging is enabled for the Warn level, otherwise it returns false.</value>
        public Boolean IsWarnEnabled
        {
            get { return _internalLogger.IsDebugEnabled; }
        }

        #region Log

        /// <summary>
        ///     Writes the diagnostic message at the default level.
        /// </summary>
        /// <param name="messageFunc">
        ///     A function returning message to be written. Function is not evaluated if logging is not
        ///     enabled.
        /// </param>
        public void Log( Func<String> messageFunc )
            => _internalLogger.Log( DefaultLevel.ToLogLevel(), new LogMessageGenerator( messageFunc ) );

        /// <summary>
        ///     Writes the diagnostic message at the default level.
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Log( String message )
            => _internalLogger.Log( DefaultLevel.ToLogLevel(), message );

        /// <summary>
        ///     Writes the diagnostic message at the specified level.
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="level">The log level.</param>
        public void Log( String message, LogLevel level )
            => _internalLogger.Log( level.ToLogLevel(), message );

        #endregion Log

        #region Trace

        /// <summary>
        ///     Writes the diagnostic message at the Trace level.
        /// </summary>
        /// <param name="messageFunc">
        ///     A function returning message to be written. Function is not evaluated if logging is not
        ///     enabled.
        /// </param>
        public void Trace( Func<String> messageFunc )
            => _internalLogger.Trace( new LogMessageGenerator( messageFunc ) );

        /// <summary>
        ///     Writes the diagnostic message at the Trace level.
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Trace( String message )
            => _internalLogger.Trace( message );

        #endregion Trace

        #region Debug

        /// <summary>
        ///     Writes the diagnostic message at the Debug level.
        /// </summary>
        /// <param name="messageFunc">
        ///     A function returning message to be written. Function is not evaluated if logging is not
        ///     enabled.
        /// </param>
        public void Debug( Func<String> messageFunc )
            => _internalLogger.Debug( new LogMessageGenerator( messageFunc ) );

        /// <summary>
        ///     Writes the diagnostic message at the Debug level.
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Debug( String message )
            => _internalLogger.Debug( message );

        #endregion Debug

        #region Info

        /// <summary>
        ///     Writes the diagnostic message at the Info level.
        /// </summary>
        /// <param name="messageFunc">
        ///     A function returning message to be written. Function is not evaluated if logging is not
        ///     enabled.
        /// </param>
        public void Info( Func<String> messageFunc )
            => _internalLogger.Info( new LogMessageGenerator( messageFunc ) );

        /// <summary>
        ///     Writes the diagnostic message at the Info level.
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Info( String message )
            => _internalLogger.Info( message );

        #endregion Info

        #region Warn

        /// <summary>
        ///     Writes the diagnostic message at the Warn level.
        /// </summary>
        /// <param name="messageFunc">
        ///     A function returning message to be written. Function is not evaluated if logging is not
        ///     enabled.
        /// </param>
        public void Warn( Func<String> messageFunc )
            => _internalLogger.Warn( new LogMessageGenerator( messageFunc ) );

        /// <summary>
        ///     Writes the diagnostic message at the Warn level.
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Warn( String message )
            => _internalLogger.Warn( message );

        #endregion Warn

        #region Error

        /// <summary>
        ///     Writes the diagnostic message at the Error level.
        /// </summary>
        /// <param name="messageFunc">
        ///     A function returning message to be written. Function is not evaluated if logging is not
        ///     enabled.
        /// </param>
        public void Error( Func<String> messageFunc )
            => _internalLogger.Error( new LogMessageGenerator( messageFunc ) );

        /// <summary>
        ///     Writes the diagnostic message at the Error level.
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Error( String message )
            => _internalLogger.Error( message );

        #endregion Error

        #region Fatal

        /// <summary>
        ///     Writes the diagnostic message at the Fatal level.
        /// </summary>
        /// <param name="messageFunc">
        ///     A function returning message to be written. Function is not evaluated if logging is not
        ///     enabled.
        /// </param>
        public void Fatal( Func<String> messageFunc )
            => _internalLogger.Fatal( new LogMessageGenerator( messageFunc ) );

        /// <summary>
        ///     Writes the diagnostic message at the Fatal level.
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Fatal( String message )
            => _internalLogger.Fatal( message );

        #endregion Fatal
    }
}