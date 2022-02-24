using System;
using System.Collections.Generic;
using ApplicantTrackingSystem.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApplicantTrackingSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobVacancyPost : ContentPage
    {

        public JobVacancyPost()
        {
            var vm = new JobVacancyPostViewModel();
            this.BindingContext = vm;
            InitializeComponent();
            StartDate.Date = DateTime.Now;

            JobName.Completed += (object sender, EventArgs e) =>
            {
                StartDate.Focus();
            };

            JobType.Completed += (object sender, EventArgs e) =>
            {
                Salary.Focus();
            };
            Salary.Completed += (object sender, EventArgs e) =>
            {
                Description.Focus();
            };
            //Description.Completed += (object sender, EventArgs e) =>
            //{
            //    vm.SubmitCommand.Execute(null);
            //};

            SubmitButton.Clicked += (object sender, EventArgs e) =>
            {
                vm.SubmitCommand.Execute(null);
            };
        }

        private void FrameFocusedReg(object sender, FocusEventArgs e)
        {
            var entry = (Entry)sender;
            var classId = entry.ClassId;
            var identifier = classId + "Frame";

            var element = (Frame)jobCard.FindByName(identifier);

            element.BorderColor = Color.FromHex("#58327F");
        }

        private void FrameUnfocusedReg(object sender, FocusEventArgs e)
        {
            var entry = (Entry)sender;
            var classId = entry.ClassId;
            var identifier = classId + "Frame";

            var element = (Frame)jobCard.FindByName(identifier);

            element.BorderColor = Color.Transparent;
        }

        private void FrameFocusedEditor(object sender, FocusEventArgs e)
        {
            var entry = (Editor)sender;
            var classId = entry.ClassId;
            var identifier = classId + "Frame";

            var element = (Frame)jobCard.FindByName(identifier);

            element.BorderColor = Color.FromHex("#58327F");
        }

        private void FrameUnfocusedEditor(object sender, FocusEventArgs e)
        {
            var entry = (Editor)sender;
            var classId = entry.ClassId;
            var identifier = classId + "Frame";

            var element = (Frame)jobCard.FindByName(identifier);

            element.BorderColor = Color.Transparent;
        }

        private void Submit(object sender, EventArgs e)
        {
            var vm = new JobVacancyPostViewModel();
            this.BindingContext = vm;
            StartDate.Date = DateTime.Now;

            //vm.SubmitCommand.Execute(null);
        }
    }
}