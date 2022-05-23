using FluentValidation;
using LinkShortener.Dtos;

namespace LinkShortener.Mediatr.Validators
{
    public class LinkShortenRequestDtoValidator : AbstractValidator<LinkShortenRequestDto>
    {
        public LinkShortenRequestDtoValidator()
        {
            RuleFor(x => x.Link)
                .Must(url =>
                    Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                        && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps));
        }
    }
}
