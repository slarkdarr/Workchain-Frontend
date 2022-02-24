using System;
using System.Collections.Generic;
using ApplicantTrackingSystem.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApplicantTrackingSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            var vm = new LoginViewModel();
            this.BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Login, try again", "OK");
            InitializeComponent();

            Email.Completed += (object sender, EventArgs e) =>
            {
                Password.Focus();
            };

            Password.Completed += (object sender, EventArgs e) =>
            {
                vm.SubmitCommand.Execute(null);
            };
        }

        private void FrameFocused(object sender, FocusEventArgs e)
        {
            var entry = (Entry)sender;
            var classId = entry.ClassId;
            var identifier = classId + "Frame";

            var element = (Frame)loginCard.FindByName(identifier);
          
            element.BorderColor = Color.FromHex("#58327F");
        }

        private void FrameUnfocused(object sender, FocusEventArgs e)
        {
            var entry = (Entry)sender;
            var classId = entry.ClassId;
            var identifier = classId + "Frame";

            var element = (Frame)loginCard.FindByName(identifier);

            element.BorderColor = Color.Transparent;
        }

        private void RegisterButtonClickedLog(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegistrationPage());
            Navigation.RemovePage(this);
        }
    }
}
