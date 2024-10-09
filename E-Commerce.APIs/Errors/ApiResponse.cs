
namespace E_Commerce.APIs.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Massage { get; set; }
        public ApiResponse(int statusCode , string? massage = null)
        {
            StatusCode=statusCode;
            Massage = massage ?? GetDefaultMassageForStatusCode(statusCode);
        }

        private string? GetDefaultMassageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request , you have made",
                404 => "Authorized, you are not",
                405 => "Resource was not found",
                500 => "Error are the path to the dark side , Error lead to anger.",
                _ => null
            };
        }
    }
}
