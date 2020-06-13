using System;
using SysCon = System.Console;

namespace GrandeHotel.Console.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            UserServiceTest.CreateUserNoHash().Wait();
        }
    }
}
