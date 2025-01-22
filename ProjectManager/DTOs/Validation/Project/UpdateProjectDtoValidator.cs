using FluentValidation;

namespace ProjectManager.DTOs.Validation
{
    public class UpdateProjectDtoValidator : AbstractValidator<UpdateProjectDto>
    {
        public UpdateProjectDtoValidator()
        {
            RuleFor(p => p.Id)
                .NotNull().WithMessage("Id must be defined.")
                .NotEmpty().WithMessage("Id can not be defined.");

            RuleFor(p => p.Name)
                .NotNull().WithMessage("Name must be defined.")
                .NotEmpty().WithMessage("Name can not be empty.")
                .MinimumLength(3).WithMessage("Name has to have at least 3 signs.")
                .MaximumLength(30).WithMessage("Name can't be longer than 30 signs.")
                .Matches(@"^[a-zA-Z0-9\s]+$").WithMessage("Name can't have special characters.");

            RuleFor(p => p.Description)
                .MaximumLength(500).WithMessage("Description can't be longer than 500 signs.");
        }
    }
}