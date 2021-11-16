using Microsoft.Extensions.Logging;

namespace iMaxSys.Max.Logging
{
    public static class LoggerExtensions
    {
        public static void LogDebug(this ILogger logger, object state)
        {
            logger.Log(LogLevel.Debug, 0, state, null, null);
        }

        public static void LogTrace(this ILogger logger, object state)
        {
            logger.Log(LogLevel.Trace, 0, state, null, null);
        }

        public static void LogInformation(this ILogger logger, object state)
        {
            logger.Log(LogLevel.Information, 0, state, null, null);
        }

        public static void LogWarning(this ILogger logger, object state)
        {
            logger.Log(LogLevel.Warning, 0, state, null, null);
        }

        public static void LogError(this ILogger logger, object state)
        {
            logger.Log(LogLevel.Error, 0, state, null, null);
        }

        public static void LogCritical(this ILogger logger, object state)
        {
            logger.Log(LogLevel.Critical, 0, state, null, null);
        }
    }
}
