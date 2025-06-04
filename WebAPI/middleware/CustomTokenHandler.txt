using Microsoft.AspNetCore.Authorization;

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
            context.Succeed(requirement);

            //var expectedToken = _configuration["CustomToken:ExpectedValue"];

            //if (httpContext.Request.Headers.TryGetValue("X-Custom-Token", out var token) &&
            //    token == expectedToken)
            //{
            //    context.Succeed(requirement);
            //}
        }

        return Task.CompletedTask;
    }
}
