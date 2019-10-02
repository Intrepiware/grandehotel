using GrandeHotel.Lib.Data;
using GrandeHotel.Lib.Data.Models;
using GrandeHotel.Lib.Data.Services.Impl;
using GrandeHotelApi.Models.Api;
using GrandeHotelApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GrandeHotelApi.Controllers.api
{
    [Route("/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly GrandeHotelContext _context;

        public RoomsController(GrandeHotelContext context)
        {
            _context = context;
        }

        [HttpPost(Name = nameof(CreateRoom))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateRoom(RoomPostModel data)
        {
            Rooms room = MappingService.ToRooms(data);
            using(var unitOfWork = new UnitOfWork(_context))
            {
                await unitOfWork.Rooms.Add(room);
                await unitOfWork.Complete();
            }

            return Created(Url.Link(
                nameof(GetRoom),
                new { id = room.RoomId }),
                null);
        }

        [HttpGet("{id}", Name = nameof(GetRoom))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetRoom(Guid id)
        {
            using(var unitOfWork = new UnitOfWork(_context))
            {
                Rooms room = await unitOfWork.Rooms.Get(id);
                if (room == null) return NotFound();
                RoomGetModel output = MappingService.ToRoomGetModel(room);
                return Ok(output);
            }
        }
    }
}
