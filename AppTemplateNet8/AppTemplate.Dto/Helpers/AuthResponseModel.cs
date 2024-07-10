using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Dto.Helpers
{
    public class AuthResponseModel
    {
        public string? Access_token { get; set; }
        public string? Refresh_token { get; set; }
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }
        public object? User { get; set; }
    }
}
