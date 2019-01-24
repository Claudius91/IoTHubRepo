

namespace Server.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Resources;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The waiter hub.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.SignalR.Hub" />
    public class IoTHub : Hub
    {
        /// <summary>
        /// The waiter identifier to connection identifier dictionary.
        /// </summary>
        private readonly Dictionary<string, string> waiterIdToConnectionIdDict;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<IoTHub> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="WaiterHub"/> class.
        /// </summary>
        /// <param name="waiterConfig">The waiter configuration.</param>
        /// <param name="logger">The logger.</param>
        public IoTHub(ILogger<IoTHub> logger)
        {
            this.waiterIdToConnectionIdDict = new Dictionary<string, string>();
            this.logger = logger;
        }

        /// <summary>
        /// Sends the notification to waiter asynchronous.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="tableNumber">The table number.</param>
        /// <returns>A task for async execution.</returns>
        /// <exception cref="ArgumentException">Could not find waiter to table number '{tableNumber}'. - tableNumber</exception>
        public Task SendNotification(string message)
        {
            try
            {
                return Clients.All.SendAsync("HubMessage", message);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, "Could not send to clients.");
                throw e;
            }
        }

        /// <summary>
        /// Called when a new connection is established with the hub.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous connect.
        /// </returns>
        public override Task OnConnectedAsync() => base.OnConnectedAsync();

        /// <summary>
        /// Called when a connection with the hub is terminated.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous disconnect.
        /// </returns>
        public override Task OnDisconnectedAsync(Exception exception) => base.OnDisconnectedAsync(exception);
    }
}
