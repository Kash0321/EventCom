using Dotnetters.EventCom.Model;
using Microsoft.AppCenter.Analytics;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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

        HttpClient HttpClient { get; set; }

        const string REST_URL = "http://eventcom.azurewebsites.net/api/Messages";

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
                if (value != string.Empty)
                    StatusInfo = string.Empty;
            }
        }

        string statusInfo;
        /// <summary>
        /// Información de estado
        /// </summary>
        public string StatusInfo
        {
            get
            {
                return statusInfo;
            }
            set
            {
                SetProperty(ref statusInfo, value);
            }
        }

        /// <summary>
        /// Descripción del error si se ha producido
        /// </summary>
        public string ErrorDescription { get; set; }

        public void Clear()
        {
            Message = string.Empty;
            ErrorDescription = string.Empty;
        }

        public MainViewModel() : base("Comunicador")
        {
            HttpClient = new HttpClient
            {
                MaxResponseContentBufferSize = 256000
            };

            SendCommand = new Command(
                async () =>
                {
                    await SendActionAsync(UserName, Message);
                }, 
                () => 
                {
                    return !IsBusy;
                });
        }

        async Task SendActionAsync(string user, string message)
        {
            var uri = new Uri(REST_URL);
            var onError = false;

            try
            {
                var msgData = new MessageData() { User = UserName, Message = Message };
                var json = JsonConvert.SerializeObject(msgData);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                StatusInfo = "Enviando mensaje...";
                IsBusy = true;
                response = await HttpClient.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    ErrorDescription = response.ToString();
                    onError = true;
                }
            }
            catch (Exception ex)
            {
                ErrorDescription = ex.ToString();
                onError = true;
            }
            finally
            {
                if (!onError)
                {
                    StatusInfo = "Mensaje enviado correctamente";
                    Analytics.TrackEvent(
                        "SendMessage",
                        new Dictionary<string, string> {
                            { "Action", "SendMessage" },
                            { "UserName", UserName },
                            { "Message", Message }
                        });
                    Clear();
                }
                else
                {
                    MessagingCenter.Send(this, "ErrorSendingMessage");
                    StatusInfo = "Error";
                    Analytics.TrackEvent(
                        "SendMessage_Error",
                        new Dictionary<string, string> {
                            { "Action", "SendMessage_Error" },
                            { "UserName", UserName },
                            { "Message", Message },
                            { "Exception", ErrorDescription },
                        });
                }
                IsBusy = false;
            }
        }
    }
}
