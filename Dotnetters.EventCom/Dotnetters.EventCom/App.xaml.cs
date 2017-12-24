using Dotnetters.EventCom.Main;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Dotnetters.EventCom
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new MainView());
		}

		protected override void OnStart ()
		{
            AppCenter.Start(
                "android=4a93ea48-35d3-4b5d-acd2-0e32f44b1abe;" +
                "ios=7d7dac6a-8c36-4f71-8722-846a2c7bec82;",
                typeof(Analytics), 
                typeof(Crashes));
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
