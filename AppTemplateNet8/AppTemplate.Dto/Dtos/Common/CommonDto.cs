using AppTemplate.Dto.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Dto.Dtos.Common
{
    public class CommonDto
    {
        public DateTime? CreatedAt { get; set; }        
        public int? CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public string? UpdatedByName { get; set; }
        public byte? StatusId { get; set; }
        public string? Status
        {
            get
            {
                if (_status != null)
                    return _status;
                else if (StatusId == 1)
                    return AppConstants.StatusId.Active.ToString();
                else if (StatusId == 0)
                    return AppConstants.StatusId.InActive.ToString();
                else if (StatusId == 2)
                    return AppConstants.StatusId.Delete.ToString();
                else
                    return null;
            }
            set { _status = value; }
        }
        private string? _status;
    }

    public class CentralAuthResponseVm
    {
        public string Code { get; set; }
        public string? Message { get; set; }
        public string Lang { get; set; }
        public string? Detail { get; set; }
    }
}
