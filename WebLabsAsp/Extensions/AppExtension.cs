using WebLabsAsp.Middleware;

namespace WebLabsAsp.Extensions;

public static class AppExtension
{
    public static IApplicationBuilder UseLogging(this IApplicationBuilder app)
    {
        return app.UseMiddleware<LogMiddleware>();
    }
}