using GrandeHotel.Lib.Data.Services;
using GrandeHotel.Lib.Data.Services.Impl;
using GrandeHotelApi.Models.Auth;
using GrandeHotelApi.Services.Impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GrandeHotelApi.Services
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
            return services;
        }

        private static IServiceCollection Production(IServiceCollection services)
        {
            return services;
        }

        private static IServiceCollection Base(IServiceCollection services,
            IConfiguration config)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<ITokenService, OktaTokenService>();
            services.Configure<OktaSettings>(config.GetSection("Okta"));
            return services;
        }
    }
}