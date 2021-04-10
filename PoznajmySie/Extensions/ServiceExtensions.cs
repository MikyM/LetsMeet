using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PoznajmySie.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace PoznajmySie.Extensions
{
    public static class ServiceExtentions
    {
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {
            });
        }

        public static void ConfigureDI(this IServiceCollection services)
        {
            services.AddTransient<ICalendarService, CalendarService>();
        }
    }
}