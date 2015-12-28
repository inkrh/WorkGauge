using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace WorkGauge
{
	public class App : Application
	{
		public App ()
		{
			Constants.Height = Acr.DeviceInfo.DeviceInfo.Instance.ScreenHeight;
			Constants.Width = Acr.DeviceInfo.DeviceInfo.Instance.ScreenWidth;

			Debug.WriteLine ("**" + Constants.Height + "x" + Constants.Width);
			//icon + toppadding
			Constants.TopRowHeight = 48;
			//buttons
			Constants.BottomButtonRowHeight = 42;
			//main data (results table + searchbar and padding)
			Constants.DataRowContentHeight = 
				Constants.Height - (40 + Constants.TopRowHeight + Constants.BottomButtonRowHeight);
			//Results tables 
			Constants.ResultsTableHeight = Constants.DataRowContentHeight - 24;


			// The root page of your application
			MainPage = new MainContent();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
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

