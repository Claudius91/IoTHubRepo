using System;

namespace SharedLib
{
    /// <summary>
    /// The url service class.
    /// </summary>
    public class UrlService
    {
        /// <summary>
        /// The base URI reference.
        /// </summary>
        private const string BaseUri = "http://localhost:52083";

        public string IoTHubConnectionAddress = $"{BaseUri}/iotHub";

        /// <summary>
        /// The post URL for IoT messages.
        /// </summary>
        public string PostOrderUrl = $"{BaseUri}/IoTButtonController";
    }
}
