using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PhotoZone.Core.DTOs;
using PhotoZone.Core.Exceptions;
using PhotoZone.Core.IServices;
using PhotoZone.EF;
using PhotoZone.Entities;
using PhotoZone.Enums;
using PhotoZone.IServices;

namespace PhotoZone.Services;

public class UserService : BaseService<User>, IUserServices
{
    private readonly IAuthServices _authServices;
    private readonly ISecurityContext _securityContext;
    private readonly ISubscribesService _subscribesService;
    private readonly ISubscribtionService _subscribtionService;


    public UserService(AppDbContext context,
        IMapper mapper,
        IAuthServices authServices,
        ISecurityContext securityContext,
        ISubscribesService subscribesService,
        ISubscribtionService subscribtionService
        ) : base(context, mapper)
    {
        _securityContext = securityContext;
        _authServices = authServices;
        _subscribesService = subscribesService;
        _subscribtionService = subscribtionService;
    }


    public string Registration(string email, string password, string userName)
    {
        User isExistingUser = Context.Users.Where(x => x.Email == email).FirstOrDefault();

        if (isExistingUser != null)
        {
            throw new PhotoZoneException("User is already exist");
        }

        User newUser = new User()
        {
            Id = Guid.NewGuid(),
            Email = email,
            Password = BCrypt.Net.BCrypt.HashPassword(password),
            UserName = userName
        };

        var user = Insert(newUser);
        Context.SaveChanges();

        var token = _authServices.GenerateJwt(Mapper.Map<User, UserDto>(user));

        return token;
    }

    public string Authenticate(string email, string password)
    {
        return (_authServices.Authentificate(email, password));
    }

    public async Task ChangeAvatar(Guid userid, string avatar)
    {

    }

    public async Task GetProfileById(Guid userid)
    {
        throw new NotImplementedException();
    }

    public UserDto GetCurrentUserInfo()
    {
        var currentUserId = _securityContext.GetCurrentUserId();
        Console.WriteLine(currentUserId);
        var user = Context.Users
            .Include(x => x.Subscribes)
            .Include(x=> x.Subscrioptions)
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == currentUserId);

        return Mapper.Map<User, UserDto>(user);
    }

    public IEnumerable<UserDto> GetUsersByIds(IEnumerable<Guid> ids)
    {
        throw new NotImplementedException();
    }

    public async Task EditUserName(string username)
    {
        var user = CurrentUser();
        user.UserName = username;

        Context.Update(user);

        await Context.SaveChangesAsync();
    }

    public async Task<string> ChangeAvatar(string Avatar)
    {
        var user = CurrentUser();

        user.Avatar = Avatar;

        Context.Update(user);

        await Context.SaveChangesAsync();

        return user.Avatar;
    }

    public string RefreshToken()
    {
        var user = CurrentUser();
        return _authServices.GenerateJwt(Mapper.Map<User, UserDto>(user));
    }

    public UserDto GetUserInfoById(Guid id)
    {
        var user = Context.Users
            .Include(x => x.Subscribes)
            .Include(x=>x.Subscrioptions)
            .FirstOrDefault(x => x.Id == id);

        return Mapper.Map<User, UserDto>(user);
    }

    private User CurrentUser()
    {
        var userId = _securityContext.GetCurrentUserId();

        var user = Context.Users
            .Include(x=>x.Subscribes)
            .Include(x => x.Subscrioptions)
            .FirstOrDefault(x => x.Id==userId);

        return user;
    }


    public async Task AddNewSubscribe(Guid subscriberId)
    {
        var currentUser = CurrentUser();

        var isExist = Context.Subscribtions.FirstOrDefault(x => x.SubscribtionId == subscriberId);
        if (isExist != null)
            throw new PhotoZoneException("You already subscribes for  this user");


        await _subscribtionService.AddNewSubscribtion(currentUser.Id,subscriberId);
        await _subscribesService.AddSubscriberForUser(currentUser.Id, subscriberId);

        //add this when i create user
        currentUser.SubscribtionsId = currentUser.Id;

        var subscriber = Context.Users.First(x => x.Id == subscriberId);

        subscriber.SubscribesId = subscriberId;

        Update(currentUser);
        Update(subscriber);

        await Context.SaveChangesAsync();
    }

    public List<UserDto> GetUsersByIds(List<Guid> Ids)
    {
        if (Ids == null)
        {
            throw new PhotoZoneException("Something went wrong");
        }
        var users = Context.Users.Where(user => Ids.Contains(user.Id));

        return Mapper.Map<List<UserDto>>(users.ToList());
    }

    public List<UserDto> GetAllUsers()
    {
        return Mapper.Map<List<User>, List<UserDto>>(Context.Users.ToList());
    }

    public List<UserDto> SearchUsers(string searchText)
    {
        var allUsers = Context.Users.ToList();

        var res = allUsers.FindAll(x => x.UserName.Contains(searchText) || x.FullName.Contains(searchText));

        return Mapper.Map<List<User>, List<UserDto>>(res);
    }

    public UserDto SaveUserInfo(UserDto userDto)
    {
        var user = Context.Users.FirstOrDefault(x => x.Id == _securityContext.GetCurrentUserId());

        user.Birthday = userDto.Birthday == null ? user.Birthday : userDto.Birthday;
        user.Gender = userDto.Gender == null ? user.Gender : userDto.Gender;
        user.FullName = userDto.FullName == null ? user.FullName : userDto.FullName;
        user.UserName = userDto.UserName == null ? user.UserName : userDto.UserName;
        user.Phone = userDto.Phone == null ? user.Phone : userDto.Phone;
        user.Hobby = userDto.Hobby == null ? user.Hobby : userDto.Phone;


        Update(user);

        Context.SaveChanges();
        return Mapper.Map<User, UserDto>(user);
    }

}
