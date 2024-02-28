namespace TaskManager.Api.Middlewares.Authorization;

public class TokenAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    private readonly AuthorizationOptions _authOptions;

    public TokenAuthorizationMiddleware(RequestDelegate next,
        AuthorizationOptions authOptions)
    {
        _next = next;
        _authOptions = authOptions;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (IsSwaggerPath(context))
        {
            await _next(context).ConfigureAwait(false);
            return;
        }

        var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
        if (token == null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized: Token is missing.").ConfigureAwait(false);
            return;
        }

        if (IsTokenInvalid(token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized: Invalid token.").ConfigureAwait(false);
        }

        await _next(context).ConfigureAwait(false);
    }

    private bool IsTokenInvalid(string token) => token == _authOptions.Token;

    private bool IsSwaggerPath(HttpContext context) => context.Request.Path.StartsWithSegments("/swagger");
}