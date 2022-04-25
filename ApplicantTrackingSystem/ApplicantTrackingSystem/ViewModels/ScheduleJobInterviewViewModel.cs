using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Npgsql;
using System.Runtime.CompilerServices;
using ApplicantTrackingSystem.Models;
using ApplicantTrackingSystem.Services;
using MonkeyCache.FileStore;
using Newtonsoft.Json;

namespace ApplicantTrackingSystem.ViewModels
{
    public class ScheduleJobInterviewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private DateTime date;
        private DateTime startTime;
        private DateTime endTime;
        private string meetingLink;
        private string applicantName;
        private string applicantPicture;
        private string jobName;
        public ICommand SubmitCommand { protected set; get; }
        public ICommand SaveCommand { protected set; get; }
        public ICommand LoadCommand { protected set; get; }

        public CredentialModel credential = new CredentialModel();

        public ScheduleJobInterviewViewModel()
        {
            //How to access the monkey cache
            Console.WriteLine("CRED");
            var json = string.Empty;
            json = Barrel.Current.Get<string>("loginCredential");
            credential = JsonConvert.DeserializeObject<CredentialModel>(json);
            Console.WriteLine(credential.token);

            Console.WriteLine("Constructor Schedule Job view model");

            Console.WriteLine("ApplicationId ID hasil passing di view model Schedule Job view model:");
            Console.WriteLine(ApplicationId);

            SubmitCommand = new Command(OnAccept);
            SaveCommand = new Command(Save);
            LoadCommand = new Command(OnLoad);

        }

        private string applicationId;
        public string ApplicationId
        {
            get => applicationId;
            set => SetProperty(ref applicationId, value);
        }


        public DateTime InterviewDate
        {
            get { return date; }
            set 
            {
                date = value;
                PropertyChanged(this, new PropertyChangedEventArgs("InterviewDate"));
            }
        }


        public string ApplicantName
        {
            get { return applicantName; }
            set
            {
                applicantName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ApplicantName"));
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

        public string JobName
        {
            get { return jobName; }
            set
            {
                jobName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("JobName"));
            }
        }

        public DateTime InterviewStartTime
        {
            get { return startTime; }
            set
            {
                startTime = value;
                PropertyChanged(this, new PropertyChangedEventArgs("InterviewStartTime"));
            }
        }

        public DateTime InterviewEndTime
        {
            get { return endTime; }
            set
            {
                endTime = value;
                PropertyChanged(this, new PropertyChangedEventArgs("InterviewEndTime"));
            }
        }

        public string MeetingLink
        {
            get { return meetingLink; }
            set
            {
                meetingLink = value;
                PropertyChanged(this, new PropertyChangedEventArgs("JobType"));
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

        async public void OnAccept()
        {
            Console.WriteLine("On Schedule Interview");
            Console.WriteLine(ApplicationId);
            await Application.Current.MainPage.DisplayAlert("Applicant ID", ApplicationId, "OK");
        }

        async void Save()
        {
            // Remove till here once the UI is connected to the view model

            var jobApplication = new UpdateJobApplication
            {
                application_id = Int32.Parse(ApplicationId),
                status = "Interview",
                interview_date = InterviewDate.ToString("dd/MM/yyyy"),
                interview_time = InterviewStartTime.ToString("HH:mm"),
                interview_link = MeetingLink
            };


            var applyResp = await AtsService.PutUpdateJobApplication(jobApplication, credential.token);
            if (applyResp != null)
            {
                Console.WriteLine("Hasil response di view model");
                Console.WriteLine(applyResp);
                await Application.Current.MainPage.DisplayAlert("Berhasil", "Submisi telah berhasil dilakukan", "OK");

                // Navigasi back to menu sebelumnya
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Gagal", "Submisi gagal dilakukan", "OK");
            }

        }

        async void OnLoad()
        {
            var application = await AtsService.GetJobApplicationById(credential.token, ApplicationId);
            if (application != null)
            {
                Console.WriteLine("Valid job opening");
                ApplicantName = application[0].applicant_name;
                ApplicantPicture = application[0].applicant_picture;
                JobName = application[0].job_name;
               
            }
            else
            {
                Console.WriteLine("EMPTYY");
            }

        }

    }
}
