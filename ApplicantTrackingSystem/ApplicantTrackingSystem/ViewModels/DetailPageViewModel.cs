using ApplicantTrackingSystem.Models;
using ApplicantTrackingSystem.Services;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ApplicantTrackingSystem.ViewModels
{
    public class DetailPageViewModel : ViewModelBase
    {
        public CredentialModel credential = new CredentialModel();
        public JobVacancy JobVacancy { get; set; }
        public JobVacancy JobVacancyQueryResult { get; set; }


        public DetailPageViewModel()
        {
            //How to access the monkey cache
            Console.WriteLine("CRED");
            var json = string.Empty;
            json = Barrel.Current.Get<string>("loginCredential");
            credential = JsonConvert.DeserializeObject<CredentialModel>(json);
            Console.WriteLine(credential.token);
        }

        private string jobId;
        public string JobId
        {
            get => jobId;
            set => SetProperty(ref jobId, value);
        }

        private string jobName;
        public string JobName
        {
            get => jobName;
            set => SetProperty(ref jobName, value);
        }

        private string companyName;
        public string CompanyName
        {
            get => companyName;
            set => SetProperty(ref companyName, value);
        }


        private string salary;
        public string Salary
        {
            get => salary;
            set => SetProperty(ref salary, value);
        }

        private string jobDescription;
        public string JobDescription
        {
            get => jobDescription;
            set => SetProperty(ref jobDescription, value);
        }

        public void SetJobID(string jobIDPassing)
        {
            JobId = jobIDPassing.ToString();
            fetchJobDetail();
        }

        async void fetchJobDetail()
        {
            var JobVacancyQueryResult = await AtsService.getJobOpeningDetail(credential.token, JobId);
            if (JobVacancyQueryResult != null)
            {
                Console.WriteLine("Ada detail Job Opening :");
                JobVacancy = JobVacancyQueryResult;
                Console.WriteLine(JobVacancy.Job_Name);
                Console.WriteLine(JobVacancy.Job_ID);
                Console.WriteLine(JobVacancy.Company_Name);
                Console.WriteLine(JobVacancy.Salary);
                Console.WriteLine(JobVacancy.Description);
                // Set the binding property
                JobName = JobVacancy.Job_Name;
                CompanyName = JobVacancy.Company_Name;
                Salary = "Rp " + JobVacancy.Salary.ToString();
                JobDescription = JobVacancy.Description;
            }
            else
            {
                Console.WriteLine("EMPTYY");
            }
        }
    }
}
