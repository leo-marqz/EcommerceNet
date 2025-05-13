using Newtonsoft.Json;

namespace Ecommerce.Api.Errors
{
    public class CodeErrorException : CodeErrorResponse
    {
        [JsonProperty(propertyName: "details")]
        public string? Details { get; set; }

        public CodeErrorException(int statusCode, string[]? messages = null, string? details = null) 
        : base(statusCode, messages)
        {
            Details = details;
        }
    }
}
