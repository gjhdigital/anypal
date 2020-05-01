using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AnyPal
{
    public partial class ppweb : ContentPage
    {
        private Models.Payment payment { get; set; }
        public ppweb(Models.Payment p)
        {
            InitializeComponent();
            payment = p as Models.Payment;
            //btnSaveRecipient.IsVisible = false;
        }
        
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //https://www.paypal.com/cgi-bin/webscr?cmd=_cart&business=gjhdigital@hotmail.com&add=1&quantity=1&item_number=001&amount=0.99&item_name=some%20stuff&bn=gjhdigital

            if (string.IsNullOrEmpty(payment.ItemNumber))
            {
                payment.ItemNumber = "001";
            }
            string url = "https://www.paypal.com/cgi-bin/webscr?cmd=_cart";
            url += "&business="+payment.Email+"&add=1&quantity=1";
            url += "&item_number=" + payment.ItemNumber + "&amount=" + payment.Amount;
            url += "&item_name="+payment.ItemName+"&bn=gjhdigital";

            //await Launcher.OpenAsync(url);

            //await btnSaveRecipient.FadeTo(1.0, 5000);

            //btnSaveRecipient.IsVisible = true;
            //await Browser.OpenAsync(url, new BrowserLaunchOptions
            //{
            //    LaunchMode = BrowserLaunchMode.SystemPreferred,
            //    TitleMode = BrowserTitleMode.Hide,
            //    PreferredToolbarColor = Color.Blue,
            //    PreferredControlColor = Color.White
            //});
            //await btnSaveRecipient.FadeTo(0.0, 50);
            ////await Task.WhenAll(
            ////    Browser.OpenAsync(url, new BrowserLaunchOptions
            ////    {
            ////        LaunchMode = BrowserLaunchMode.SystemPreferred,
            ////        TitleMode = BrowserTitleMode.Default,
            ////        PreferredToolbarColor = Color.Blue,
            ////        PreferredControlColor = Color.White
            ////    })
            ////);

            //await btnSaveRecipient.FadeTo(1.0, 5000);
        }

        void btnSaveRecipient_Clicked(System.Object sender, System.EventArgs e)
        {
            Models.Contact c = new Models.Contact();
            c.Email = payment.Email;
            bool bSaved = c.AddItem(c);

            if (bSaved)
            {
                lblMessage.Text = payment.Email + " has been added to your AnyPal contacts.";
                lblMessage.TextColor = Color.Green;
            }
            else
            {
                lblMessage.Text = "Sorry, " + payment.Email + " could not be added to your AnyPal contacts.";
                lblMessage.TextColor = Color.Red;
            }
        }
    }
}
