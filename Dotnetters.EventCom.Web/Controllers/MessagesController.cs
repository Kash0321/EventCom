using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnetters.EventCom.Web.Hubs;
using Dotnetters.EventCom.Web.Models;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Dotnetters.EventCom.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Messages")]
    public class MessagesController : Controller
    {
        IHubContext<MessagingHub> MessagingHubContext { get; set; }

        /// <summary>
        /// Inicializa una instancia de <see cref="MessagesController"/>
        /// </summary>
        /// <param name="messagingHubContext"></param>
        public MessagesController(IHubContext<MessagingHub> messagingHubContext)
        {
            MessagingHubContext = messagingHubContext;
        }

        // POST: api/Messages
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MessagePost messageData)
        {
            await MessagingHubContext.Clients.All.InvokeAsync("Send", messageData.User, messageData.Message);

            return Ok(messageData);
        }
    }
}
