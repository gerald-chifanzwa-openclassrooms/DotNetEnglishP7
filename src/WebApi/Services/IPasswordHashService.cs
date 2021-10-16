namespace WebApi.Services
{
    public interface IPasswordHashService
    {
        string Hash(string plainText);
    }
}