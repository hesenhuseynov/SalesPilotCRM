namespace SalesPilotCRM.Application.Interfaces.Auth
{
    public interface IPasswordHasher
    {

        (byte[] PasswordHash, byte[] PasswordSalt) HashPassword(string password);
        bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt);

    }
}
