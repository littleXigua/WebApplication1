
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

         

            builder.Services.AddAuthentication(options =>
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
                                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                                ValidAudience = builder.Configuration["Jwt:Audience"],
                                IssuerSigningKey = new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                            };
                        }).AddNegotiate(NegotiateDefaults.AuthenticationScheme, options =>
                        {
                            // Windows/Negotiate 认证配置
                            options.PersistKerberosCredentials = true;
                        });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
