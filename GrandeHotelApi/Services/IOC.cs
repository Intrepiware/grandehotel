using GrandeHotel.Lib.Data.Services;
using GrandeHotel.Lib.Data.Services.Impl;
using GrandeHotelApi.Models.Auth;
using GrandeHotelApi.Services.Impl;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeHotelApi.Services
{
    public class IOC
    {
        public static IServiceCollection Register(IServiceCollection collection)
        {
            var devEnvironmentVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

            var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) ||
                                devEnvironmentVariable.ToLower() == "development";

            collection = Base(collection);
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

        private static IServiceCollection Base(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<ITokenService, OktaTokenService>();
            services.Configure<OktaSettings>(Configuration.GetSection("Okta"));
            return services;
        }
    }
}
