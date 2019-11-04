using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace API.Helpers
{
    public class ApiError
    {
        public int StatusCode { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; private set; }

        public ApiError(int statusCode)
        {
            StatusCode = statusCode;
        }

        public ApiError(int statusCode, string message, ILogger logger)
            : this(statusCode)
        {
            Message = message;
            Task.Delay(10).ContinueWith(t =>
            {
                logger.LogError(message);
            });
        }
    }
}
