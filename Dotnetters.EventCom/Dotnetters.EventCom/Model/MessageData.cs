using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnetters.EventCom.Model
{
    /// <summary>
    /// Datos necesarios para enviar un mensaje
    /// </summary>
    public class MessageData
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
