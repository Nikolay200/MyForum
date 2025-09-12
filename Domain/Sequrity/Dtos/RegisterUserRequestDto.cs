
using System.ComponentModel.DataAnnotations;

namespace Domain.Sequrity.Dtos
{
    public record RegisterUserRequestDto(
        [MinLength(length:2, ErrorMessage = "Пароль должен быть больше 2-х символов")]
        string FullName,
        string UserName,
        string Email,
        string Password
        );
}
