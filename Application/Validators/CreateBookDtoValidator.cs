using BookStoreApi.Application.DTOs;
using FluentValidation;

namespace BookStoreApi.Application.Validators
{
    public class CreateBookDtoValidator : AbstractValidator<CreatedBookDto>
    {
        public CreateBookDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100);

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Author is required")
                .MaximumLength(100);

            RuleFor(x => x.Year)
                .GreaterThan(0).WithMessage("Year must be greater than 0")
                .LessThanOrEqualTo(DateTime.Now.Year)
                .WithMessage("Year cannot be in the future"); ;
        }
    }
}
