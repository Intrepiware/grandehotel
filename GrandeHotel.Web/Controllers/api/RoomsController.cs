using GrandeHotel.Lib.Data.Services;
using GrandeHotel.Web.Models.Api;
using GrandeHotel.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeHotel.Web.Controllers.api
{
    [Route("/api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates a new room
        /// </summary>
        /// <param name="data">The room data</param>
        /// <returns>No content; Location header contains the url for the new room</returns>
        [HttpPost]
        public async Task<ActionResult> CreateRoom([FromBody]RoomPostModel data)
        {
            var room = MappingService.ToRoom(data);
            await _unitOfWork.Rooms.Add(room);
            await _unitOfWork.Complete();

            return Created(Url.Link(
                "Rooms_GetRoom",
                new { id = room.RoomId }),
                null);
        }

        /// <summary>
        /// Retrieves an individual room
        /// </summary>
        /// <param name="id">Room Id</param>
        /// <returns>The room record</returns>
        [HttpGet("{id}", Name = "Rooms_GetRoom")]
        public async Task<ActionResult> GetRoom(Guid id)
        {
            var room = await _unitOfWork.Rooms.Get(id);
            if (room == null) return NotFound();
            var output = MappingService.ToRoomGetModel(room);
            return Ok(output);
        }

        /// <summary>
        /// Retrieves a list of all rooms on the property
        /// </summary>
        /// <returns>A list of rooms</returns>
        [HttpGet]
        public async Task<ActionResult> GetRooms()
        {
            var rooms = await _unitOfWork.Rooms.GetAll();
            var output = rooms.Select(MappingService.ToRoomGetModel).ToList();
            return Ok(output);
        }

        [HttpGet("Availabilities")]
        public async Task<ActionResult> GetRoomAvailabilities()
        {
            var availabilities = await _unitOfWork.Rooms.GetRoomAvailabilities(30, 720);
            return Ok(availabilities);
        }
    }
}
