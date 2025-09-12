using Domain.Sequrity;

namespace Api.Sequrity.Services
{
    public interface IJwtSecurityService
    {
        string CreateToken(CustomIdentityUser user); 
    }
}
