using System;
using System.Globalization;
using NLog;

namespace LogWrapper.Log
{
    internal class NlogLogger : ILogger
    {
        private readonly NLog.ILogger _logger;

        internal NlogLogger(string name)
        {
            _logger = NLog.LogManager.GetLogger(name);
        }

        public bool IsDebugEnabled => _logger.IsDebugEnabled;
        public bool IsErrorEnabled => _logger.IsErrorEnabled;
        public bool IsFatalEnabled => _logger.IsFatalEnabled;
        public bool IsInfoEnabled => _logger.IsInfoEnabled;
        public bool IsTraceEnabled => _logger.IsTraceEnabled;
        public bool IsWarnEnabled => _logger.IsWarnEnabled;

        public void Log(LogLevel logLevel,object item)
        {
            Log(logLevel, CultureInfo.CurrentCulture, null, item.ToString());
        }

        public void Log(LogLevel logLevel,string message, params object[] args)
        {
            Log(logLevel, CultureInfo.CurrentCulture, null, message, args);
        }

        public void Log(LogLevel logLevel, Exception exception, string message = null, params object[] args)
        {
            Log(logLevel, CultureInfo.CurrentCulture, exception, message, args);
        }

        public void Log(LogLevel logLevel, IFormatProvider formatProvider, string message, params object[] args)
        {
            Log(logLevel, formatProvider, null, message, args);
        }

        public void Log(LogLevel logLevel, Exception exception, IFormatProvider formatProvider, string message, params object[] args)
        {
            Log(logLevel, formatProvider, exception, message, args);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, Exception ex, string message, params object[] args)
        {
            var logEvent = CreateEventInfo(level, formatProvider, message, args);
            logEvent.Exception = ex;
            _logger.Log(typeof(NlogLogger), logEvent);
        }
        private LogEventInfo CreateEventInfo(LogLevel level, IFormatProvider formatProvider, string message, params object[] args)
        {
            return new LogEventInfo(Convert(level), _logger.Name, formatProvider, message, args);
        }

        private NLog.LogLevel Convert(LogLevel loglevel)
        {
            switch (loglevel)
            {
                case LogLevel.Off:
                    return NLog.LogLevel.Off;
                case LogLevel.Trace:
                    return NLog.LogLevel.Trace;
                case LogLevel.Debug:
                    return NLog.LogLevel.Debug;
                case LogLevel.Info:
                    return NLog.LogLevel.Info;
                case LogLevel.Warn:
                    return NLog.LogLevel.Info;
                case LogLevel.Error:
                    return NLog.LogLevel.Error;
                case LogLevel.Fatal:
                    return NLog.LogLevel.Fatal;
                default:
                    return NLog.LogLevel.Off;
            }
        }

        public void Debug(object item)
        {
            Log(LogLevel.Debug, CultureInfo.CurrentCulture, null, item.ToString());
        }

        public void Debug(string message, params object[] args)
        {
            Log(LogLevel.Debug, CultureInfo.CurrentCulture, null, message, args);
        }

        public void Debug(Exception exception, string message = null, params object[] args)
        {
            Log(LogLevel.Debug, CultureInfo.CurrentCulture, exception, message, args);
        }

        public void Debug(IFormatProvider formatProvider, string message, params object[] args)
        {
            Log(LogLevel.Debug, formatProvider, null, message, args);
        }

        public void Debug(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
        {
            Log(LogLevel.Debug, formatProvider, exception, message, args);
        }

        public void Trace(object item)
        {
            Log(LogLevel.Trace, CultureInfo.CurrentCulture, null, item.ToString());
        }

        public void Trace(string message, params object[] args)
        {
            Log(LogLevel.Trace, CultureInfo.CurrentCulture, null, message, args);
        }

        public void Trace(Exception exception, string message = null, params object[] args)
        {
            Log(LogLevel.Trace, CultureInfo.CurrentCulture, exception, message, args);
        }

        public void Trace(IFormatProvider formatProvider, string message, params object[] args)
        {
            Log(LogLevel.Trace, formatProvider, null, message, args);
        }

        public void Trace(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
        {
            Log(LogLevel.Trace, formatProvider, exception, message, args);
        }

        public void Info(object item)
        {
            Log(LogLevel.Info, CultureInfo.CurrentCulture, null, item.ToString());
        }

        public void Info(string message, params object[] args)
        {
            Log(LogLevel.Info, CultureInfo.CurrentCulture, null, message, args);
        }

        public void Info(Exception exception, string message = null, params object[] args)
        {
            Log(LogLevel.Info, CultureInfo.CurrentCulture, exception, message, args);
        }

        public void Info(IFormatProvider formatProvider, string message, params object[] args)
        {
            Log(LogLevel.Info, formatProvider, null, message, args);
        }

        public void Info(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
        {
            Log(LogLevel.Info, formatProvider, exception, message, args);
        }

        public void Warn(object item)
        {
            Log(LogLevel.Warn, CultureInfo.CurrentCulture, null, item.ToString());
        }

        public void Warn(string message, params object[] args)
        {
            Log(LogLevel.Warn, CultureInfo.CurrentCulture, null, message, args);
        }

        public void Warn(Exception exception, string message, params object[] args)
        {
            Log(LogLevel.Warn, CultureInfo.CurrentCulture, exception, message, args);
        }

        public void Warn(IFormatProvider formatProvider, string message, params object[] args)
        {
            Log(LogLevel.Warn, formatProvider, null, message, args);
        }

        public void Warn(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
        {
            Log(LogLevel.Warn, formatProvider, exception, message, args);
        }

        public void Error(object item)
        {
            Log(LogLevel.Error, CultureInfo.CurrentCulture, null, item.ToString());
        }

        public void Error(string message, params object[] args)
        {
            Log(LogLevel.Error, CultureInfo.CurrentCulture, null, message, args);
        }

        public void Error(Exception exception, string message, params object[] args)
        {
            Log(LogLevel.Error, CultureInfo.CurrentCulture, exception, message, args);
        }

        public void Error(IFormatProvider formatProvider, string message, params object[] args)
        {
            Log(LogLevel.Error, formatProvider, null, message, args);
        }

        public void Error(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
        {
            Log(LogLevel.Error, formatProvider, exception, message, args);
        }

        public void Fatal(object item)
        {
            Log(LogLevel.Fatal, CultureInfo.CurrentCulture, null, item.ToString());
        }

        public void Fatal(string message, params object[] args)
        {
            Log(LogLevel.Fatal, CultureInfo.CurrentCulture, null, message, args);
        }

        public void Fatal(Exception exception, string message, params object[] args)
        {
            Log(LogLevel.Fatal, CultureInfo.CurrentCulture, exception, message, args);
        }

        public void Fatal(IFormatProvider formatProvider, string message, params object[] args)
        {
            Log(LogLevel.Fatal, formatProvider, null, message, args);
        }

        public void Fatal(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
        {
            Log(LogLevel.Fatal, formatProvider, exception, message, args);
        }
    }
}
