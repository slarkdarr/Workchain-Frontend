using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ApplicantTrackingSystem
{
    public partial class ApplicationProgressPage : ContentPage
    {
        public ApplicationProgressPage()
        {
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
            Console.WriteLine(identifier);

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

            vm.FetchAll();


        }
    }
}
