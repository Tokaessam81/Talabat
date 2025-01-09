namespace TalabatAPI.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
       
        public ApiResponse(int statusCode,string? message=null)
        {
            StatusCode = statusCode;
            Message = message??GetMessageOfError(statusCode);
        }
        private string? GetMessageOfError(int StatusCode)
        {
           
            return StatusCode switch
            {
                 401=> "Unauthorized",
                 400=> "BadRequest",
                 404 =>"NotFound",
                 _=>null,
            };
        }
    }
}
