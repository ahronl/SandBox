using System;
using System.Diagnostics;
using System.Reflection;

namespace LogWrapper.Log
{
    public static class LogManager
    {
        private static volatile bool _isEnabled;
        private static Func<string, ILogger> _getLogger;
        private static Func<int, ILogger> _getCurrentClassLogger;
        private static int _skipFrames = 3;
        
        static LogManager()
        {
            Enable();
        }

        public static ILogger GetCurrentClassLogger(int skipFrames = 0)
        {
            return _getCurrentClassLogger(_skipFrames + skipFrames);
        }

        public static ILogger GetLogger(string loggerName)
        {
            return _getLogger(loggerName);
        }

        public static void Disable()
        {
            if(_isEnabled == false) return;

            _isEnabled = false;
            _getLogger = i => new NullObjectLogger();
            _getCurrentClassLogger = s => new NullObjectLogger();
        }
        public static void Enable()
        {
            if (_isEnabled) return;

            _isEnabled = true;
            _getLogger = GetLoggerImpl;
            _getCurrentClassLogger = GetCurrentClassLoggerImpl;
        }

        private static ILogger GetCurrentClassLoggerImpl(int skipFrames = 2)
        {
            string loggerName = GetClassName(skipFrames);
            return GetLogger(loggerName);
        }

        private static string GetClassName(int skipFrames)
        {
            var stack = new StackTrace();
            StackFrame stackFrame = stack.GetFrame(skipFrames);
            MethodBase method = stackFrame.GetMethod();
            return method.DeclaringType != null ? method.DeclaringType.FullName : method.Name;
        }

        private static ILogger GetLoggerImpl(string loggerName)
        {
            return new NlogLogger(loggerName);
        }

        public static void Flush()
        {
            NLog.LogManager.Flush();
        }
    }
}
