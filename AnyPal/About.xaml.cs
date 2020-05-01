using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AnyPal
{
    public partial class About : ContentPage
    {
        public About()
        {
            InitializeComponent();
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Button btnVisit = new Button();
            btnVisit.Clicked += BtnVisit_Clicked;
            btnVisit.Text = "AnyPal® website";

            stk.Children.Clear();

            stk.Padding = new Thickness(10, 20, 5, 0);
            stk.HorizontalOptions = LayoutOptions.CenterAndExpand;
            stk.VerticalOptions = LayoutOptions.FillAndExpand;
            stk.Children.Add(new Image { Source = "anypal.PNG", WidthRequest = 150 });
            stk.Children.Add(new Label { Text = "The AnyPal® app allows you to send money to anyone's PayPal® account. You do not need to have a PayPal® account. All you need is the PayPal® email address of the person you are sending money to." });
            stk.Children.Add(new Label { Text = "PayPal® takes a percentage of the money for all transactions. Please visit www.Paypal.com for more details." });
            stk.Children.Add(new Label { Text = "All money is sent to PayPal® and the AnyPal® app does not store any personal information, optionally you can save their email address and name." });
            stk.Children.Add(new Label { Text = "For more information, please visit: AnyPal®" });
            stk.Children.Add(btnVisit);

            //Content = new StackLayout
            //{
            //    Padding = new Thickness(10, 20, 5, 0),
            //    HorizontalOptions = LayoutOptions.CenterAndExpand,
            //    VerticalOptions = LayoutOptions.StartAndExpand,
            //    Children = {
            //        //new Image {Source = "anypal.PNG" },
            //        new Label { Text = "The AnyPal® app allows you to send money to anyone's PayPal® account. You do not need to have a PayPal® account. All you need is the PayPal® email address of the person you are sending money to." },
            //        new Label { Text = "PayPal® takes a percentage of the money for all transactions. Please visit www.Paypal.com for more details." },
            //        new Label { Text = "All money is sent to PayPal® and the AnyPal® app does not store any personal information, optionally you can save their email address and name." },
            //        new Label {Text = "For more information, please visit: AnyPal®"},
            //        btnVisit

            //    }
            //};
        }

        private async void BtnVisit_Clicked(object sender, EventArgs e)
        {
            await Launcher.TryOpenAsync("http://gjhdigital.com/anypal");
        }
    }
}
