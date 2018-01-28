using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnetters.EventCom.Web.Models
{
    /// <summary>
    /// Datos de envío de un mensaje desde un dispositivo remoto
    /// </summary>
    public class MessagePost
    {
        /// <summary>
        /// Nombre del usuario que envía el mensaje
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Mensaje enviado por el usuario
        /// </summary>
        public string Message { get; set; }
    }
}
