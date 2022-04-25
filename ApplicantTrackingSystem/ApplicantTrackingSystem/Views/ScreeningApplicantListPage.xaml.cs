using ApplicantTrackingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApplicantTrackingSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScreeningApplicantListPage : ContentPage
    {
        public ScreeningApplicantListViewModel vm = new ScreeningApplicantListViewModel();
        string currentState;

        public ScreeningApplicantListPage()
        {
            this.BindingContext = vm;
            InitializeComponent();

            //var state = (Button)States.FindByName("All");
            //state.BackgroundColor = Color.FromHex("9955DE");
            currentState = "All";

            //vm.FetchAllCommand.Execute(null);
            //var stateTitle = (Label)JobList.FindByName("StateTitle");
            //stateTitle.Text = "All";
        }

        private void StateChosen(object sender, EventArgs e)
        {
            string[] stateNamesList = { "All", "InReview", "Interview", "Offered", "Declined" };
            var entry = (Button)sender;
            var classId = entry.ClassId;
            var identifier = classId;
            currentState = identifier;

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

        protected override async void OnAppearing()
        {
            if (currentState == "InReview")
            {
                var state = (Button)States.FindByName("InReview");
                state.BackgroundColor = Color.FromHex("9955DE");
                currentState = "InReview";

                vm.FetchInReviewCommand.Execute(null);
                var stateTitle = (Label)JobList.FindByName("StateTitle");
                stateTitle.Text = "In Review";
            }
            else if (currentState == "Interview")
            {
                var state = (Button)States.FindByName("Interview");
                state.BackgroundColor = Color.FromHex("9955DE");
                currentState = "Interview";

                vm.FetchInterviewCommand.Execute(null);
                var stateTitle = (Label)JobList.FindByName("StateTitle");
                stateTitle.Text = "Interview";
            }
            else if (currentState == "Offered")
            {
                var state = (Button)States.FindByName("Offered");
                state.BackgroundColor = Color.FromHex("9955DE");
                currentState = "Offered";

                vm.FetchOfferedCommand.Execute(null);
                var stateTitle = (Label)JobList.FindByName("StateTitle");
                stateTitle.Text = "Offered";
            }
            else if (currentState == "Declined")
            {
                var state = (Button)States.FindByName("Declined");
                state.BackgroundColor = Color.FromHex("9955DE");
                currentState = "Declined";

                vm.FetchDeclinedCommand.Execute(null);
                var stateTitle = (Label)JobList.FindByName("StateTitle");
                stateTitle.Text = "Declined";
            }
            else if (currentState == "All")
            {
                var state = (Button)States.FindByName("All");
                state.BackgroundColor = Color.FromHex("9955DE");
                currentState = "All";

                vm.FetchAllCommand.Execute(null);
                var stateTitle = (Label)JobList.FindByName("StateTitle");
                stateTitle.Text = "All";
            }
        }

        private void InReviewCommand(object sender, EventArgs e)
        {
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