using FluentValidation;
using ProjectManager.DTOs.ProjectTaskDTO;

namespace ProjectManager.DTOs.Validation.ProjectTask
{
    public class UpdateProjectTaskDtoValidator : AbstractValidator<UpdateProjectTaskDto>
    {
        public UpdateProjectTaskDtoValidator()
        {
            RuleFor(p => p.Title)
                .NotNull().WithMessage("Title must not be null")
                .NotEmpty().WithMessage("Title must not be empty.")
                .MaximumLength(100).WithMessage("Title can't be longer than 100 signs.");

            RuleFor(p => p.IsCompleted)
                .NotNull().WithMessage("IsCompleted is required.");
        }
    }
}
