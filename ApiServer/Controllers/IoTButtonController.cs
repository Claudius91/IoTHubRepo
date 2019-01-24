using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Manager;

namespace ApiServer.Controllers
{
    [Route("/IoTButtonController")]
    [ApiController]
    public class IoTButtonController : ControllerBase
    {
        private readonly IHub hub;
        private readonly ILogger<IoTButtonController> logger;

        public IoTButtonController(IHub hub, ILogger<IoTButtonController> logger)
        {
            this.hub = hub;
            this.logger = logger;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] string value)
        {
            try
            {
                await this.hub.SendNotification(value);
            }
            catch (Exception e)
            {
                this.logger.LogDebug(e.Message);
                return NotFound();
            }

            return Ok();
        }
    }
}
