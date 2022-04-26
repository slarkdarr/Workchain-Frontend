using System;
using System.Collections.Generic;
using System.Net.Http;
using ApplicantTrackingSystem.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApplicantTrackingSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        //public ProfileViewModel vm = new ProfileViewModel();
        public ProfilePage()
        {
            // vm.DisplayInvalidProfilePrompt += () => DisplayAlert("Error", "Invalid Profile Edit, try again", "OK");
            InitializeComponent();


            FullName.Completed += (object sender, EventArgs e) =>
            {
                Headline.Focus();
            };

            Headline.Completed += (object sender, EventArgs e) =>
            {
                Phone.Focus();
            };

            //ProfilePicture.Completed += (object sender, EventArgs e) =>
            //{
            //    BirthDate.Focus();
            //};

            //BirthDate.Completed += (object sender, EventArgs e) =>
            //{
            //    Phone.Focus();
            //};

            Phone.Completed += (object sender, EventArgs e) =>
            {
                Gender.Focus();
            };

            //Gender.Completed += (object sender, EventArgs e) =>
            //{
            //    Country.Focus();
            //};

            Country.Completed += (object sender, EventArgs e) =>
            {
                City.Focus();
            };

            City.Completed += (object sender, EventArgs e) =>
            {
                Description.Focus();
            };

            Description.Completed += (object sender, EventArgs e) =>
            {
                
            };
        }
       

        async void ChangeProfilePicture_Clicked(System.Object sender, System.EventArgs e)
        {
            var pickResult = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Choose an image"
            });

            if (pickResult != null)
            {
                var stream = await pickResult.OpenReadAsync();
                ProfilePic.Source = ImageSource.FromStream(() => stream);

                MultipartFormDataContent content = new MultipartFormDataContent();

                content.Add(new StreamContent(stream), "file", pickResult.FileName);
            }
        }
    }
}
