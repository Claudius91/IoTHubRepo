using System;
using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventHubs;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using SharedLib;

namespace AzureFunction
{
    public static class Function1
    {
        private static HttpClient client = new HttpClient();

        [FunctionName("Function1")]
        public static async Task Run([IoTHubTrigger("messages/events", Connection = "IoTButtonConnectionString")]EventData message, ILogger log)
        {
            string receivedMessage = Encoding.UTF8.GetString(message.Body.Array);
            log.LogInformation($"C# IoT Hub trigger function processed a message: {message}");

            try
            {
                await client.PostAsJsonAsync(new UrlService().PostOrderUrl, message);
            }
            catch (Exception e)
            {
                log.LogDebug(e.Message);
            }
        }
    }
}