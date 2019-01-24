using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading;
using SharedLib;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json.Linq;

namespace Client
{
   public class IoTViewModel
    {
        /// <summary>
        /// The hub connection.
        /// </summary>
        private HubConnection hubConnection;

        private UrlService urlService;

        private ILogger<IoTViewModel> logger;

        public IoTViewModel(UrlService service, ILogger<IoTViewModel> logger)
        {
            this.urlService = service;
            this.logger = logger;
        }

        public void Connect()
        {
            CloseConnectionAsync().Wait();
            hubConnection = new HubConnectionBuilder()
                .WithUrl(this.urlService.IoTHubConnectionAddress)
                .ConfigureLogging(loggingBuilder => loggingBuilder.AddDebug())
                .Build();

            hubConnection.Closed += HubConnectionClosed;
            hubConnection.On<string>("HubMessage", OnMessageReceived);

            do
            {
                try
                {
                    Console.WriteLine("Connecting...");
                    hubConnection.StartAsync().Wait();
                    Console.WriteLine("Connected.");
                    Console.Clear();
                    break;
                }
                catch (HttpRequestException ex)
                {
                    this.logger.LogCritical(ex.Message);
                    this.logger.LogCritical(ex.StackTrace);
                }
                catch (Exception e)
                {
                    this.logger.LogError(e.Message);
                    this.logger.LogError(e.StackTrace);
                }
            }
            while (true);

            Wait();
        }

        private void Wait()
        {
            do
            {
                Thread.Sleep(400);
            } while (true);
        }
        

        private void OnMessageReceived(string message)
        {
            Console.WriteLine(message);
            JObject json = JObject.Parse(message);
            Console.WriteLine(json.GetValue("id"));
            //IDictionary<string,object> dict = Json.JsonParser.FromJson(message);
            //dict.Keys.ToList().ForEach(x => Console.WriteLine(x));
        }


        /// <summary>
        /// Hubs the connection closed.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns></returns>
        private Task HubConnectionClosed(Exception arg)
        {
            this.logger.LogError("Hub connection closed. Restart application!");
            this.logger.LogInformation("Terminating in 3 seconds...");
            Thread.Sleep(3000);
            Environment.Exit(1);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Closes the connection asynchronous.
        /// </summary>
        /// <returns></returns>
        private Task CloseConnectionAsync()
            => this.hubConnection?.DisposeAsync() ?? Task.CompletedTask;

    }
}
