using System;
using System.Collections.Generic;
using System.Text;

namespace GrandeHotel.Models
{
    public class UserCreateModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CleartextPassword { get; set; }
    }
}
