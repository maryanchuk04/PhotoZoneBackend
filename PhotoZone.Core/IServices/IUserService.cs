using Microsoft.AspNetCore.Http;
using PhotoZone.Core.DTOs;
using PhotoZone.Entities;


namespace PhotoZone.IServices;

public interface IUserServices
{
    List<UserDto> GetAllUsers();

    string Registration(string email, string password, string userName);

    string Authenticate(string email, string password);

    Task ChangeAvatar(Guid userid, string avatar);

    Task GetProfileById(Guid userid);

    UserDto GetCurrentUserInfo();

    IEnumerable<UserDto> GetUsersByIds(IEnumerable<Guid> ids);

    Task EditUserName(string username);

    Task EditAvatar(string Avatar);

    UserDto GetUserInfoById(Guid id);

    string RefreshToken();

    Task AddNewSubscribe(Guid subscriberId);

    List<UserDto> GetUsersByIds(List<Guid> Ids);
}