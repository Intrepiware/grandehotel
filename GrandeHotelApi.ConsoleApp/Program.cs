using GrandeHotel.Lib.Data.Models;
using GrandeHotel.Lib.Data.Services;
using GrandeHotel.Lib.Data.Services.Impl;
using GrandeHotelApi.ConsoleApp.Services;
using GrandeHotelApi.ConsoleApp.Services.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace GrandeHotelApi.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = ConfigureServices();

            ServiceProvider provider = services.BuildServiceProvider();

            provider.GetService<ConsoleApp>().RunWithGeneratedUnitOfWork().Wait();
        }

        static IServiceCollection ConfigureServices()
        {
            IServiceCollection serviceProvider = new ServiceCollection();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
#if DEBUG
                .AddJsonFile("appSettings.Development.json", optional: true)
#endif
                .AddEnvironmentVariables();

            IConfigurationRoot config = builder.Build();

            serviceProvider.AddDbContext<GrandeHotelCustomContext>(options => options.UseSqlServer(config.GetConnectionString("grande_hotel")));
            serviceProvider.AddTransient<IUnitOfWork, UnitOfWork>();
            serviceProvider.AddTransient<ConsoleApp>();
            serviceProvider.AddTransient<IUnitOfWorkFactory, UnitOfWorkFactory>();
            return serviceProvider;
        }
    }
}