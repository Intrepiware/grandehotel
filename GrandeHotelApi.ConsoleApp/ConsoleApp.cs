using GrandeHotel.Lib.Data.Models;
using GrandeHotel.Lib.Data.Services;
using GrandeHotelApi.ConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeHotelApi.ConsoleApp
{
    class ConsoleApp
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfWorkFactory _uowFactory;

        public ConsoleApp(IUnitOfWork unitOfWork,
            IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWork = unitOfWork;
            _uowFactory = unitOfWorkFactory;
        }

        public async Task RunWithGeneratedUnitOfWork()
        {
            using(IUnitOfWork uow = _uowFactory.Generate())
            {
                var rooms = await uow.Rooms.GetAll();
                Console.WriteLine("Initial list:");
                rooms.ToList().ForEach(r => Console.WriteLine($"{r.RoomId}: {r.Name}"));
                Console.WriteLine();
            }

            using(IUnitOfWork uow = _uowFactory.Generate())
            {
                // Add a new room
                Room room = new Room
                {
                    Name = $"New room {Guid.NewGuid().ToString("n").Substring(0, 6)}",
                    NightlyRate = 101
                };
                await uow.Rooms.Add(room);
                await uow.Complete();
            }

            using (IUnitOfWork uow = _uowFactory.Generate())
            {
                var rooms = await uow.Rooms.GetAll();
                Console.WriteLine("Final list:");
                rooms.ToList().ForEach(r => Console.WriteLine($"{r.RoomId}: {r.Name}"));
                Console.WriteLine();
            }
        }

        public async Task RunWithSharedUnitOfWork()
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
