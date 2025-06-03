
using System.Text;
using EasyCaching.InMemory;
using EasyCaching.Interceptor.AspectCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebAPI.services;

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
            builder.Services.AddSwaggerGen(
                options =>
                {

                    //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    //{
                    //    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    //    Name = "Authorization",
                    //    In = ParameterLocation.Header,
                    //    Type = SecuritySchemeType.ApiKey,
                    //    Scheme = "Bearer"
                    //});


                    //options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    //{
                    //    {
                    //        new OpenApiSecurityScheme
                    //        {
                    //            Reference = new OpenApiReference
                    //            {
                    //                Type = ReferenceType.SecurityScheme,
                    //                Id = "Bearer"
                    //            }
                    //        },
                    //        new string[] {}
                    //    }
                    //});       


                });

            builder.Services.AddScoped<IRsaKeyService, RsaKeyService>();

        
            //builder.Services.Regisetrjwt();

            builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();

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


            builder.Services.AddEasyCaching(options =>
            {
                // 使用内存缓存
                options.UseInMemory(config =>
                {
                    config.DBConfig = new InMemoryCachingOptions
                    {
                        // 内存缓存大小限制
                        SizeLimit = 1000
                    };
                    // 启用日志
                    config.EnableLogging = true;
                   
                }, "default");
            });

           

            // 配置拦截器
            builder.Services.ConfigureAspectCoreInterceptor(options =>
            {
                options.CacheProviderName = "default";
            });

            builder.Services.AddTransient<IStudentService, StudentService>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

           

            app.UseMiddleware<CustomTokenMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
