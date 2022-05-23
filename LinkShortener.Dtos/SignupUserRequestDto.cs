using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LinkShortener.Dtos
{
    public class SignupUserRequestDto : IRequest<SignupUserResponseDto>
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
