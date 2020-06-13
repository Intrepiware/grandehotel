using GrandeHotel.Console.Test.Services;
using GrandeHotel.Lib.Data.Services.Data;
using GrandeHotel.Lib.Data.Services.Data.Impl;
using GrandeHotel.Lib.Services;
using GrandeHotel.Lib.Services.Impl;
using GrandeHotel.Lib.Services.Security;
using GrandeHotel.Lib.Services.Security.Impl;
using SimpleInjector.Lifestyles;
using System.Threading.Tasks;

namespace GrandeHotel.Console.Test
{
    class UserServiceTest
    {
        public static async Task CreateUserNoHash()
        {
            var container = ContainerGenerator.StartNew()
                                .WithDbContext()
                                .WithMockedUnitOfWorkFactory()
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
                    Email = "f@ke.com",
                    FirstName = "Mike",
                    LastName = "Devlin"
                });
            }
        }
    }
}
