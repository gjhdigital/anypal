using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AnyPal
{
    public partial class ContactDetails : ContentPage
    {
        Models.Contact _contact { get; set; }

        public ContactDetails()
        {
            InitializeComponent();
            //_contact = contact as Models.Contact;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            txtEmail.Text = _contact.Email;
        }

        async void btnSave_Clicked(System.Object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text))
            {
                _contact.Email = txtEmail.Text;
                await Task.WhenAll(
                        _contact.UpdateItem(_contact),
                        DisplayAlert("", "Saved", "Ok")
                    );
                //await _contact.UpdateItem(_contact);
                await App.Current.MainPage.Navigation.PopModalAsync(true);
            }
            else
            {
                txtEmail.BackgroundColor = Color.Red;
            }
        }
    }
}
