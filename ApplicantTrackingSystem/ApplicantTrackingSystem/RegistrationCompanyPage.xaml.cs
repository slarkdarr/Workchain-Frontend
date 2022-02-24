using System;
using System.Collections.Generic;
using ApplicantTrackingSystem.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApplicantTrackingSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationCompanyPage : ContentPage
    {
        public RegistrationCompanyPage()
        {
            var vm = new RegistrationCompanyViewModel();
            this.BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Email has been taken", "OK");
            InitializeComponent();

            Name.Completed += (object sender, EventArgs e) =>
            {
                Phone.Focus();
            };

            Phone.Completed += (object sender, EventArgs e) =>
            {
                Email.Focus();
            };

            Email.Completed += (object sender, EventArgs e) =>
            {
                Password.Focus();
            };

            Password.Completed += (object sender, EventArgs e) =>
            {
                vm.SubmitCommand.Execute(null);
            };
        }

        private void FrameFocusedReg(object sender, FocusEventArgs e)
        {
            var entry = (Entry)sender;
            var classId = entry.ClassId;
            var identifier = classId + "Frame";

            var element = (Frame)loginCard.FindByName(identifier);

            element.BorderColor = Color.FromHex("#58327F");
        }

        private void FrameUnfocusedReg(object sender, FocusEventArgs e)
        {
            var entry = (Entry)sender;
            var classId = entry.ClassId;
            var identifier = classId + "Frame";

            var element = (Frame)loginCard.FindByName(identifier);

            element.BorderColor = Color.Transparent;
        }

        private void LoginButtonClickedReg(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginCompanyPage());
            Navigation.RemovePage(this);
        }
    }
}
