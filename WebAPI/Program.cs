
using System.Text;
using AspectCore.Extensions.Hosting;
using EasyCaching.Core;
using EasyCaching.InMemory;
using EasyCaching.Interceptor.AspectCore;

using Microsoft.AspNetCore.Authorization;

using WebAPI.services;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Logging.AddConsole(); // Logs to the default console

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();


            builder.Host.UseServiceContext();

            builder.Services.AddScoped<IAspectCoreService, AspectCoreService>();

            builder.Services.AddEasyCaching(options =>
            {
                options.UseInMemory(config =>
                {
                    config.EnableLogging = true;
                });
            });

            builder.Services.AddControllers();

            //1 AspectCore
            builder.Services.ConfigureAspectCoreInterceptor(options => options.CacheProviderName = EasyCachingConstValue.DefaultInMemoryName);

            //builder.Services.AddTransient<ICastleService, CastleService>();

            //2 Castle  
            //builder.Services.ConfigureCastleInterceptor(options => options.CacheProviderName = EasyCachingConstValue.DefaultInMemoryName);

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

          

            app.Logger.LogInformation("App started!");
            app.Run();
        }
    }
}
