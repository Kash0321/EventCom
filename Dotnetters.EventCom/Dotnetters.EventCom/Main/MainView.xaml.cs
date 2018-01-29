using Dotnetters.EventCom.Main;
using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Dotnetters.EventCom.Main
{
	public partial class MainView : ContentPage
	{
        MainViewModel ViewModel { get; set; }

		public MainView()
		{
			InitializeComponent();

            ViewModel = new MainViewModel();
            BindingContext = ViewModel;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Analytics.TrackEvent(
                "Main",
                new Dictionary<string, string> {
                    { "Action", "OnAppearing" }
                });

            SubscribeToMessages();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            Analytics.TrackEvent(
                "Main",
                new Dictionary<string, string> {
                    { "Action", "OnDisappearing" }
                });

            UnsubscribeFromMessages();
        }

        void SubscribeToMessages()
        {
            MessagingCenter.Subscribe<MainViewModel>(this, "SendMessage", (vm) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert($"Mensaje enviado como {vm.UserName}", vm.Message, "OK");
                    vm.Clear();
                });
            });

            MessagingCenter.Subscribe<MainViewModel>(this, "ErrorSendingMessage", (vm) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Se ha producido un error enviando el mensaje", vm.ErrorDescription, "OK");
                    vm.Clear();
                });
            });
        }

        void UnsubscribeFromMessages()
        {
            MessagingCenter.Unsubscribe<MainViewModel>(this, "SendMessage");
            MessagingCenter.Unsubscribe<MainViewModel>(this, "ErrorSendingMessage");
        }
    }
}
