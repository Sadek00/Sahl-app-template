using AppTemplate.Dto.Dtos.Admin;
using FluentValidation;

namespace AppTemplate.Dto.Validators.Admin
{
    public class UserTypeCreateValidator : AbstractValidator<UserTypeCreateVm>
    {
        public UserTypeCreateValidator()
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

    public class UserTypeUpdateValidator : AbstractValidator<UserTypeUpdateVm>
    {
        public UserTypeUpdateValidator()
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