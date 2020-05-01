using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnyPal
{
    public partial class App : Application
    {
        public static string AnyPalBlue = "#5b9bd5";

        public App()
        {
            InitializeComponent();
            
            MainPage = new Splash();
            //var tabs = new TabbedPage();
            //tabs.Title = "Send Money To PayPal";
            ////tabs.BackgroundColor = Color.Blue;
            //tabs.BarBackgroundColor = Color.FromHex(App.AnyPalBlue);
            //tabs.BarTextColor = Color.White;

            //tabs.Children.Add(new MainPage { Title = "Send Money", IconImageSource = "money.png" });
            //tabs.Children.Add(new Contacts { Title = "Contacts", IconImageSource = "phonebook.png" });
            //tabs.Children.Add(new About { Title = "About", IconImageSource = "info.png" });
            //MainPage = tabs;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            await MainPage.Navigation.PopModalAsync(true);
        }
    }
}
