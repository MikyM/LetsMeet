using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SpotkajmySie.Services;
using SpotkajmySie.ExceptionMiddleware.Models;
using SpotkajmySie.LoggerService;

namespace SpotkajmySie.Extensions
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