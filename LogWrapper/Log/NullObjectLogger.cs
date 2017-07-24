using System;

namespace LogWrapper.Log
{
    internal class NullObjectLogger : ILogger
    {
        public bool IsDebugEnabled => false;

        public bool IsErrorEnabled => false;

        public bool IsFatalEnabled => false;

        public bool IsInfoEnabled => false;

        public bool IsTraceEnabled => false;

        public bool IsWarnEnabled => false;

        public void Debug(object item)
        {
        }

        public void Debug(string message, params object[] args)
        {
        }

        public void Debug(IFormatProvider formatProvider, string message, params object[] args)
        {
        }

        public void Debug(Exception exception, string message = null, params object[] args)
        {
        }

        public void Debug(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
        {
        }

        public void Error(object item)
        {
        }

        public void Error(string message, params object[] args)
        {
        }

        public void Error(IFormatProvider formatProvider, string message, params object[] args)
        {
        }

        public void Error(Exception exception, string message = null, params object[] args)
        {
        }

        public void Error(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
        {
        }

        public void Fatal(object item)
        {
        }

        public void Fatal(string message, params object[] args)
        {
        }

        public void Fatal(IFormatProvider formatProvider, string message, params object[] args)
        {
        }

        public void Fatal(Exception exception, string message = null, params object[] args)
        {
        }

        public void Fatal(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
        {
        }

        public void Info(object item)
        {
        }

        public void Info(string message, params object[] args)
        {
        }

        public void Info(IFormatProvider formatProvider, string message, params object[] args)
        {
        }

        public void Info(Exception exception, string message = null, params object[] args)
        {
        }

        public void Info(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
        {
        }

        public void Log(LogLevel level, object item)
        {
            
        }

        public void Log(LogLevel level, string message, params object[] args)
        {
            
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, params object[] args)
        {
            
        }

        public void Log(LogLevel level, Exception exception, string message = null, params object[] args)
        {
            
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, Exception ex, string message, params object[] args)
        {
            
        }

        public void Log(LogLevel level, Exception exception, IFormatProvider formatProvider, string message, params object[] args)
        {
            
        }

        public void Trace(object item)
        {
        }

        public void Trace(string message, params object[] args)
        {
        }

        public void Trace(IFormatProvider formatProvider, string message, params object[] args)
        {
        }

        public void Trace(Exception exception, string message = null, params object[] args)
        {
        }

        public void Trace(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
        {
        }

        public void Warn(object item)
        {
        }

        public void Warn(string message, params object[] args)
        {
        }

        public void Warn(IFormatProvider formatProvider, string message, params object[] args)
        {
        }

        public void Warn(Exception exception, string message = null, params object[] args)
        {
        }

        public void Warn(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
        {
        }
    }
}