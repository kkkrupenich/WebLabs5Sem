namespace WebLabsAsp.Extensions;

public static class RequestExtension
{
    public static bool IsAjax(this HttpRequest request)
    {
        return request.Headers["x-requested-with"].Equals("XMLHttpRequest");
    }
}