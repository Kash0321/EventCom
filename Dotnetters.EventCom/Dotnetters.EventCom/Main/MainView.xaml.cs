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
                "MainView",
                new Dictionary<string, string> {
                    { "Action", "OnAppearing" }
                });

            SubscribeToMessages();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            Analytics.TrackEvent(
                "MainView",
                new Dictionary<string, string> {
                    { "Action", "OnDisappearing" }
                });

            UnsubscribeFromMessages();
        }

        void SubscribeToMessages()
        {
            MessagingCenter.Subscribe<MainViewModel>(this, "SendMessage", (vm) =>
            {
                Analytics.TrackEvent(
                    "MainView",
                    new Dictionary<string, string> {
                        { "Action", "SendMessage" },
                        { "UserName", vm.UserName },
                        { "Message", vm.Message }
                    });
                Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Mensaje enviado", vm.UserName + ": " + vm.Message, "OK");
                });
            });
        }

        void UnsubscribeFromMessages()
        {
            MessagingCenter.Unsubscribe<MainViewModel>(this, "SendMessage");
        }
    }
}
