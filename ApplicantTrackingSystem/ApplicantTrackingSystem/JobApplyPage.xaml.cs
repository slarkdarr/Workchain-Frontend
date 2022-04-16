using ApplicantTrackingSystem.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ApplicantTrackingSystem
{
    [QueryProperty(nameof(PassedJobID), nameof(PassedJobID))]
    public partial class JobApplyPage : ContentPage
    {
        public string PassedJobID { get; set; }

        public JobApplyViewModel vm = new JobApplyViewModel();
        public JobApplyPage()
        {
            
            InitializeComponent();

            ApplicantName.Completed += (object sender, EventArgs e) =>
            {
                ApplicantEmail.Focus();
            };

            ApplicantEmail.Completed += (object sender, EventArgs e) =>
            {
                ApplicantTelp.Focus();
            };

            ApplicantTelp.Completed += (object sender, EventArgs e) =>
            {
                RequirementLink.Focus();
            };

            RequirementLink.Completed += (object sender, EventArgs e) =>
            {
                //vm.SubmitCommand.Execute(null);
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = vm;
            Console.WriteLine("PassedJobID HASIL PASSING di view");
            Console.WriteLine(PassedJobID);
            int.TryParse(PassedJobID, out var result);
            vm.JobId = result;
        }

        private void FrameFocusedReg(object sender, FocusEventArgs e)
        {
            var entry = (Entry)sender;
            var classId = entry.ClassId;
            var identifier = classId + "Frame";

            var element = (Frame)applyCard.FindByName(identifier);

            element.BorderColor = Color.FromHex("#58327F");
        }

        private void FrameUnfocusedReg(object sender, FocusEventArgs e)
        {
            var entry = (Entry)sender;
            var classId = entry.ClassId;
            var identifier = classId + "Frame";

            var element = (Frame)applyCard.FindByName(identifier);

            element.BorderColor = Color.Transparent;
        }

        private void FrameFocusedEditor(object sender, FocusEventArgs e)
        {
            var entry = (Editor)sender;
            var classId = entry.ClassId;
            var identifier = classId + "Frame";

            var element = (Frame)applyCard.FindByName(identifier);

            element.BorderColor = Color.FromHex("#58327F");
        }

        private void FrameUnfocusedEditor(object sender, FocusEventArgs e)
        {
            var entry = (Editor)sender;
            var classId = entry.ClassId;
            var identifier = classId + "Frame";

            var element = (Frame)applyCard.FindByName(identifier);

            element.BorderColor = Color.Transparent;
        }

        private void Submit(object sender, EventArgs e)
        {
            //var vm = new JobVacancyPostViewModel();
            //this.BindingContext = vm;
            //StartDate.Date = DateTime.Now;

            vm.SubmitCommand.Execute(null);
        }
    }
}
