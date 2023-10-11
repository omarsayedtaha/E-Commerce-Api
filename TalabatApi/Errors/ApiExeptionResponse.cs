namespace TalabatApi.Errors
{
    public class ApiExeptionResponse:ApiErrorResponse
    {
        private readonly string? Details;

        public ApiExeptionResponse(int statuscode,string? Message = null , string? details = null):base(statuscode, Message)
        {
            Details = details;
        }
    }
}
