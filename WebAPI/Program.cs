
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


            //builder.Services.AddScoped<IAspectCoreService, AspectCoreService>();


            builder.Services.AddScoped<IStudentService, StudentService>();

            builder.Services.AddEasyCaching(options =>
            {
                // ÄÚ´æ»º´æ
                options.UseInMemory("m1");

            });



            // ÅäÖÃÀ¹½ØÆ÷
            builder.Services.ConfigureAspectCoreInterceptor(options =>
            {
                options.CacheProviderName = "m1";
                //options.CacheProviderName
            });


            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
                       

            //app.UseMiddleware<CustomTokenMiddleware>();

            app.MapControllers();

            //app.UseEasyCaching();

            app.Run();
        }

      

    }
}
