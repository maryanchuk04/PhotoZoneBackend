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
    private readonly IMailService _mailService;
    private readonly IAuthServices _authServices;

    public UserController(IUserServices userServices,
        IMapper mapper,
        ISubscribesService subscribesService,
        ISubscribtionService subscribtionService,
        ISecurityContext securityContext,
        IMailService mailService)
    {
        _subscribesService = subscribesService;
        _subscribtionService = subscribtionService;
        _userServices = userServices;
        _mapper = mapper;
        _securityContext = securityContext;
        _mailService = mailService;
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    public IActionResult Registration(RegisterViewModel registerViewModel)
    {
        try
        {
            var token = _userServices.Registration(registerViewModel.Email, registerViewModel.Password,
                registerViewModel.UserName);

            _mailService.sendMailRegistration(registerViewModel.Email,registerViewModel.UserName);

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

    [HttpGet("[action]")]
    [AllowAnonymous]
    public IActionResult GetAllUsers()
    {
        return Ok(_userServices.GetAllUsers());
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    public IActionResult SearchUsers(SearchViewModel searchViewModel)
    {
        return Ok(_userServices.SearchUsers(searchViewModel.searchText));
    }

    [HttpPost("[action]")]
    public IActionResult SaveUserInfo(UserGeneralInfoViewModel userGeneralInfoViewModel)
    {
        var userDto = new UserDto()
        {
            FullName = userGeneralInfoViewModel.FullName,
            Birthday = userGeneralInfoViewModel.Birthday,
            Gender = userGeneralInfoViewModel.Gender,
            Hobby = userGeneralInfoViewModel.Hobby,
            UserName = userGeneralInfoViewModel.UserName,
            Phone = userGeneralInfoViewModel.Phone,
        };
        return Ok(_userServices.SaveUserInfo(userDto));
    }

    [HttpPost("[action]")]
    public IActionResult ChangeAvatar(AvatarViewModel avatarViewModel)
    {
        return Ok(_userServices.ChangeAvatar(avatarViewModel.Avatar));
    }

    [HttpPost("[action]")]
    public IActionResult SaveUserSocials(UserSocialsViewModel socialsViewModel)
    {
        var userDto = new UserDto()
        {
           TikTokLink = socialsViewModel.TikTokLink,
           FacebookLink = socialsViewModel.FacebookLink,
           GitHubLink = socialsViewModel.GitHubLink,
           InstLink = socialsViewModel.InstLink
        };
        return Ok(_userServices.SaveUserSocials(userDto));
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    public IActionResult SendFeedback(FeedbackViewModel feedbackViewModel)
    {
        try
        {
            _mailService.sendFeedback(feedbackViewModel.Email, feedbackViewModel.UserName, feedbackViewModel.Text);
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
    [AllowAnonymous]
    public IActionResult GoogleLogin(GoogleLoginViewModel googleLoginViewModel)
    {
        try
        {
            return Ok(_userServices.GoogleLogin(googleLoginViewModel.Email, googleLoginViewModel.Avatar, googleLoginViewModel.UserName));
        }
        catch (PhotoZoneException e)
        {
            return BadRequest(new
            {
                error = e.Message
            });
        }
    }
}