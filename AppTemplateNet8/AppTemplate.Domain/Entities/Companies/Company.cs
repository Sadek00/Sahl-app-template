using AppTemplate.Domain.Entities.Common;
using AppTemplate.Domain.Entities.Countries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Domain.Entities.Companies
{
    public class Company : BaseEntity
    {
        [MaxLength(100)]
        public required string Name { get; set; }  // The name of the company

        [MaxLength(200)]
        public string? LegalName { get; set; }  // The legal name of the company

        [MaxLength(100)]
        public string? RegistrationNumber { get; set; }  // Registration number with government

        [MaxLength(200)]
        public string? Website { get; set; }  // Company's official website

        [MaxLength(200)]
        public string? Industry { get; set; }  // Industry type (e.g., IT, Healthcare)
 
        public string? CompanyCode { get; set; } = string.Empty;

        public string? CompanyNameBangla { get; set; }

        public string? CompanyShortName { get; set; }

        public string? PrimaryAddress { get; set; } = string.Empty;

        public string? CompanyAddressBangla { get; set; }

        public string? SecoundaryAddress { get; set; }

        [MaxLength(100)]
        public string? PhoneNumber { get; set; }  // Company contact phone number

        [MaxLength(100)]
        public string? Email { get; set; }  // Company contact email

        [MaxLength(500)]
        public string? Address { get; set; }  // Headquarters address

        public int? CountryId { get; set; }  // Foreign key for the associated country

        [ForeignKey("CountryId")]
        public virtual Country? Country { get; set; }  // Navigation property for the country

        [MaxLength(100)]
        public string? City { get; set; }  // City where the company is located

        [MaxLength(50)]
        public string? PostalCode { get; set; }  // Postal code for the company's location

        public DateTime? EstablishedDate { get; set; }  // Date the company was established

        [MaxLength(100)]
        public string? CEO { get; set; }  // Name of the CEO

        [MaxLength(200)]
        public string? LogoUrl { get; set; }  // URL for the company's logo

        public int? TaxId { get; set; }  // Foreign key for the associated tax

        [ForeignKey("TaxId")]
        public virtual Tax? Tax { get; set; }  // Navigation property for the tax
    }
}
