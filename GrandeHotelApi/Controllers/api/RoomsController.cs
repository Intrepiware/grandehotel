using GrandeHotel.Lib.Data.Models;
using GrandeHotel.Lib.Data.Services;
using GrandeHotelApi.Models.Api;
using GrandeHotelApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeHotelApi.Controllers.api
{
    [Route("/[controller]")]
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
        [HttpPost(Name = nameof(CreateRoom))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateRoom([FromBody]RoomPostModel data)
        {
            Room room = MappingService.ToRoom(data);
            await _unitOfWork.Rooms.Add(room);
            await _unitOfWork.Complete();

            return Created(Url.Link(
                nameof(GetRoom),
                new { id = room.RoomId }),
                null);
        }

        /// <summary>
        /// Retrieves an individual room
        /// </summary>
        /// <param name="id">Room Id</param>
        /// <returns>The room record</returns>
        [HttpGet("{id}", Name = nameof(GetRoom))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type=typeof(RoomGetModel))]
        public async Task<ActionResult> GetRoom(Guid id)
        {
            Room room = await _unitOfWork.Rooms.Get(id);
            if (room == null) return NotFound();
            RoomGetModel output = MappingService.ToRoomGetModel(room);
            return Ok(output);
        }

        /// <summary>
        /// Retrieves a list of all rooms on the property
        /// </summary>
        /// <returns>A list of rooms</returns>
        [HttpGet(Name = nameof(GetRooms))]
        [ProducesResponseType(200, Type=typeof(List<RoomGetModel>))]
        public async Task<ActionResult> GetRooms()
        {
            IEnumerable<Room> rooms = await _unitOfWork.Rooms.GetAll();
            List<RoomGetModel> output = rooms.Select(MappingService.ToRoomGetModel).ToList();
            return Ok(output);
        }

        [HttpGet("Availabilities", Name = nameof(GetRoomAvailabilities))]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetRoomAvailabilities()
        {
            IList<RoomAvailability> availabilities = await _unitOfWork.Rooms.GetRoomAvailabilities(30, 720);
            return Ok(availabilities);
        }
    }
}
