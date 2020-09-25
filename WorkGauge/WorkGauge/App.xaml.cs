using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.DeviceInfo;
using System.Diagnostics;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace WorkGauge
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent();

            Constants.Height = CrossDevice.Device.ScreenHeight;
            Constants.Width = CrossDevice.Device.ScreenWidth;

            Debug.WriteLine("**" + Constants.Height + "x" + Constants.Width);
            //icon + toppadding
            //Constants.TopRowHeight = 48;
            ////buttons
            //Constants.BottomButtonRowHeight = 48;
            ////main data (results table + searchbar and padding)
            //Constants.DataRowContentHeight =
            //    Constants.Height - (Constants.TopRowHeight + Constants.BottomButtonRowHeight);
            ////Results tables 
            //Constants.ResultsTableHeight = Constants.DataRowContentHeight - 24;
            //Debug.WriteLine(Constants.ResultsTableHeight);

            // The root page of your application
           
            MainPage = new MainPage();
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
