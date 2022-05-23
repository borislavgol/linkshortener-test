using LinkShortener.Dtos;
using LinkShortener.Services.Abstractions;
using MediatR;

namespace LinkShortener.Mediatr.Handlers
{
    internal class SignupUserHandler : IRequestHandler<SignupUserRequestDto, SignupUserResponseDto>
    {
        private readonly IAuthorizationService _authorizationService;
        public SignupUserHandler(
            IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public async Task<SignupUserResponseDto> Handle(SignupUserRequestDto request, CancellationToken cancellationToken)
        {
            var user = await _authorizationService.CreateUserAsync(request.Login, request.Password);

            return new SignupUserResponseDto
            {
                IsSuccess = user is not null,
                UserId = user?.Id ?? -1
            };
        }
    }
}
