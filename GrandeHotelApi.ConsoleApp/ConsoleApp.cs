using GrandeHotel.Lib.Data.Models;
using GrandeHotel.Lib.Data.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeHotelApi.ConsoleApp
{
    class ConsoleApp
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConsoleApp(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Run()
        {
            // Get the initial list of rooms
            var rooms = await _unitOfWork.Rooms.GetAll();
            Console.WriteLine("Initial list:");
            rooms.ToList().ForEach(r => Console.WriteLine($"{r.RoomId}: {r.Name}"));
            Console.WriteLine();

            // Add a new room
            Room room = new Room
            {
                Name = $"New room {Guid.NewGuid().ToString("n").Substring(0, 6)}",
                NightlyRate = 101
            };
            await _unitOfWork.Rooms.Add(room);
            await _unitOfWork.Complete();

            // Get the final list of rooms
            rooms = await _unitOfWork.Rooms.GetAll();
            Console.WriteLine("Final list:");
            rooms.ToList().ForEach(r => Console.WriteLine($"{r.RoomId}: {r.Name}"));
            Console.WriteLine();
        }
    }
}
