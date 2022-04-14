using ApplicantTrackingSystem.Models;
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
        //public JobVacancyViewModel vm = new JobVacancyViewModel();

        public DetailPage()
        {
            BindingContext = new JobVacancyViewModel();
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Console.WriteLine("JOB HASIL PASSING");
            Console.WriteLine(PassedJob);

            //Console.WriteLine(PassedJob.Description);
            //int.TryParse(PassedJob, out var result);

            var vm = BindingContext as JobVacancyViewModel;
            vm.CurrJobId = PassedJob;
            vm.FetchByIdCommand.Execute(null);
            
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
