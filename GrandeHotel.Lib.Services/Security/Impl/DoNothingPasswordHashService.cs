namespace GrandeHotel.Lib.Services.Security.Impl
{
    public class DoNothingPasswordHashService : IPasswordHashService
    {
        public string Hash(string password) => password;
        public bool Validate(string message, string hash) => message == hash;
    }
}
