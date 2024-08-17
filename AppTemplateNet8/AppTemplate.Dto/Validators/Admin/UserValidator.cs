using AppTemplate.Dto.Dtos.Admin;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppTemplate.Dto.Validators.Admin
{
    public class UserCreateValidator : AbstractValidator<UserCreateVm>
    {
        public UserCreateValidator()
        {
            //Validation for name
            RuleFor(obj => obj.FullName).NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Length(1, 100).WithMessage("{PropertyName} must be between 1 and 100 characters long.")
                .Must(value => !Regex.IsMatch(value, @"[^\w\s]")).WithMessage("{PropertyName} cannot contain special characters")
                .Must(value => !value.Contains("||")).WithMessage("{PropertyName} cannot contain the characters '||'")
                .Must(value => !value.Contains("&&")).WithMessage("{PropertyName} cannot contain the characters '&&'");

            //Validation for email 
            RuleFor(obj => obj.Email)
                .EmailAddress()
                //.WithMessage("A valid {PropertyName} is required!")
                .WithMessage("{PropertyName} is a required field!")
                //.Length(3, 70).WithMessage("{PropertyName} must be between 3 and 70 characters long.")
                .Must(value => !value.Contains("||")).WithMessage("{PropertyName} cannot contain the characters '||'")
                .Must(value => !value.Contains("&&")).WithMessage("{PropertyName} cannot contain the characters '&&'")
                //.Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]{3}$").WithMessage("Email must have a valid domain.")
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]{3}$").WithMessage("Your email address appears to be invalid. Please use the following format: mark@gmail.com.")
                .Must(value => !value.Contains(" ")).WithMessage("{PropertyName} cannot contain spaces.")
                .Must(value => !value.StartsWith(" ") && !value.EndsWith(" ")).WithMessage("{PropertyName} cannot have leading or trailing spaces.")
                .Must(value => value == value.ToLower()).WithMessage("{PropertyName} can only contain lowercase letters.");

            //Validation for username
            RuleFor(obj => obj.Username)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Length(4, 20).WithMessage("{PropertyName} must be between 4 and 20 characters long.")
                .Must(value => !Regex.IsMatch(value, @"[^\w\s]")).WithMessage("{PropertyName} cannot contain special characters")
                .Must(value => !value.Contains("||")).WithMessage("{PropertyName} cannot contain the characters '||'")
                .Must(value => !value.Contains("&&")).WithMessage("{PropertyName} cannot contain the characters '&&'");

            //Validation for password
            RuleFor(obj => obj.Password)
                .NotEmpty().WithMessage("Your {PropertyName} cannot be empty")
                .MinimumLength(8).WithMessage("{PropertyName} must have minimum 8 characters.")
                .MaximumLength(50).WithMessage("{PropertyName} must have maximum 50 characters.")
                .Matches("[A-Z]").WithMessage("{PropertyName} must contain one or more uppercase letters.")
                .Matches("[a-z]").WithMessage("{PropertyName} must contain one or more lowercase letters.")
                .Matches(@"\d").WithMessage("{PropertyName} must contain one or more digits.")
                .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("{PropertyName} must contain one or more special characters.");

            //Validation for Confirm Password
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm password is required.")
                .Equal(x => x.Password).WithMessage("Passwords do not match.");


            //Validation for Phone Number
            RuleFor(obj => obj.PhoneNumber)
                .NotEmpty().WithMessage("Your {PropertyName} cannot be empty")
                .Length(5, 15).WithMessage("{PropertyName} must be between 0 and 15 characters long.")
                //.Matches(@"^[0-9]+$").WithMessage("{PropertyName} can only contain numeric digits.")
                .Must(value => !value.Contains(" ")).WithMessage("{PropertyName} cannot contain spaces.")
                .Must(value => !value.StartsWith(" ") && !value
                .EndsWith(" ")).WithMessage("{PropertyName} cannot have leading or trailing spaces.");
            //.Must(StartsWithPlus880).WithMessage("Phone number must start with +880")
            //.Must(ContainsValidPrefix).WithMessage("contain a valid prefix (017 or 018 or 019 or 015 or 016 or 013)");

            //Validation for Id
            //RuleFor(obj => obj.TeamId).NotEmpty()
            //    .WithMessage("{PropertyName} is required")
            //    .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");

            //Validation for Id
            RuleFor(obj => obj.UserTypeId).NotEmpty()
                .WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");
        }


    }

    public class UserUpdateValidator : AbstractValidator<UserUpdateVm>
    {
        public UserUpdateValidator()
        {
            

            //Validation for email 
            RuleFor(obj => obj.Email)
                .EmailAddress()
                //.WithMessage("A valid {PropertyName} is required!")
                .WithMessage("{PropertyName} is a required field!")
                //.Length(3, 70).WithMessage("{PropertyName} must be between 3 and 70 characters long.")
                .Must(value => !value.Contains("||")).WithMessage("{PropertyName} cannot contain the characters '||'")
                .Must(value => !value.Contains("&&")).WithMessage("{PropertyName} cannot contain the characters '&&'")
                //.Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]{3}$").WithMessage("Email must have a valid domain.")
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]{3}$").WithMessage("Your email address appears to be invalid. Please use the following format: mark@gmail.com.")
                .Must(value => !value.Contains(" ")).WithMessage("{PropertyName} cannot contain spaces.")
                .Must(value => !value.StartsWith(" ") && !value.EndsWith(" ")).WithMessage("{PropertyName} cannot have leading or trailing spaces.")
                .Must(value => value == value.ToLower()).WithMessage("{PropertyName} can only contain lowercase letters.");

            //Validation for username
            RuleFor(obj => obj.Username)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Length(4, 20).WithMessage("{PropertyName} must be between 4 and 20 characters long.")
                .Must(value => !Regex.IsMatch(value, @"[^\w\s]")).WithMessage("{PropertyName} cannot contain special characters")
                .Must(value => !value.Contains("||")).WithMessage("{PropertyName} cannot contain the characters '||'")
                .Must(value => !value.Contains("&&")).WithMessage("{PropertyName} cannot contain the characters '&&'");

            //Validation for Phone Number
            RuleFor(obj => obj.PhoneNumber)
                .NotEmpty().WithMessage("Your {PropertyName} cannot be empty")
                .Length(5, 15).WithMessage("{PropertyName} must be between 0 and 15 characters long.")
                //.Matches(@"^[0-9]+$").WithMessage("{PropertyName} can only contain numeric digits.")
                .Must(value => !value.Contains(" ")).WithMessage("{PropertyName} cannot contain spaces.")
                .Must(value => !value.StartsWith(" ") && !value
                .EndsWith(" ")).WithMessage("{PropertyName} cannot have leading or trailing spaces.");
            //.Must(StartsWithPlus880).WithMessage("Phone number must start with +880")
            //.Must(ContainsValidPrefix).WithMessage("contain a valid prefix (017 or 018 or 019 or 015 or 016 or 013)");

            //Validation for Id
            RuleFor(obj => obj.TeamId).NotEmpty()
                .WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");

            //Validation for Id
            RuleFor(obj => obj.UserTypeId).NotEmpty()
                .WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");
        }


    }
}
