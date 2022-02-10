using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ApplicantTrackingSystem
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
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
