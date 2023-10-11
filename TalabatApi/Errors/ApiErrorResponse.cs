using Microsoft.AspNetCore.Http;

namespace TalabatApi.Errors
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }

        public string? Message { get; set; }

        public ApiErrorResponse(int statusCode, string? message=null)
        {
            StatusCode = statusCode;
            Message = message is not null ?message:GetMessageForStatusCode(statusCode);
        }

        public string? GetMessageForStatusCode( int StatusCode)
        {
            return StatusCode switch
            {
                 400 =>"Bad Request",
                 401 =>"You Are Not Authoried",
                 404 =>"Resource Not Found",
                 500 =>"Internal Server Error",
                _ => null 
                    
            };

        }
    }
}
