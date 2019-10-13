using System;

namespace GrandeHotel.Lib.Data.Models.Exceptions
{
    public class BookingException : ApplicationException
    {
        public BookingException(string message): base(message)
        { }

        public BookingException() : base()
        {
        }

        public BookingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
