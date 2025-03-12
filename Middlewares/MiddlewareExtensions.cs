using Microsoft.AspNetCore.Builder;

namespace ASP.NET_MVC.Middleware;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingMiddleware>();
    }
}