using Api.Sequrity.Services;
using Domain.Sequrity;
using Domain.Sequrity.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

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
    }
}
