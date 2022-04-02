using System;
using System.Collections.Generic;
using ApplicantTrackingSystem.ViewModels;


using Xamarin.Forms;

namespace ApplicantTrackingSystem
{
    public partial class ApplicationProgressPage : ContentPage
    {
        public ApplicationProgressViewModel vm = new ApplicationProgressViewModel();

        public ApplicationProgressPage()
        {
            this.BindingContext = vm;
            InitializeComponent();
        }

        private void StateChosen(object sender, EventArgs e)
        {
            string[] stateNamesList = { "Applied", "InReview", "Interview", "Offered", "Declined" };
            var entry = (Button)sender;
            var classId = entry.ClassId;
            var identifier = classId;

            var element = (Button)States.FindByName(identifier);
            var stateTitle = (Label)JobList.FindByName("StateTitle");
            bool lastState = element.BackgroundColor == Color.FromHex("9955DE");
            //Console.WriteLine(identifier);

            foreach (string stateName in stateNamesList)
            {
                var state = (Button)States.FindByName(stateName);
                state.BackgroundColor = Color.FromHex("D9D9D9");
            }

            if (!lastState)
            {
                element.BackgroundColor = Color.FromHex("9955DE");
                stateTitle.Text = element.Text;
            }

        }

        private void InReviewCommand(object sender, EventArgs e){
            vm.FetchInReviewCommand.Execute(null);
        }

        private void InterviewCommand(object sender, EventArgs e)
        {
            vm.FetchInterviewCommand.Execute(null);
        }

        private void OfferedCommand(object sender, EventArgs e)
        {
            vm.FetchOfferedCommand.Execute(null);
        }

        private void DeclinedCommand(object sender, EventArgs e)
        {
            vm.FetchDeclinedCommand.Execute(null);
        }
    }
}
