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
    [QueryProperty(nameof(PassedApplicant), nameof(PassedApplicant))]
    public partial class ScheduleJobInterviewPage : ContentPage
    {
        public string PassedApplicant { get; set; }

        public ScheduleJobInterviewViewModel vm = new ScheduleJobInterviewViewModel();
        public ScheduleJobInterviewPage()
        {
            
            InitializeComponent();
            InterviewDate.Date = DateTime.Now;
            InterviewStartTime.Time = DateTime.Now.TimeOfDay;
            InterviewEndTime.Time = DateTime.Now.TimeOfDay;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = vm;
            Console.WriteLine("APPLICANT HASIL PASSING Schedule");
            Console.WriteLine(PassedApplicant);
            vm.ApplicantId = PassedApplicant;
        }

        private void DeclineButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ScreeningApplicantListPage());
            Navigation.RemovePage(this);
        }
    }
}