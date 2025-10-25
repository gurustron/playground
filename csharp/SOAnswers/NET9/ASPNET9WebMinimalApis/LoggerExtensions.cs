namespace Microsoft.Extensions.Logging;

public static class LoggerExtensions
{
    public static void MyLogInformation(this ILogger logger, string? message, params object?[] args)
        => logger.Log(LogLevel.Information, message, args);
}