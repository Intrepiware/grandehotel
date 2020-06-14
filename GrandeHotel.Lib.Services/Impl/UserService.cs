using GrandeHotel.Lib.Data.Models;
using GrandeHotel.Lib.Data.Services;
using GrandeHotel.Lib.Services.Security;
using GrandeHotel.Models;
using System.Threading.Tasks;

namespace GrandeHotel.Lib.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHashService _passwordHashService;

        public UserService(IUnitOfWork unitOfWork,
            IPasswordHashService passwordHashService)
        {
            _unitOfWork = unitOfWork;
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

            await _unitOfWork.Users.Add(user);
            await _unitOfWork.Complete();
            return user.UserId;
        }
    }
}
