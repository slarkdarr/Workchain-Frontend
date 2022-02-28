using System;
using System.Collections.Generic;
using ApplicantTrackingSystem.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Xaml;
using System.IO;

namespace ApplicantTrackingSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompanyProfilePage : ContentPage
    {
        public CompanyProfilePage()
        {
            var vm = new CompanyProfileViewModel();
            this.BindingContext = vm;
            // vm.DisplayInvalidProfilePrompt += () => DisplayAlert("Error", "Invalid Profile Edit, try again", "OK");
            InitializeComponent();

            CompanyName.Completed += (object sender, EventArgs e) =>
            {
                Password.Focus();
            };

            Password.Completed += (object sender, EventArgs e) =>
            {
                ProfilePicture.Focus();
            };

            //ProfilePicture.Completed += (object sender, EventArgs e) =>
            //{
            //    FoundDate.Focus();
            //};

            //FoundDate.Completed += (object sender, EventArgs e) =>
            //{
            //    Phone.Focus();
            //};

            Phone.Completed += (object sender, EventArgs e) =>
            {
                Country.Focus();
            };

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
