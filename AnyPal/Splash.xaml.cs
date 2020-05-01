using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AnyPal
{
    public partial class Splash : ContentPage
    {
        public Splash()
        {
            InitializeComponent();
            //this.BackgroundImageSource = ImageSource.FromResource("AnyPal.Images.splash.png");
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            imgSplash.Opacity = 0.0;
            imgSplash.Source= ImageSource.FromResource("AnyPal.Images.splash.png");
            await Task.WhenAll(
                imgSplash.FadeTo(1.0, 500, Easing.Linear)
                );

            var tabs = new TabbedPage();
            tabs.Title = "Send Money To PayPal";
            //tabs.BackgroundColor = Color.Blue;
            tabs.BarBackgroundColor = Color.FromHex(App.AnyPalBlue);
            tabs.BarTextColor = Color.White;

            tabs.Children.Add(new MainPage { Title = "Send Money", IconImageSource = "money.png" });
            tabs.Children.Add(new Contacts { Title = "Contacts", IconImageSource = "phonebook.png" });
            tabs.Children.Add(new About { Title = "About", IconImageSource = "info.png" });

            await Task.Delay(1200);
            await Task.WhenAll(
                imgSplash.ScaleTo(0.0, 250, Easing.Linear),
                imgSplash.FadeTo(0.0, 250, Easing.Linear)
                );

            App.Current.MainPage = tabs;
        }
    }
}
