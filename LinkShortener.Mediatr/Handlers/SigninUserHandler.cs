using LinkShortener.Dtos;
using LinkShortener.Services.Abstractions;
using MediatR;

namespace LinkShortener.Mediatr.Handlers
{
    internal class SigninUserHandler : IRequestHandler<SigninUserRequestDto, SigninUserResponseDto>
    {
        private readonly IAuthorizationService _authorizationService;
        public SigninUserHandler(
            IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public async Task<SigninUserResponseDto> Handle(SigninUserRequestDto request, CancellationToken cancellationToken)
        {
            var user = await _authorizationService.AuthorizeUserAsync(request.Login, request.Password);

            return new SigninUserResponseDto
            {
                IsSuccess = user is not null,
                UserId = user?.Id ?? -1
            };
        }
    }
}
