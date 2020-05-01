using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AnyPal
{
    public partial class Contacts : ContentPage
    {
        public List<Models.Contact> ContactsEnum { get; set; }
        Models.Contact contact = new Models.Contact();
        TapGestureRecognizer tapper = new TapGestureRecognizer();
        bool bAddNew = false;
        bool itemTapped { get; set; }
        public Contacts()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var tapGesture = new TapGestureRecognizer();
            //tapGestureRecognizer.Tapped += (s, e) => {
            //    // handle the tap
            //};
            //tapper.Tapped += TapGesture_Tapped;

            

            contact = new Models.Contact();
            ContactsEnum = new List<Models.Contact>();
            //ContactsEnum = await contact.GetContacts();
            //lstContacts.ItemsSource = ContactsEnum;
            BindData();
            lstContacts.ItemTapped += LstContacts_ItemTapped;
            //BuildList();
            //itemTapped = false;
        }
        private async void BindData()
        {
            ContactsEnum = await contact.GetContacts();
            lstContacts.ItemsSource = ContactsEnum;
        }
        private async void LstContacts_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if(lstContacts.SelectedItem != null)
            {
                Models.Contact contact = e.Item as Models.Contact;
                lstContacts.SelectedItem = null;
                await Prompt(contact);
                //await App.Current.MainPage.Navigation.PushModalAsync(new Details(), true);
            }
        }

        private async Task Prompt(Models.Contact contact)
        {
            string action = await DisplayActionSheet("Edit or Delete", "Cancel", null, "Edit", "Delete");
            //string action = await DisplayActionSheet("Contact", "Cancel", "Edit", "Delete");
            if (action.Equals("Cancel"))
            {

            }
            if (action.Equals("Delete"))
            {
                bool answer = await DisplayAlert("Confirm Delete", contact.Email, "Yes", "Cancel");
                if (answer)
                {
                    await contact.DeleteItem(contact);
                    //itemTapped = true;
                    ContactsEnum = await contact.GetContacts();
                    BindData();
                    await FadeDetailOut();
                }
            }
            if (action.Equals("Edit"))
            {
                await FadeDetailIn();

                //await frmContacts.FadeTo(0.0, 500, Easing.Linear);
                //frmDetails.IsVisible = true;
                //frmContacts.IsVisible = false;
                txtEmail.Text = contact.Email;
                txtName.Text = contact.Name;
                
            }
        }

        private async Task FadeDetailIn()
        {
            await Task.WhenAll(
                    frmContacts.FadeTo(0.0, 200, Easing.Linear)
                    );
            frmContacts.IsVisible = false;
            frmDetails.IsVisible = true;
            await Task.WhenAll(
                frmDetails.FadeTo(1.0, 200, Easing.Linear)
                );
        }

        private async Task FadeDetailOut()
        {
            await Task.WhenAll(
                   frmDetails.FadeTo(0.0, 200, Easing.Linear)
                    );
            frmContacts.IsVisible = true;
            frmDetails.IsVisible = false;
            await Task.WhenAll(
                frmContacts.FadeTo(1.0, 200, Easing.Linear)
                );
        }

        async void btnSave_Clicked(System.Object sender, System.EventArgs e)
        {
            bool isgood = false;
            if (txtEmail.Text != null)
            {
                if (txtEmail.Text.Trim().Length > 7)
                {
                    if (txtEmail.Text.Contains("@") && txtEmail.Text.Contains("."))
                        isgood = true;

                    if (isgood)
                    {
                        contact.Email = txtEmail.Text;
                        contact.Name = txtName.Text;
                        if (bAddNew)
                        {
                            bool addednew = contact.AddItem(contact);
                        }
                        else
                        {
                            bool added = await contact.UpdateItem(contact);
                        }
                        BindData();
                        await FadeDetailOut();
                    }
                }
            }
            if(!isgood)
            {
                await DisplayAlert("Error", "Email is not valid", "Ok");
            }
        }

        async void btnCancel_Clicked(System.Object sender, System.EventArgs e)
        {
            ClearForm();
            await FadeDetailOut();
        }
        private void ClearForm()
        {
            txtEmail.Text = "";
            txtName.Text = "";
            bAddNew = false;
        }

        async void btnAddNew_Clicked(System.Object sender, System.EventArgs e)
        {
            bAddNew = true;
            await FadeDetailIn();
        }
    }
}
