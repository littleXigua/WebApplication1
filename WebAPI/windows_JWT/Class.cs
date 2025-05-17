using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebAPI
{
    public static class Regisetr01
    {
        public static IServiceCollection RegisetrWindows_jwt(this IServiceCollection service)
        {

            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtOrWindows"; // 自定义组合方案
                options.DefaultChallengeScheme = "JwtOrWindows";
            }).AddPolicyScheme("JwtOrWindows", "JWT or Windows", options =>
            {
                // 根据请求头决定使用哪种认证方式
                options.ForwardDefaultSelector = context =>
                {
                    string authorization = context.Request.Headers["Authorization"];
                    if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
                    {
                        return JwtBearerDefaults.AuthenticationScheme;
                    }
                    return NegotiateDefaults.AuthenticationScheme;
                };
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "Jwt:Issuer",
                    ValidAudience = "Jwt:Audience",
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("Jwt:Key"))
                };
            }).AddNegotiate(NegotiateDefaults.AuthenticationScheme, options =>
            {
                // Windows/Negotiate 认证配置
                options.PersistKerberosCredentials = true;
            });
            return service;
        }
    }
}
