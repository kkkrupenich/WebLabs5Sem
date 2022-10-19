using Microsoft.Build.Logging;

namespace WebLabsAsp.Middleware;

public class LogMiddleware 
{
    private readonly RequestDelegate _next;
    public LogMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILoggerFactory factory)
    {
        var time = DateTime.Now;  

        await _next(context);
            
        var statusCode = context.Response.StatusCode;
        if (statusCode != StatusCodes.Status200OK)
        {
            var logger = factory.CreateLogger<FileLogger>();
            logger.LogInformation("asd");
        }
    }        
}   