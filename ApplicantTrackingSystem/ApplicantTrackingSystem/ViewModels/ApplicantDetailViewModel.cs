using System;
using System.ComponentModel;
using System.Windows.Input;
using MonkeyCache.FileStore;
using Xamarin.Forms;
using Npgsql;
using System.Runtime.CompilerServices;
using ApplicantTrackingSystem.Services;
using ApplicantTrackingSystem.Models;
using Newtonsoft.Json;
using MvvmHelpers;
using Xamarin.Essentials;

namespace ApplicantTrackingSystem.ViewModels
{
    public class ApplicantDetailViewModel : INotifyPropertyChanged
    {
        public CredentialModel credential = new CredentialModel();
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableRangeCollection<JobApplication> JobApplicationQueryResult { get; set; }

        public ICommand ScheduleCommand { protected set; get; }
        public ICommand LoadCommand { protected set; get; }
        public ICommand AcceptCommand { protected set; get; }
        public ICommand DeclineCommand { protected set; get; }
        public ICommand OpenCVCommand { protected set; get; }
        public ICommand OpenMeetLinkCommand { protected set; get; }

        public ApplicantDetailViewModel()
        {
            //How to access the monkey cache
            Console.WriteLine("CRED");
            var json = string.Empty;
            json = Barrel.Current.Get<string>("loginCredential");
            credential = JsonConvert.DeserializeObject<CredentialModel>(json);
            Console.WriteLine(credential.token);

            Console.WriteLine("Constructor Applicant detail view model");

            Console.WriteLine("ApplicationId hasil passing di view model applicant detail view model:");
            Console.WriteLine(ApplicationId);

            JobApplicationQueryResult = new ObservableRangeCollection<JobApplication>();

            ScheduleCommand = new Command(OnSchedule);
            LoadCommand = new Command(OnLoad);
            AcceptCommand = new Command(OnAccept);
            DeclineCommand = new Command(OnDecline);
            OpenCVCommand = new Command(OpenCV);
            OpenMeetLinkCommand = new Command(OpenMeetLink);
        }

        private string applicationId;
        public string ApplicationId
        {
            get => applicationId;
            set => SetProperty(ref applicationId, value);
        }

        private string applicantName;
        private string jobName;
        private string applicantEmail;
        private string applicantTelp;
        private string applicationStatus;
        private string interviewDate = "Not set";
        private string interviewTime = "Not set";
        private string interviewLink = "Not set";
        private string applicantPicture;
        private bool enabledAccept;
        private bool enabledDecline;
        private string applicantRequirement;

        public string ApplicantName
        {
            get { return applicantName; }
            set
            {
                applicantName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ApplicantName"));
            }
        }

        public string JobName
        {
            get { return jobName; }
            set
            {
                jobName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("JobName"));
            }
        }

        public string ApplicantEmail
        {
            get { return applicantEmail; }
            set
            {
                applicantEmail = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ApplicantEmail"));
            }
        }

        public string ApplicantTelp
        {
            get { return applicantTelp; }
            set
            {
                applicantTelp = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ApplicantTelp"));
            }
        }

        public string ApplicationStatus
        {
            get { return applicationStatus; }
            set
            {
                applicationStatus = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ApplicationStatus"));
            }
        }

        public string InterviewDate
        {
            get { return interviewDate; }
            set
            {
                interviewDate = value;
                PropertyChanged(this, new PropertyChangedEventArgs("InterviewDate"));
            }
        }

        public string InterviewTime
        {
            get { return interviewTime; }
            set
            {
                interviewTime = value;
                PropertyChanged(this, new PropertyChangedEventArgs("InterviewTime"));
            }
        }

        public string InterviewLink
        {
            get { return interviewLink; }
            set
            {
                interviewLink = value;
                PropertyChanged(this, new PropertyChangedEventArgs("InterviewLink"));
            }
        }

        public string ApplicantPicture
        {
            get { return applicantPicture; }
            set
            {
                applicantPicture = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ApplicantPicture"));
            }
        }

        public bool EnabledAccept
        {
            get { return enabledAccept; }
            set
            {
                enabledAccept = value;
                PropertyChanged(this, new PropertyChangedEventArgs("EnabledAccept"));
            }
        }

        public bool EnabledDecline
        {
            get { return enabledDecline; }
            set
            {
                enabledDecline = value;
                PropertyChanged(this, new PropertyChangedEventArgs("EnabledDecline"));
            }
        }

        public string ApplicantRequirement
        {
            get { return applicantRequirement; }
            set
            {
                applicantRequirement = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ApplicantRequirement"));
            }
        }

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand SubmitCommand { protected set; get; }

        async public void OnSchedule()
        {
            Console.WriteLine("On Schedule");
            Console.WriteLine(ApplicationId);
            // Navigate to ScheduleJobInterviewPage
            var route = $"{nameof(ScheduleJobInterviewPage)}?PassedApplication={ApplicationId}";
            await Shell.Current.GoToAsync(route);
        }

        async void OnAccept()
        {
            if (interviewDate == "Not set")
            {
                InterviewDate = null;
                InterviewTime = null;
                InterviewLink = null;
            }
            var jobApplication = new UpdateJobApplication
            {
                application_id = Int32.Parse(ApplicationId),
                status = "Offered",
                interview_date = InterviewDate,
                interview_time = InterviewTime,
                interview_link = InterviewLink
            };


            var applyResp = await AtsService.PutUpdateJobApplication(jobApplication, credential.token);
            if (applyResp != null)
            {
                Console.WriteLine("Hasil response di view model");
                Console.WriteLine(applyResp);
                await Application.Current.MainPage.DisplayAlert("Berhasil", "Update status telah berhasil dilakukan", "OK");

                // Navigasi back to menu sebelumnya
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Gagal", "Update status gagal dilakukan", "OK");
            }
        }

        async void OnDecline()
        {
            if (interviewDate == "Not set")
            {
                InterviewDate = null;
                InterviewTime = null;
                InterviewLink = null;
            }
            var jobApplication = new UpdateJobApplication
            {
                application_id = Int32.Parse(ApplicationId),
                status = "Declined",
                interview_date = InterviewDate,
                interview_time = InterviewTime,
                interview_link = InterviewLink
            };


            var applyResp = await AtsService.PutUpdateJobApplication(jobApplication, credential.token);
            if (applyResp != null)
            {
                Console.WriteLine("Hasil response di view model");
                Console.WriteLine(applyResp);
                await Application.Current.MainPage.DisplayAlert("Berhasil", "Update status telah berhasil dilakukan", "OK");

                // Navigasi back to menu sebelumnya
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Gagal", "Update status gagal dilakukan", "OK");
            }
        }

        async void OnLoad()
        {
            var application = await AtsService.GetJobApplicationById(credential.token, ApplicationId);
            if (application != null)
            {
                Console.WriteLine("Valid job opening");
                ApplicantName = application[0].applicant_name;
                JobName = application[0].job_name;
                ApplicantEmail = application[0].applicant_email;
                ApplicationStatus = application[0].status;
                ApplicantTelp = application[0].applicant_telp;
                ApplicantPicture = application[0].applicant_picture;
                ApplicantRequirement = application[0].requirement_link;
                //ApplicantRequirement = "https://stei19.kuliah.itb.ac.id/login/index.php";

                if (application[0].interview_date != null)
                {
                    InterviewDate = application[0].interview_date;
                    InterviewTime = application[0].interview_time;
                    InterviewLink = application[0].interview_link;
                }

                IsEnabledAccept();
                IsEnabledDecline();

                Console.WriteLine("REQ LINK: " + ApplicantRequirement);

            }
            else
            {
                Console.WriteLine("EMPTYY");
            }

        }

        void IsEnabledAccept()
        {
            if (ApplicationStatus == "Offered")
            {
                EnabledAccept = false;
            } else
            {
                EnabledAccept = true;
            }
        }

        void IsEnabledDecline()
        {
            if (ApplicationStatus == "Declined")
            {
                EnabledDecline = false;
            }
            else
            {
                EnabledDecline = true;
            }
        }

        async void OpenCV()
        {
            await Browser.OpenAsync(ApplicantRequirement, BrowserLaunchMode.SystemPreferred);
        }

        async void OpenMeetLink()
        {
            if (InterviewLink != "Not set")
            {
                await Browser.OpenAsync(InterviewLink, BrowserLaunchMode.External);
            }
        }


    }
}
