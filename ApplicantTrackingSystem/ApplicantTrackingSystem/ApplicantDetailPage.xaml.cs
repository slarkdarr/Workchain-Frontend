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
    [QueryProperty(nameof(PassedApplication), nameof(PassedApplication))]
    public partial class ApplicantDetailPage : ContentPage
    {
        public string PassedApplication { get; set; }

        public ApplicantDetailViewModel vm = new ApplicantDetailViewModel();
        public ApplicantDetailPage()
        {
            
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = vm;
            Console.WriteLine("APPLICANT HASIL PASSING Detail page");
            Console.WriteLine(PassedApplication);
            vm.ApplicationId = PassedApplication;
        }

        private void ScheduleInterviewButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ScheduleJobInterviewPage());
            //Navigation.RemovePage(this);
        }

        private void DeclineButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ScreeningApplicantListPage());
            Navigation.RemovePage(this);
        }
    }
}