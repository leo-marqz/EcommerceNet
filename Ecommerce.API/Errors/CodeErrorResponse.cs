using Newtonsoft.Json;

namespace Ecommerce.Api.Errors
{
    public class CodeErrorResponse
    {
        [JsonProperty(propertyName: "statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty(propertyName: "messages")]
        public string[]? Messages { get; set; }

        public CodeErrorResponse(int statusCode, string[]? messages = null)
        {
            StatusCode = statusCode;
            if(messages is null)
            {
                Messages = new string[0];
                var text = GetDefaultMessageStatusCode(statusCode);
                
                Messages[0] = text;
            }
            else
            {
                Messages = messages;
            }
        }

        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "El request enviado contiene errores",
                401 => "No estas autorizado en este recurso",
                404 => "No se encontro el recurso solicitado",
                500 => "Se produjeron errores del lado del servidor",
                _ => "Unknown Error"
            };
        }
    }
}
