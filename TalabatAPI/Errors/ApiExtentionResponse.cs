
namespace TalabatAPI.Errors
{
    public class ApiExtentionResponse:ApiResponse
    {
        public string? _Details { get; set; }
        public ApiExtentionResponse(int statusCode,string?Message=null,string?Details=null):base(statusCode,Message)
        {
            _Details = Details;
        }

    }
}
