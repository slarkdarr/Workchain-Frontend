using System;
using System.Collections.Generic;
using ApplicantTrackingSystem.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApplicantTrackingSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ApplicantProfilePage : ContentPage
    {
        public ApplicantProfilePage()
        {
            var vm = new ApplicantProfileViewModel();
            this.BindingContext = vm;
            // vm.DisplayInvalidProfilePrompt += () => DisplayAlert("Error", "Invalid Profile Edit, try again", "OK");
            InitializeComponent();

            ApplicantName.Completed += (object sender, EventArgs e) =>
            {
                Password.Focus();
            };

            Password.Completed += (object sender, EventArgs e) =>
            {
                ProfilePicture.Focus();
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
                Headline.Focus();
            };

            Headline.Completed += (object sender, EventArgs e) =>
            {
                Description.Focus();
            };

            Description.Completed += (object sender, EventArgs e) =>
            {
                vm.SubmitCommand.Execute(null);
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