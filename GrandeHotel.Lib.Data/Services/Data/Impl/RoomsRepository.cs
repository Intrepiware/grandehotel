using GrandeHotel.Lib.Data.Models;
using GrandeHotel.Lib.Data.Services.Repository.Impl;

namespace GrandeHotel.Lib.Data.Services.Data.Impl
{
    public class RoomsRepository : Repository<Rooms>, IRoomsRepository
    {
        public RoomsRepository(Microsoft.EntityFrameworkCore.DbContext context) : base(context)
        {
        }
    }
}
