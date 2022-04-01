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
    public partial class ScheduleJobInterviewPage : ContentPage
    {
        public ScheduleJobInterviewPage()
        {
            var vm = new ScheduleJobInterviewViewModel();
            this.BindingContext = vm;
            InitializeComponent();
            InterviewDate.Date = DateTime.Now;
            InterviewStartTime.Time = DateTime.Now.TimeOfDay;
            InterviewEndTime.Time = DateTime.Now.TimeOfDay;
        }

        private void DeclineButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ScreeningApplicantListPage());
            Navigation.RemovePage(this);
        }
    }
}