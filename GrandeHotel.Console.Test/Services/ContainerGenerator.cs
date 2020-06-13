using GrandeHotel.Lib.Data.Models;
using GrandeHotel.Lib.Data.Services;
using GrandeHotel.Lib.Data.Services.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace GrandeHotel.Console.Test.Services
{
    internal class ContainerGenerator
    {
        public static ContainerGenerator StartNew() => new ContainerGenerator();

        private Container _container;

        private IConfiguration _configuration;

        public ContainerGenerator()
        {
            _container = new Container();
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
        }

        public ContainerGenerator WithMockedUnitOfWorkFactory()
        {
            var uowFactory = Substitute.For<IUnitOfWorkFactory>();
            uowFactory.Generate().Returns(_ => _container.GetInstance<IUnitOfWork>());
            _container.RegisterInstance(uowFactory);
            _container.Register<IUnitOfWork>(() => new UnitOfWork(_container.GetInstance<GrandeHotelCustomContext>()), Lifestyle.Scoped);
            return this;
        }

        public ContainerGenerator WithDbContext()
        {
            _configuration = _configuration ?? GetConfiguration();
            var options = new DbContextOptionsBuilder<GrandeHotelContext>()
                .UseSqlServer(_configuration.GetConnectionString("grande_hotel"))
                .Options;
            var context = new GrandeHotelCustomContext(options);
            _container.RegisterInstance<DbContext>(context);
            _container.RegisterInstance<GrandeHotelCustomContext>(context);
            return this;
        }

        public Container Generate() => _container;

        private IConfiguration GetConfiguration() =>
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
    }
}