using GrandeHotel.Lib.Data.Models;
using GrandeHotel.Lib.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeHotelApi.Controllers.api
{
    [Route("/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet(Name = nameof(Get))]
        [ProducesResponseType(200, Type=typeof(IEnumerable<Booking>))]
        public async Task<ActionResult> Get()
        {
            var output = await _unitOfWork.Bookings.GetAll();
            return Ok(output);
        }
    }
}
