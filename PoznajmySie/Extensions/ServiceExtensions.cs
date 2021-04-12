using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PoznajmySie.Services;
using PoznajmySie.ExceptionMiddleware.Models;
using PoznajmySie.LoggerService;

namespace PoznajmySie.Extensions
{
    public static class ServiceExtentions
    {
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options => {
            });
        }

        public static void ConfigureDI(this IServiceCollection services)
        {
            services.AddTransient<ICalendarService, CalendarService>();
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}