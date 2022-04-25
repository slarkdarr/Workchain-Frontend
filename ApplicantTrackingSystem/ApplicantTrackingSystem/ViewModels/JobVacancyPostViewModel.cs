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
    internal class JobVacancyPostViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string jobName;
        private DateTime startDate;
        private DateTime endDate;
        private string jobType;
        private string salary;
        private string description;

        public CredentialModel credential = new CredentialModel();

        public string JobName
        {
            get { return jobName; }
            set
            {
                jobName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("JobName"));
            }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                PropertyChanged(this, new PropertyChangedEventArgs("StartDate"));
            }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                PropertyChanged(this, new PropertyChangedEventArgs("EndDate"));
            }
        }


        //public DateTime StartDate
        //{
        //    get { return startDate; }
        //    set { SetProperty(ref startDate, value); }
        //}

        //public DateTime EndDate
        //{
        //    get { return endDate; }
        //    set { SetProperty(ref endDate, value); }
        //}

        public string JobType
        {
            get { return jobType; }
            set
            {
                jobType = value;
                PropertyChanged(this, new PropertyChangedEventArgs("JobType"));
            }
        }

        public string Salary
        {
            get { return salary; }
            set
            {
                salary = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Salary"));
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Description"));
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

        public JobVacancyPostViewModel()
        {
            //How to access the monkey cache
            Console.WriteLine("CRED");
            var json = string.Empty;
            json = Barrel.Current.Get<string>("loginCredential");
            credential = JsonConvert.DeserializeObject<CredentialModel>(json);
            Console.WriteLine(credential.token);

            SubmitCommand = new Command(OnSubmit);
        }

        async void OnSubmit()
        {
            var jobVacancy = new AddJobVacancy
            {
                job_name = JobName,
                start_recruitment_date = StartDate.ToString("dd/MM/yyyy"),
                end_recruitment_date = EndDate.ToString("dd/MM/yyyy"),
                job_type = JobType,
                salary = Int32.Parse(Salary),
                description = Description
            };


            var applyResp = await AtsService.AddJobOpening(jobVacancy, credential.token);
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
    }
}
