using Api.Sequrity.Services;
using Domain.Sequrity;
using Domain.Sequrity.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(UserManager<CustomIdentityUser> userManager, IJwtSecurityService securityService) : ControllerBase
    {

        [HttpPost("login")]
        public async Task<IResult> Login(LoginRequest dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user is null) 
            {
            return Results.Unauthorized();
            }
            var result = await userManager.CheckPasswordAsync(user, dto.Password);

            if (result)
            {
                var response = new IdentityUserResponseDto(user.UserName, user.Email, securityService.CreateToken(user));
                return Results.Ok(new {result = response});
            }
            return Results.Unauthorized();
        }
        [HttpPost("register")]
        public async Task<IResult> Register(RegisterUserRequestDto userDto)
        {
            if(await userManager.Users.AnyAsync(x=>x.UserName == userDto.UserName))
            {
                return Results.BadRequest("UserName занят");
            }
            if (await userManager.Users.AnyAsync(x => x.Email == userDto.Email))
            {
                return Results.BadRequest("Email занят");
            }

            var user = new CustomIdentityUser
            {
                UserName = userDto.UserName,
                FullName = userDto.FullName,
                Email = userDto.Email,
                About = String.Empty,
            };

            var result = await userManager.CreateAsync(user, userDto.Password!);

            if (result.Succeeded) 
            {
                var response = new IdentityUserResponseDto(user.UserName!, user.Email!, securityService.CreateToken(user));
                return Results.Ok(new { result = response });
            }
            return Results.BadRequest(result.Errors);
        }
    }
}
