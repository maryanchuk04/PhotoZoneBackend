using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoZone.Core.DTOs;
using PhotoZone.Core.Exceptions;
using PhotoZone.Core.IServices;
using PhotoZone.Entities;
using PhotoZone.IServices;
using PhotoZone.PhotoZoneExtention;
using PhotoZone.Services;
using PhotoZone.ViewModels;

namespace PhotoZone.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserServices _userServices;
    private readonly ISubscribesService _subscribesService;
    private readonly ISubscribtionService _subscribtionService;
    private readonly ISecurityContext _securityContext;
    public UserController(IUserServices userServices,
        IMapper mapper,
        ISubscribesService subscribesService,
        ISubscribtionService subscribtionService,
        ISecurityContext securityContext)
    {
        _subscribesService = subscribesService;
        _subscribtionService = subscribtionService;
        _userServices = userServices;
        _mapper = mapper;
        _securityContext = securityContext;
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    public IActionResult Registration(RegisterViewModel registerViewModel)
    {
        try
        {
            var token = _userServices.Registration(registerViewModel.Email, registerViewModel.Password,
                registerViewModel.UserName);

            return Ok(new
            {
                Token = token
            });
        }
        catch (PhotoZoneException e )
        {
            return BadRequest(new
            {
                error = e.Message
            });
        }

    }

    [AllowAnonymous]
    [HttpPost("[action]")]
    public IActionResult Authentificate([FromBody]AuthViewModel authViewModel)
    {
        try
        {
            var token = _userServices.Authenticate(authViewModel.Email, authViewModel.Password);
            HttpContext.SetTokenCookie(token);
            return Ok(new
            {
                Token = token
            });
        }
        catch (PhotoZoneException e )
        {
            return BadRequest(new
            {
                error = e.Message
            });

        }

    }

    [HttpGet("[action]")]
    public IActionResult GetCurrentUserInfo()
    {
        var userdto = _userServices.GetCurrentUserInfo();

        return Ok(_mapper.Map<UserDto, UserViewModel>(userdto));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> EditAvatar(AvatarViewModel avatarViewModel)
    {
        await _userServices.EditAvatar(avatarViewModel.Avatar);
        return Ok();
    }


    [HttpPost("[action]")]
    public async Task<IActionResult> AddNewSubscribe(SubscribeViewModel subscribeViewModel)
    {

        try
        {
            await _userServices.AddNewSubscribe(subscribeViewModel.SubscribeId);
            return Ok();
        }
        catch (PhotoZoneException e)
        {
            return BadRequest(new
            {
                error = e.Message
            });
        }
    }

    [HttpPost("[action]")]
    public IActionResult GetUsersInfoByIds(UsersIdsViewModel usersViewModel)
    {
        try
        {
            return Ok(_userServices.GetUsersByIds(usersViewModel.Ids));
        }
        catch (PhotoZoneException e)
        {
            return BadRequest(new
            {
                error = e.Message
            });
        }
    }

    [HttpGet("[action]")]
    public IActionResult GetUserSubscriptionsInfo()
    {
        try
        {
            var userId = _securityContext.GetCurrentUserId();
            var users = _userServices.GetUsersByIds(_subscribtionService.GetAllSubscribtion(userId));
            return Ok(_mapper.Map<List<UserDto>,List<UserViewModel>>(users));
        }
        catch (PhotoZoneException e)
        {
            return BadRequest(new
            {
                error = e.Message
            });
        }
    }

    [HttpGet("[action]")]
    public IActionResult GetUserSubscribersInfo()
    {
        try
        {
            var userId = _securityContext.GetCurrentUserId();
            var res = _subscribesService.GetAlSubscribers(userId);
            if (res == null)
            {
                List<Guid> arr = new List<Guid>();
                return Ok(arr);
            }
            var users = _userServices.GetUsersByIds(res);
            return Ok(_mapper.Map<List<UserDto>, List<UserViewModel>>(users));
        }
        catch (PhotoZoneException e)
        {
            return BadRequest(new
            {
                error = e.Message
            });
        }
    }

    [HttpGet("[action]/{id}")]
    [AllowAnonymous]
    public IActionResult GetUserInfoById(Guid id)
    {
        var res = _userServices.GetUserInfoById(id);

        return Ok(_mapper.Map<UserDto, UserViewModel>(res));
    }


}