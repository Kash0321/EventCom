using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Dotnetters.EventCom.Main
{
    /// <summary>
    /// ViewModel para la vista principal de la aplicación
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        public Command SendCommand { get; set; }

        string userName;
        /// <summary>
        /// Nombre del usuario actual
        /// </summary>
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                SetProperty(ref userName, value);
            }
        }

        string message;
        /// <summary>
        /// Mensaje a enviar
        /// </summary>
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                SetProperty(ref message, value);
            }
        }

        public MainViewModel()
        {
            SendCommand = new Command(() =>
            {
                MessagingCenter.Send(this, "SendMessage");
            });
        }
    }
}
