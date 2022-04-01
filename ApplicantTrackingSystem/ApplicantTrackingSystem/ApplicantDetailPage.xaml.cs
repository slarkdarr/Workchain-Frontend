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
    public partial class ApplicantDetailPage : ContentPage
    {
        public ApplicantDetailPage()
        {
            InitializeComponent();
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