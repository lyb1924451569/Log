using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace SamJan.LogService.Logger
{
    /// <summary>
    /// EF数据库专用日志记录器
    /// </summary>
    public class EFLogger : ILogger
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        private readonly string categoryName;

        public EFLogger(string categoryName) => this.categoryName = categoryName;

        /// <summary>
        /// 是否启用
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel) => true;

        /// <summary>
        /// 日志输出
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="state"></param>
        /// <param name="exception"></param>
        /// <param name="formatter"></param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (categoryName == "Microsoft.EntityFrameworkCore.Database.Command" &&
                logLevel == LogLevel.Information)
            {
                var logContent = formatter(state, exception);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"[TD-{Thread.CurrentThread.ManagedThreadId.ToString()}][{ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] ：");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(logContent);
                Console.ResetColor();
            }
        }

        /// <summary>
        /// 开始域
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState state) => null;
    }
}
