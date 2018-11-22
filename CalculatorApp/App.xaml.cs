using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CalculatorApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var calc = new Calc
            {
                BackgroundColor = Color.FromHex("#343434")
            };
            MainPage = calc;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
