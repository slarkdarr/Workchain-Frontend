using ApplicantTrackingSystem.Models;
using ApplicantTrackingSystem.Services;
using MonkeyCache.FileStore;
using MvvmHelpers;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.ComponentModel;


namespace ApplicantTrackingSystem.ViewModels
{
    public class ApplicationProgressViewModel : ViewModelBase
    {
        public CredentialModel credential = new CredentialModel();
        public ObservableRangeCollection<JobApplication> JobApplications { get; set; }
        public ObservableRangeCollection<JobApplication> JobApplicationQueryResults { get; set; }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private string stateTitle;
        public ICommand FetchAllCommand { protected set; get; }
        public ICommand FetchInReviewCommand { protected set; get; }
        public ICommand FetchInterviewCommand { protected set; get; }
        public ICommand FetchOfferedCommand { protected set; get; }
        public ICommand FetchDeclinedCommand { protected set; get; }

        public MvvmHelpers.Commands.AsyncCommand<object> SelectedCommand { get; }

        public string StateTitle
        {
            get { return stateTitle; }
            set
            {
                stateTitle = value;
                PropertyChanged(this, new PropertyChangedEventArgs("StateTitle"));
            }
        }

        public ApplicationProgressViewModel()
        {
            //How to access the monkey cache
            Console.WriteLine("CRED AT APPLICATION PROGRESS");
            var json = string.Empty;
            json = Barrel.Current.Get<string>("loginCredential");
            credential = JsonConvert.DeserializeObject<CredentialModel>(json);
            Console.WriteLine(credential.token);

            JobApplications = new ObservableRangeCollection<JobApplication>();
            JobApplicationQueryResults = new ObservableRangeCollection<JobApplication>();

            FetchInReviewCommand = new Command(FetchInReview);
            FetchInterviewCommand = new Command(FetchInterview);
            FetchOfferedCommand = new Command(FetchOffered);
            FetchDeclinedCommand = new Command(FetchDeclined);
            FetchAllCommand = new Command(FetchAll);

            SelectedCommand = new MvvmHelpers.Commands.AsyncCommand<object>(Selected);

            FetchAll();

        }

        JobApplication selectedApplicant;
        public JobApplication SelectedApplicant
        {
            get => selectedApplicant;
            set => SetProperty(ref selectedApplicant, value);
        }

        async Task Selected(object args)
        {
            var applicant = args as JobApplication;
            if (applicant == null)
            {
                return;
            }
            Console.WriteLine("SelectedApplicant SELECTED!!!!");
            SelectedApplicant = null;

            //await Application.Current.MainPage.DisplayAlert("Selected", applicant.applicant_name, "OK");

            // Navigate to Job Detail Page
            var route = $"{nameof(ApplicationDetailPage)}?PassedApplication={applicant.application_id}";
            await Shell.Current.GoToAsync(route);

        }

        async void FetchAll()
        {
            JobApplications.Clear();
            var JobApplicationQueryResults = await AtsService.GetJobApplication(credential.token);
            if (JobApplicationQueryResults != null)
            {
                Console.WriteLine(JobApplicationQueryResults);
                foreach (JobApplication job in JobApplicationQueryResults)
                {
                   
                    JobApplications.Add(job);

                }
            }
            else
            {
                Console.WriteLine("EMPTYY");
            }
        }

        async void FetchAll(string state)
        {
            JobApplications.Clear();
            var JobApplicationQueryResults = await AtsService.GetJobApplication(credential.token);
            if (JobApplicationQueryResults != null)
            {
                Console.WriteLine(JobApplicationQueryResults);
                foreach (JobApplication job in JobApplicationQueryResults)
                {
                    //Console.WriteLine(job.status)
                    Console.WriteLine(state);
                    if (job.status == state)
                    {
                        JobApplications.Add(job);
                    }
                    
                }
            }
            else
            {
                Console.WriteLine("EMPTYY");
            }
        }

        void FetchInReview() { FetchAll("In Review"); }
        void FetchInterview() { FetchAll("Interview"); }
        void FetchOffered() { FetchAll("Offered"); }
        void FetchDeclined() { FetchAll("Declined"); }


    }


}
