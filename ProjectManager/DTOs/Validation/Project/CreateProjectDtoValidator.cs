using FluentValidation;

namespace ProjectManager.DTOs.Validation
{
    public class CreateProjectDtoValidator : AbstractValidator<CreateProjectDto>
    {
        public CreateProjectDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotNull().WithMessage("Name must be defined.")
                .NotEmpty().WithMessage("Name must not be empty.")
                .MinimumLength(3).WithMessage("Name has to have at least 3 signs.")
                .MaximumLength(50).WithMessage("Name can't be longer than 50 signs.")
                .Matches(@"^[a-zA-Z0-9\s]+$").WithMessage("Name can't have special characters.");

            RuleFor(p => p.Description)
                .MaximumLength(500).WithMessage("Description can't be longer than 500 signs.");
        }
    }
}