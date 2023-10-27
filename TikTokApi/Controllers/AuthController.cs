using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using TikTokApi.Dtos;
using TikTokApi.Interfaces;
using TikTokApi.Models;

namespace TikTokApi.Controllers; 

[Route("api/auth")]
[ApiController]
public class AuthController: Controller {
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    
    public AuthController(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    [HttpPost("register")]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    public IActionResult Register(UserRegisterRequestDto request)
    {
        
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new User()
        {
            Name = request.Name,
            Username = request.Username,
            PasswordHash = passwordHash,
        };
        
        var newUser = _userRepository.SaveUser(user);

        var response = new UserDto()
        {
            Id = newUser.Id,
            Username = newUser.Username
        };

       

        return Ok(response);
    }
    
    
    [HttpPost("login")]
    [ProducesResponseType(200, Type = typeof(AuthResponseDto))]
    public IActionResult Login(LoginRequestDto request)
    {

        var userExists = _userRepository.UserExists(request.Username);
        
        if (!userExists)
        {
            return NotFound();
        }

        var user = _userRepository.GetUserByUsername(request.Username);

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return BadRequest("Wrong username/password.");
        }

        var token = CreateJwt(user);

        var mappedUser = new UserDto()
        {
            Id = user.Id,
            Username = user.Username
        };

        var response = new AuthResponseDto()
        {
            User = mappedUser,
            Token = token,
        };

        return Ok(response);
    }

    private string CreateJwt(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("userId", user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration["JwtSettings:Token"]!
            ));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(15), signingCredentials:cred, issuer: _configuration["JwtSettings:Issuer"]!, audience:_configuration["JwtSettings:Audience"]!);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        
        return jwt;
    }
    
    

}