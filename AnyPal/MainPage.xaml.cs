using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AnyPal
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public List<Models.Contact> ContactEnum { get; set; }
        TapGestureRecognizer tapper = new TapGestureRecognizer();

        public MainPage()
        {
            InitializeComponent();
            
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            btnPayNow.Source = ImageSource.FromResource("AnyPal.Images.paynowbutton.png");
            btnContacts.Source = ImageSource.FromResource("AnyPal.Images.contacts.png");
            
            ButtonBackgrounds();

            Models.Contact contact = new Models.Contact();
            ContactEnum = await contact.GetContacts();
        }


        private void ButtonBackgrounds()
        {
            txtAmount.BackgroundColor = Color.AliceBlue;
            txtEmail.BackgroundColor = Color.AliceBlue;
            txtItemName.BackgroundColor = Color.AliceBlue;
            txtItemNumber.BackgroundColor = Color.AliceBlue;
        }

        private void ClearForm()
        {
            txtAmount.Text = "";
            txtEmail.Text = "";
            txtItemName.Text = "";
            txtItemNumber.Text = "";
        }
        
        async void btnPayNow_Clicked(System.Object sender, System.EventArgs e)
        {
            string msg = "";// IsValid();
            Entry obj = IsValid2() as Entry;
            
            //if (!string.IsNullOrEmpty(msg))
            if (obj != null)
            {
                //await DisplayAlert("Error", msg, "Ok");

                Device.BeginInvokeOnMainThread(async () =>
                {
                    try
                    {
                        await obj.TranslateTo(-15, 0, 50);
                        await obj.TranslateTo(15, 0, 50);
                        await obj.TranslateTo(-10, 0, 50);
                        await obj.TranslateTo(10, 0, 50);
                        await obj.TranslateTo(-5, 0, 50);
                        await obj.TranslateTo(5, 0, 50);
                        obj.TranslationX = 0;
                    }
                    finally
                    {
                        //_isAnimating = false;
                    }
                });
            }
            else
            {
                Models.Payment payment = new Models.Payment()
                {
                    ItemName = txtItemName.Text,
                    Amount = decimal.Parse(txtAmount.Text),
                    ItemNumber = txtItemNumber.Text,
                    Email = txtEmail.Text
                };
                //await Navigation.PushModalAsync(new ppweb(payment), true);
                if (string.IsNullOrEmpty(payment.ItemNumber))
                {
                    payment.ItemNumber = "001";
                }
                //string url = "https://www.paypal.com/cgi-bin/webscr?cmd=_cart";
                //url += "&business=" + payment.Email + "&add=1&quantity=1";
                //url += "&item_number=" + payment.ItemNumber + "&amount=" + payment.Amount;
                //url += "&item_name=" + payment.ItemName + "&bn=gjhdigital";

                string url = new Models.PayPalUrl().PayPalLink(payment);

                //await Launcher.OpenAsync(url);
                await Task.WhenAll(
                    Browser.OpenAsync(url, new BrowserLaunchOptions
                    {
                        LaunchMode = BrowserLaunchMode.SystemPreferred,
                        TitleMode = BrowserTitleMode.Default,
                        PreferredToolbarColor = Color.FromHex(App.AnyPalBlue),
                        PreferredControlColor = Color.White
                    })
                );
                //await Navigation.PushModalAsync(new ppweb(payment), true);

                //await DisplayAlert("", "Add New Contact", "Ok");
                lblSaveContact.Text = "Save " + payment.Email + " to AnyPal contacts?";
                frmAddContact.IsVisible = true;
                frmPayment.IsVisible = false;
            }
        }

        object IsValid2()
        {
            if (string.IsNullOrEmpty(txtItemName.Text))
            {
                return txtItemName as Entry;// as object;
            }
            if (string.IsNullOrEmpty(txtAmount.Text))
            {
                return txtAmount as Entry;// as object;
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                return txtEmail as Entry;// as object;
            }
            if (!string.IsNullOrEmpty(txtEmail.Text))
            {
                if (txtEmail.Text.Contains("@") == false)
                    return txtEmail as Entry;
                else if (txtEmail.Text.Contains(".") == false)
                    return txtEmail as Entry;
                else return null;
            }
            else
                return null;

        }

        string IsValid()
        {
            string msg = string.Empty;

            if (string.IsNullOrEmpty(txtAmount.Text))
            {
                msg += "- Amount is Required" + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(txtItemName.Text))
            {
                msg += "- Payment For is Required" + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                msg += "- Recipient Email is Required" + Environment.NewLine;
            }
            //if (!string.IsNullOrEmpty(txtEmail.Text))
            //{
            //    if(txtAmount.Text.Trim().Contains("@") == false)
            //    msg += "- Recipient Email is Required" + Environment.NewLine;

            //    if (txtAmount.Text.Trim().Contains(".") == false)
            //        msg += "- Recipient Email is Required" + Environment.NewLine;
            //}

            return msg;
        }

        async void btnContacts_Clicked(System.Object sender, System.EventArgs e)
        {
            if (ContactEnum.Count > 0)
            {
                string[] sContacts = new string[ContactEnum.Count];
                for(int i =0; i < ContactEnum.Count;  i++)
                {
                    sContacts[i] = ContactEnum[i].Email;
                }
                string action = await DisplayActionSheet("Select From Contacts", "Cancel", null, sContacts);
                if (action.Equals("Cancel") == false)
                {
                    txtEmail.Text = action;
                    Models.Contact c = ContactEnum.Where(x => x.Email == action).FirstOrDefault();
                    if (!string.IsNullOrEmpty(c.Name))
                    {
                        txtItemName.Text = c.Name;
                    }
                }
            }
            else
            {
                await DisplayAlert("No Contacts", "Sorry, you currently do not have an AnyPal contact saved.", "Ok");
            }
            
        }

        async void btnNewContact_Clicked(System.Object sender, System.EventArgs e)
        {
            Models.Contact c = new Models.Contact();
            c.Email = txtEmail.Text;
            c.Name = txtItemName.Text;
            bool bSaved = c.AddItem(c);

            if (bSaved)
            {
                ContactEnum = await c.GetContacts();
                await DisplayAlert("Success", "Your contact has been saved.", "Ok");                
            }
            else
            {
                await DisplayAlert("Error", "Sorry, your contact could not saved.", "Ok");
            }
            ClearForm();
            frmAddContact.IsVisible = false;
            frmPayment.IsVisible = true;
        }

        void btnCancelContact_Clicked(System.Object sender, System.EventArgs e)
        {
            ClearForm();
            frmAddContact.IsVisible = false;
            frmPayment.IsVisible = true;
        }
        async void btnTryAgain_Clicked(System.Object sender, System.EventArgs e)
        {
            Models.Payment payment = new Models.Payment()
            {
                ItemName = txtItemName.Text,
                Amount = decimal.Parse(txtAmount.Text),
                ItemNumber = txtItemNumber.Text,
                Email = txtEmail.Text
            };
            //await Navigation.PushModalAsync(new ppweb(payment), true);
            if (string.IsNullOrEmpty(payment.ItemNumber))
            {
                payment.ItemNumber = "001";
            }
            string url = new Models.PayPalUrl().PayPalLink(payment);

            //await Launcher.OpenAsync(url);
            await Task.WhenAll(
                Browser.OpenAsync(url, new BrowserLaunchOptions
                {
                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                    TitleMode = BrowserTitleMode.Default,
                    PreferredToolbarColor = Color.FromHex(App.AnyPalBlue),
                    PreferredControlColor = Color.White
                })
            );
        }
    }
}
