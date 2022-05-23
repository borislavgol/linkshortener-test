using FluentValidation;
using LinkShortener.Dtos;

namespace LinkShortener.Mediatr.Validators
{
    public class GetShortedLinksRequestDtoValidator : AbstractValidator<GetShortedLinksRequestDto>
    {
        public GetShortedLinksRequestDtoValidator()
        {
            RuleFor(x => x.Limit)
                .GreaterThan(0)
                .LessThan(50);

            RuleFor(x => x.Offset)
                .GreaterThanOrEqualTo(0);
        }
    }
}
