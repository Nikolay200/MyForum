
namespace Domain.Sequrity.Dtos
{
    public record IdentityUserResponseDto(
        string UserName,
        string Email,
        string JwtToken
        );
}
