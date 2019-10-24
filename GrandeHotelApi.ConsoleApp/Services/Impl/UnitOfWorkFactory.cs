using System;
using System.Collections.Generic;
using System.Text;
using GrandeHotel.Lib.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GrandeHotelApi.ConsoleApp.Services.Impl
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory, IDisposable
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private List<IDisposable> _toBeDisposed = new List<IDisposable>();

        public UnitOfWorkFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        
        public IUnitOfWork Generate()
        {
            IServiceScope scope = _serviceScopeFactory.CreateScope();
            IUnitOfWork output = scope.ServiceProvider.GetService<IUnitOfWork>();
            _toBeDisposed.Add(scope);
            _toBeDisposed.Add(output);
            return output;
        }

        public void Dispose()
        {
            _toBeDisposed.ForEach(obj => obj.Dispose());
        }
    }
}
