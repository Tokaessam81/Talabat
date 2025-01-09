namespace TalabatAPI.Errors
{
    public class ApiResponseValidationError:ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiResponseValidationError():base(400)
        {
            Errors=new List<string>();
            
        }
    }
}
