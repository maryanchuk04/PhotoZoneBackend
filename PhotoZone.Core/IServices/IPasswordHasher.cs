namespace PhotoZone.Core.IServices;

public interface IPasswordHasher
{
    string GenerateSalt();

    string GenerateHash(string password, string salt);
}