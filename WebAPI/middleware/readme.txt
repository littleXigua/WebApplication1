

csharp
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// 添加自定义 token 验证中间件
app.UseMiddleware<CustomTokenMiddleware>();

// 其他中间件...
app.MapControllers();
app.Run();





csharp
// 在 Program.cs 中添加
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("WindowsAndTokenPolicy", policy =>
    {
        policy.AddAuthenticationSchemes(NegotiateDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
        policy.Requirements.Add(new CustomTokenRequirement());
    });
});

builder.Services.AddSingleton<IAuthorizationHandler, CustomTokenHandler>();
然后创建要求和处理器：


public class CustomTokenRequirement : IAuthorizationRequirement { }

public class CustomTokenHandler : AuthorizationHandler<CustomTokenRequirement>
{
    private readonly IConfiguration _configuration;

    public CustomTokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        CustomTokenRequirement requirement)
    {
        if (context.Resource is HttpContext httpContext)
        {
            var expectedToken = _configuration["CustomToken:ExpectedValue"];
            
            if (httpContext.Request.Headers.TryGetValue("X-Custom-Token", out var token) && 
                token == expectedToken)
            {
                context.Succeed(requirement);
            }
        }

        return Task.CompletedTask;
    }
}



然后在控制器或端点中使用：

csharp
[Authorize(Policy = "WindowsAndTokenPolicy")]
[HttpGet("secure-data")]
public IActionResult GetSecureData()
{
    return Ok("This is secure data accessible only with Windows auth and valid token");
}
5. 配置注意事项
确保在 IIS 或 Kestrel 中正确配置了 Windows 认证

在生产环境中，自定义 token 应该存储在安全的地方（如 Azure Key Vault）

考虑使用 HTTPS 来保护 token 传输

这种双重验证方式既利用了 Windows 域认证的安全性，又通过自定义 token 提供了额外的保护层