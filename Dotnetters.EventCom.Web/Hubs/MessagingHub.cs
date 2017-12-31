using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnetters.EventCom.Web.Hubs
{
    /// <summary>
    /// Hub para la emisión en tiempo real de los mensajes recibidos desde la APP
    /// </summary>
    public class MessagingHub : Hub
    {
        /// <summary>
        /// Emite un mensaje a todos los clientes del HUB
        /// </summary>
        /// <param name="user">Nick del usuario que envía el mensaje</param>
        /// <param name="message">Texto del mensaje</param>
        /// <returns></returns>
        public Task Send(string user, string message)
        {
            return Clients.All.InvokeAsync("Send", user, message);
        }
    }
}
