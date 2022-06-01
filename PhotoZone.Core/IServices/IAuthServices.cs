using PhotoZone.Core.DTOs;

namespace PhotoZone.Core.IServices;

public interface IAuthServices
{
    UserDto Auth(string email, string password);

    string GenerateJwt(UserDto user);

    string Authentificate(string email, string password);

}