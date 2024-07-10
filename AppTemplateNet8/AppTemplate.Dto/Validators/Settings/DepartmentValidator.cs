using AppTemplate.Dto.Dtos.Settings;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Dto.Validators.Settings
{
    public class DepartmentCreateValidator : AbstractValidator<DepartmentCreateVm>
    {
        public DepartmentCreateValidator()
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

    public class DepartmentUpdateValidator : AbstractValidator<DepartmentUpdateVm>
    {
        public DepartmentUpdateValidator()
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
