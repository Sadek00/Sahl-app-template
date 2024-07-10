using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Dto.Constants
{
    public class AppConstants
    {
        public enum StatusId
        {
            InActive = 0,
            Active = 1,
            Delete = 2
        }
        public enum DisplayStatus
        {
            Hide = 0,
            Show = 1
        }
        public enum ResultStatus
        {
            Success,
            Error,
            Canceled
        }
        public enum ApprovalStatusId
        {
            Pending = 0,
            Approved = 1,
            Rejected = 2
        }
    }
}
