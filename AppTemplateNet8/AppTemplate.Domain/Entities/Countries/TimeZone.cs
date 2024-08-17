using AppTemplate.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Domain.Entities.Countries
{
    public class TimeZone : BaseEntity
    {
        public required string TimeZoneId { get; set; }  // Time zone ID (e.g., "Pacific Standard Time")
        public string? DisplayName { get; set; }  // Optional: user-friendly name
        public TimeSpan? UtcOffset { get; set; }  // Time offset from UTC (e.g., +6 hours)
    }
}
