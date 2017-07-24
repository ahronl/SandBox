using System;

namespace LogWrapper.Log
{
    public interface ILogger
    {
        bool IsDebugEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsFatalEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsTraceEnabled { get; }
        bool IsWarnEnabled { get; }
        void Log(LogLevel level, object item);
        void Log(LogLevel level, string message, params object[] args);
        void Log(LogLevel level, Exception exception, string message = null, params object[] args);
        void Log(LogLevel level, IFormatProvider formatProvider, string message, params object[] args);
        void Log(LogLevel level, Exception exception, IFormatProvider formatProvider, string message, params object[] args);
        void Log(LogLevel level, IFormatProvider formatProvider, Exception ex, string message, params object[] args);
        void Debug(object item);
        void Debug(string message, params object[] args);
        void Debug(Exception exception, string message = null, params object[] args);
        void Debug(IFormatProvider formatProvider, string message, params object[] args);
        void Debug(Exception exception, IFormatProvider formatProvider, string message, params object[] args);

        void Trace(object item);
        void Trace(string message, params object[] args);
        void Trace(Exception exception, string message = null, params object[] args);
        void Trace(IFormatProvider formatProvider, string message, params object[] args);
        void Trace(Exception exception, IFormatProvider formatProvider, string message, params object[] args);

        void Info(object item);
        void Info(string message, params object[] args);
        void Info(Exception exception, string message = null, params object[] args);
        void Info(IFormatProvider formatProvider, string message, params object[] args);
        void Info(Exception exception, IFormatProvider formatProvider, string message, params object[] args);

        void Warn(object item);
        void Warn(string message, params object[] args);
        void Warn(Exception exception, string message = null, params object[] args);
        void Warn(IFormatProvider formatProvider, string message, params object[] args);
        void Warn(Exception exception, IFormatProvider formatProvider, string message, params object[] args);

        void Error(object item);
        void Error(string message, params object[] args);
        void Error(Exception exception, string message = null, params object[] args);
        void Error(IFormatProvider formatProvider, string message, params object[] args);
        void Error(Exception exception, IFormatProvider formatProvider, string message, params object[] args);

        void Fatal(object item);
        void Fatal(string message, params object[] args);
        void Fatal(Exception exception, string message = null, params object[] args);
        void Fatal(IFormatProvider formatProvider, string message, params object[] args);
        void Fatal(Exception exception, IFormatProvider formatProvider, string message, params object[] args);
    }
}
