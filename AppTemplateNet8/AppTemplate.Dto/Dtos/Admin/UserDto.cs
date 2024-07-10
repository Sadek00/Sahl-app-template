using AppTemplate.Dto.Dtos.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppTemplate.Dto.Dtos.Admin
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public string SecurityStamp { get; set; }
    }
    public class UserLoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserCreateVm
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public int UserTypeId { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "'Password' and 'Confirm Password' don't match.")]
        public string ConfirmPassword { get; set; }
        public bool IsAdmin { get; set; }
        public int TeamId { get; set; }
        public int? DepartmentId { get; set; }
        public int? DesignationId { get; set; }
    }

    public class UserUpdateVm
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int? UserTypeId { get; set; }
        public int? TeamId { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? DepartmentId { get; set; }
        public int? DesignationId { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class UserListVm : CommonDto
    {
        public int? Id { get; set; }
        public string? FullName { get; set; }
        public string? Username { get; set; }
        public int? UserTypeId { get; set; }
        public string? UserTypeName { get; set; }
        public int? TeamId { get; set; }
        public string? TeamName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public int? DesignationId { get; set; }
        public string? DesignationName { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class UserCentralAuthVm
    {
        public string user_full_name { get; set; }
        public string auth_mail { get; set; }
        public string auth_phone { get; set; }
        public string service_key { get; set; }
        public string auth_username { get; set; }
        public string password { get; set; }
        public bool is_mail_confirmed { get; set; }
        public bool is_phone_confirmed { get; set; }
        public bool enable_two_factor { get; set; }
        public string user_mapping_id { get; set; }
        public bool is_admin { get; set; }
        public byte status_id { get; set; }
    }
}
