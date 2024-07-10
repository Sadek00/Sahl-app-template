using AppTemplate.Dto.Dtos.Settings;
using FluentValidation;

namespace AppTemplate.Dto.Validators.Settings
{
    public class DesignationCreateValidator : AbstractValidator<DesignationCreateVm>
    {
        public DesignationCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MinimumLength(3)
                .MaximumLength(255)
                .WithMessage("{PropertyName} must be between 3 to 255 characters long.")
                .Must(value => !value.StartsWith(" ") && !value.EndsWith(" "))
                .WithMessage("{PropertyName} cannot have leading or trailing spaces.");
        }
    }

    public class DesignationUpdateValidator : AbstractValidator<DesignationUpdateVm>
    {
        public DesignationUpdateValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MinimumLength(3)
                .MaximumLength(255)
                .WithMessage("{PropertyName} must be between 3 to 255 characters long.")
                .Must(value => !value.StartsWith(" ") && !value.EndsWith(" "))
                .WithMessage("{PropertyName} cannot have leading or trailing spaces.");
        }
    }
}
