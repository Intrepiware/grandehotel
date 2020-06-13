using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrandeHotel.Lib.Data.Services.Impl
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
            var scope = _serviceScopeFactory.CreateScope();
            var output = scope.ServiceProvider.GetService<IUnitOfWork>();
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
