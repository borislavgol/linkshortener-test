using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LinkShortener.Dtos
{
    public class SigninUserRequestDto : IRequest<SigninUserResponseDto>
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
