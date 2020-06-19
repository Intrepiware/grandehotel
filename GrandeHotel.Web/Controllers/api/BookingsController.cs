using GrandeHotel.Lib.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GrandeHotel.Web.Controllers.api
{
    [Route("/api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var output = await _unitOfWork.Bookings.GetAll();
            return Ok(output);
        }
    }
}
