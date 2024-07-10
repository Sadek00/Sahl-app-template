using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Domain.Utilities
{
    public class ResponseModel
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}
