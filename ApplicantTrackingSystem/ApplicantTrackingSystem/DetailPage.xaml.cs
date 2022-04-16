using ApplicantTrackingSystem.Models;
using ApplicantTrackingSystem.Services;
using ApplicantTrackingSystem.ViewModels;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using ApplicantTrackingSystem.ViewModels;

namespace ApplicantTrackingSystem
{
    [QueryProperty(nameof(PassedJob), nameof(PassedJob))]
    public partial class DetailPage : ContentPage
    {
        public string PassedJob { get; set; }

        public DetailPageViewModel vm = new DetailPageViewModel();

        public CredentialModel credential = new CredentialModel();
        public JobVacancy JobVacancy { get; set; }
        public JobVacancy JobVacancyQueryResult { get; set; }
        public DetailPage()
        {
            BindingContext = new JobVacancyViewModel();
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = vm;

            Console.WriteLine("JOB HASIL PASSING");
            Console.WriteLine(PassedJob);
            vm.SetJobID(PassedJob);
        }

        private void ApplyButtonClicked(object sender, EventArgs e)
        {
            Console.WriteLine("APPLY BUTTON CLICKED!!!");
            int.TryParse(PassedJob, out var result);
            Console.WriteLine("RESULT :");
            Console.WriteLine(result);
            var route = $"{nameof(JobApplyPage)}?PassedJobID={result}";
            Shell.Current.GoToAsync(route);
        }
    }
}
