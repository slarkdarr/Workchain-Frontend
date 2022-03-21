using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ApplicantTrackingSystem
{
    public partial class JobApplyPage : ContentPage
    {
        public JobApplyPage()
        {
            InitializeComponent();
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

            //vm.SubmitCommand.Execute(null);
        }
    }
}
