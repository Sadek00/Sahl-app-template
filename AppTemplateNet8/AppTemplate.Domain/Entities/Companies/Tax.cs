using AppTemplate.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Domain.Entities.Companies
{
    public class Tax : BaseEntity
    {
        [MaxLength(200)]
        public required string TaxId { get; set; }  // Tax Identification Number (TIN)

        [MaxLength(200)]
        public string TaxName { get; set; } = string.Empty;  // Name or description of the tax

        public decimal? Rate { get; set; }  // Tax rate (e.g., 0.15 for 15%)
    }
}
