using Microsoft.AppCenter.Analytics;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Dotnetters.EventCom.Main
{
    /// <summary>
    /// ViewModel para la vista principal de la aplicación
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        public Command SendCommand { get; set; }

        public HubConnection HubConnection { get; set; }

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
            HubConnection = new HubConnection("http://192.168.1.133:54762");

            SendCommand = new Command(async () =>
            {
                await SendActionAsync(UserName, Message);
            });
        }

        async Task SendActionAsync(string user, string message)
        {
            MessagingCenter.Send(this, "SendMessage");

            Analytics.TrackEvent(
                "Main",
                new Dictionary<string, string> {
                                    { "Action", "SendMessage" },
                                    { "UserName", UserName },
                                    { "Message", Message }
                });

            if (HubConnection.State == ConnectionState.Disconnected)
            {
                try
                {
                    await HubConnection.Start();
                }
                catch (Exception ex)
                {
                    Analytics.TrackEvent(
                        "Main",
                        new Dictionary<string, string> {
                                                        { "Action", "SendMessageError" },
                                                        { "UserName", UserName },
                                                        { "Message", Message },
                                                        { "Exception", ex.ToString() },
                        });
                }
            }

            IHubProxy messagingHubProxy = HubConnection.CreateHubProxy("MessagingHub");
            await messagingHubProxy.Invoke("Send", user, message);
        }
    }
}
