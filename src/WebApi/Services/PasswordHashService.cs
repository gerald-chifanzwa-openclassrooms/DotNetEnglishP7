namespace WebApi.Services
{
    public class PasswordHashService : IPasswordHashService
    {
        public string Hash(string plainText)
        {
            return plainText;
        }
    }
}
