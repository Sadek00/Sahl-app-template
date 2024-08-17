using AppTemplate.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Domain.Entities.Countries
{
    public class Country : BaseEntity
    {
        [MaxLength(100)]
        public required string Name { get; set; }  // The name of the country

        [MaxLength(3)]
        public string ISOCode { get; set; } = string.Empty;  // ISO 3166-1 alpha-3 code (e.g., "USA", "GBR") 

        [MaxLength(5)]
        public string PhoneCode { get; set; } = string.Empty; // International dialing code (e.g., "+1", "+44")
        public int TimeZoneId { get; set; }  // Foreign Key
        [ForeignKey(nameof(TimeZoneId))]
        public TimeZone? TimeZone { get; set; }  // Navigation property to the TimeZone model

        public DateTime GetLocalTime()
        {
            if (TimeZone != null)
            {
                TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZone.TimeZoneId);
                return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
            }
            else
            {
                throw new InvalidOperationException("Time zone is not set.");
            }
        }
    }
}
