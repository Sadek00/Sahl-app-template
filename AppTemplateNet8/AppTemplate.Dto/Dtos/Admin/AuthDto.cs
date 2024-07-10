using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Dto.Dtos.Admin
{
    public class AuthDto
    {

    }
    public class RefreshTokenDto
    {
        public string ExpiredToken { get; set; }
        public string RefreshToken { get; set; }
    }
    public class ForgetPasswordDto
    {
        public string Email { get; set; }
    }
    public class ChangePasswordDto
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        [Compare(nameof(NewPassword), ErrorMessage = "'Password' and 'Confirm Password' don't match.")]
        public string ConfirmPassword { get; set; }
    }
    public class UpdatePasswordDto
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        [Compare(nameof(NewPassword), ErrorMessage = "'Password' and 'Confirm Password' don't match.")]
        public string ConfirmPassword { get; set; }
    }
}
