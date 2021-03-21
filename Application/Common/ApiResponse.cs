using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Application.Common
{
    public class ApiResponse  //<T>
    {
        public ApiResponse()
        {
        }
        
        public ApiResponse(object data, string message = null)
        {
            IsSuccess = true;
            Message = message != null ? message : "success";
            Errors = null;
            Data = data;
            StatusCode = HttpStatusCode.OK;
        }

        public ApiResponse(string[] errors, string message = "error", HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            IsSuccess = false;
            Message = message;
            Errors = errors;
            StatusCode = statusCode;
        }

        // public T Data { get; set; }
        public object Data { get; set; }
        public bool IsSuccess { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
