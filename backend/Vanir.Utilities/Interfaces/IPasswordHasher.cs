namespace Vanir.Utilities.Interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(byte[] salt, string password);
    }
}