using Microsoft.AspNetCore.Http;
using PhotoZone.Core.DTOs;
using PhotoZone.Entities;


namespace PhotoZone.IServices;

public interface IUserServices
{
    List<UserDto> GetAllUsers();

    string Registration(string email, string password, string userName);

    string Authenticate(string email, string password);

    UserDto GetCurrentUserInfo();

    IEnumerable<UserDto> GetUsersByIds(IEnumerable<Guid> ids);

    Task EditUserName(string username);

    string ChangeAvatar(string Avatar);

    UserDto GetUserInfoById(Guid id);

    string RefreshToken();

    Task AddNewSubscribe(Guid subscriberId);

    List<UserDto> GetUsersByIds(List<Guid> Ids);

    List<UserDto> SearchUsers(string searchText);

    UserDto SaveUserInfo(UserDto userDto);

    UserDto SaveUserSocials(UserDto userDto);



}