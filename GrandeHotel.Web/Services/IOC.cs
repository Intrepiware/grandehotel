using GrandeHotel.Lib.Data.Services;
using GrandeHotel.Lib.Data.Services.Impl;
using GrandeHotel.Lib.Services.Security;
using GrandeHotel.Lib.Services.Security.Impl;
using GrandeHotel.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GrandeHotel.Web.Services
{
    public static class IOC
    {
        public static IServiceCollection Register(IServiceCollection collection, IConfiguration configuration, bool isDevelopment)
        {
            collection = Base(collection, configuration);
            if (isDevelopment)
                collection = Development(collection);
            else
                collection = Production(collection);

            return collection;
        }

        private static IServiceCollection Development(IServiceCollection services)
        {
            services.AddSingleton<IPasswordHashService, DoNothingPasswordHashService>();
            return services;
        }

        private static IServiceCollection Production(IServiceCollection services)
        {
            services.AddSingleton<IPasswordHashService, BCryptPasswordHashService>();
            return services;
        }

        private static IServiceCollection Base(IServiceCollection services,
            IConfiguration config)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.Configure<AppSettings>(config);
            return services;
        }
    }
}