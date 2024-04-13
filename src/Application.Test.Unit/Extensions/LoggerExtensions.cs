using Microsoft.Extensions.Logging;
using NSubstitute;
using Shouldly;

namespace MinefieldBoard.Application.Test.Unit.Extensions
{
    public static class LoggerExtensions
    {
        /// <summary>
        /// Assert log error
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="logLevel"></param>
        /// <param name="errorMessage"></param>
        /// <param name="innerException"></param>
        public static void AssertLoggerEntry(this ILogger logger,
                                             LogLevel logLevel,
                                             string errorMessage,
                                             string innerException = "")
        {
            var calls = logger.ReceivedCalls()
                              .Select(call => new { call, m = call.GetMethodInfo() })
                              .Select(m => new { m, args = m.call.GetArguments() })
                              .Where(t => t.m.m.Name == "Log"
                                          && (LogLevel)t.args[0] == logLevel
                                          && (EventId)t.args[1] == 0
                                          && $"{t.args[2]}".Contains(errorMessage)
                                          && (string.IsNullOrEmpty(innerException)
                                              || $"{t.args[3]}".Contains(innerException)
                                             )
                                    )
                              .Select(t => t.m.call).ToList();

            calls.Count.ShouldBeGreaterThan(0);
        }
    }
}
