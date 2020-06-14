using GrandeHotel.Console.Test.Services;
using GrandeHotel.Lib.Data.Services.Data;
using GrandeHotel.Lib.Data.Services.Data.Impl;
using GrandeHotel.Lib.Services;
using GrandeHotel.Lib.Services.Impl;
using GrandeHotel.Lib.Services.Security;
using GrandeHotel.Lib.Services.Security.Impl;
using SimpleInjector.Lifestyles;
using System;
using System.Threading.Tasks;

namespace GrandeHotel.Console.Test
{
    class UserServiceTest
    {
        public static async Task CreateUserNoHash()
        {
            var container = ContainerGenerator.StartNew()
                                .WithDbContext()
                                .WithUnitOfWork()
                                .Generate();
            
            container.Register<IUserRepository, UserRepository>();
            container.Register<IPasswordHashService, DoNothingPasswordHashService>();
            container.Register<UserService>();

            using (var scope = AsyncScopedLifestyle.BeginScope(container))
            {
                var userService = container.GetInstance<UserService>();

                await userService.Create(new Models.UserCreateModel
                {
                    CleartextPassword = "password",
                    Email = $"f@ke-{Guid.NewGuid().ToString("n").Substring(0, 6)}.com",
                    FirstName = "Mike",
                    LastName = "Devlin"
                });
                await scope.DisposeAsync();
            }
        }

        public static async Task CreateUserWithHash()
        {
            var container = ContainerGenerator.StartNew()
                                .WithDbContext()
                                .WithUnitOfWork()
                                .Generate();

            container.Register<IUserRepository, UserRepository>();
            container.Register<IPasswordHashService, BCryptPasswordHashService>();
            container.Register<UserService>();
            container.Register<AuthenticationService>();

            var email = $"f@ke-{Guid.NewGuid().ToString("n").Substring(0, 6)}.com";

            using (var scope = AsyncScopedLifestyle.BeginScope(container))
            {
                var userService = container.GetInstance<UserService>();

                await userService.Create(new Models.UserCreateModel
                {
                    CleartextPassword = "password",
                    Email = email,
                    FirstName = "Mike",
                    LastName = "Devlin"
                });
                await scope.DisposeAsync();
            }

            using(var scope = AsyncScopedLifestyle.BeginScope(container))
            {
                var authService = container.GetInstance<AuthenticationService>();
                var happyResult = await authService.Authenticate(email, "password");
                System.Console.WriteLine($"Happy result: {happyResult}");

                var sadResult = await authService.Authenticate(email, "");
                System.Console.WriteLine($"Sad result: {sadResult}");
            }
        }
    }
}
