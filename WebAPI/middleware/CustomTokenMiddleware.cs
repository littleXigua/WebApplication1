using Microsoft.AspNetCore.Authorization;

public class CustomTokenMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _expectedToken;

    public CustomTokenMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _expectedToken = "123";
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // 跳过特定路径（如健康检查）
        if (context.Request.Path.StartsWithSegments("/health"))
        {
            await _next(context);
            return;
        }

        // 检查是否已通过 Windows 认证
        //if (context.User!=null && context.User.Identity!=null &&!context.User.Identity.IsAuthenticated)
        //{
        //    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        //    return;
        //}

        // 验证自定义 token
        if (!context.Request.Headers.TryGetValue("X-Custom-Token", out var token) ||
            token != _expectedToken)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Invalid or missing custom token");
            return;
        }

        await _next(context);
    }
}


//app.UseMiddleware<CustomTokenMiddleware>();


//// 在 Program.cs 中添加
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("WindowsAndTokenPolicy", policy =>
//    {
//        policy.AddAuthenticationSchemes(NegotiateDefaults.AuthenticationScheme);
//        policy.RequireAuthenticatedUser();
//        policy.Requirements.Add(new CustomTokenRequirement());
//    });
//});

//builder.Services.AddSingleton<IAuthorizationHandler, CustomTokenHandler>();