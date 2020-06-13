using GrandeHotel.Lib.Data.Models;
using GrandeHotel.Lib.Data.Services;
using GrandeHotel.Lib.Services.Security;
using GrandeHotel.Models;
using System.Threading.Tasks;

namespace GrandeHotel.Lib.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IPasswordHashService _passwordHashService;

        public UserService(IUnitOfWorkFactory unitOfWorkFactory,
            IPasswordHashService passwordHashService)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _passwordHashService = passwordHashService;
        }

        public async Task<int> Create(UserCreateModel userModel)
        {
            var user = new User
            {
                Email = userModel.Email,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Password = _passwordHashService.Hash(userModel.CleartextPassword)
            };

            using(var uow = _unitOfWorkFactory.Generate())
            {
                await uow.Users.Add(user);
                await uow.Complete();
                return user.UserId;
            }
        }
    }
}
