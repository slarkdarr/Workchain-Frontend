using System;
using System.Collections.Generic;
using ApplicantTrackingSystem.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApplicantTrackingSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            var vm = new ProfileViewModel();
            this.BindingContext = vm;
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

        //public async void OnChooseProfilePictureClicked(object sender, EventArgs e)
        //{
        //    (sender as Button).IsEnabled = false;

        //    Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
        //    if (stream != null)
        //    {
        //        image.Source = ImageSource.FromStream(() => stream);
        //    }

        //    (sender as Button).IsEnabled = true;
        //}
    }
}
