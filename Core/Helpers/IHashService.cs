namespace Core.Helpers
{
    public interface IHashService
    {
        string GenerateSHA256String(string inputString);

        string GenerateSHA512String(string inputString);

        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
    }
}
