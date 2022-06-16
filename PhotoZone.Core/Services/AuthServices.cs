using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PhotoZone.Core.DTOs;
using PhotoZone.Core.Exceptions;
using PhotoZone.Core.IServices;
using PhotoZone.EF;
using PhotoZone.Entities;

namespace PhotoZone.Services;

public class AuthServices : BaseService<User>, IAuthServices
{
    private readonly IConfiguration _config;
    private readonly IMailService _mailService;

    public AuthServices(AppDbContext dbContext, IMapper mapper,IConfiguration config, IMailService mailService) : base(dbContext, mapper)
    {
        _config = config;
        _mailService = mailService;
    }

    public UserDto Auth(string email, string password)
    {
        var currentUser = Context.Users.FirstOrDefault(o => o.Email == email);

        if (currentUser != null)
        {
            if (BCrypt.Net.BCrypt.Verify(password, currentUser.Password))
                return Mapper.Map<User, UserDto>(currentUser);
            else throw new PhotoZoneException("Incorrect Login or Password");
        }

        return null;
    }

    public string GenerateJwt(UserDto user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, $"{user.Id}")
        };

        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(30000),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string Authentificate(string email, string password)
    {
        UserDto user = Auth(email, password);
        if (user != null)
        {
            var token = GenerateJwt(user);
            return token;
        }

        throw new PhotoZoneException("User Not found");
    }

    public string GoogleLogin(string email, string avatar, string userName)
    {
       var user = Context.Users.Where(x => x.Email == email).FirstOrDefault();

       if (user == null)
       {
           var newUser = new User()
           {
               UserName = userName,
               Email = email,
               FullName = userName,
               Id = Guid.NewGuid(),
               Avatar = avatar
           };
           _mailService.sendMailRegistration(email,userName);
           Insert(newUser);
           Context.SaveChanges();

           return GenerateJwt(Mapper.Map<User, UserDto>(newUser));

       }

       if (user.Password != null)
       {
           throw new PhotoZoneException("User is alredy exist");
       }
       else
       {
           return GenerateJwt(Mapper.Map<User, UserDto>(user));
       }


    }
}