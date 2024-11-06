using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

public class ErrorLoggingFilter : IExceptionFilter
{
    private readonly ILogger<ErrorLoggingFilter> _logger;

    public ErrorLoggingFilter(ILogger<ErrorLoggingFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "An error occurred.");
    }
}
