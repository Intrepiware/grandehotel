using GrandeHotel.Lib.Data.Models;
using GrandeHotel.Lib.Data.Services.Repository.Impl;

namespace GrandeHotel.Lib.Data.Services.Data.Impl
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(Microsoft.EntityFrameworkCore.DbContext context) : base(context)
        {
        }
    }
}
