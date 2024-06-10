namespace ChineseSaleServer.Middleware;
public static class BearerTokenMiddlewareExtensions
{
    public static IApplicationBuilder UseBearerTokenMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<BearerTokenMiddleware>();
    }
}