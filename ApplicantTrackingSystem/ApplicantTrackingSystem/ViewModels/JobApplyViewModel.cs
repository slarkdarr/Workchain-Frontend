using ApplicantTrackingSystem.Models;
using ApplicantTrackingSystem.Services;
using MvvmHelpers.Commands;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;
using System.Globalization;
using MonkeyCache.FileStore;
using Newtonsoft.Json;

namespace ApplicantTrackingSystem.ViewModels
{
    public class JobApplyViewModel : ViewModelBase
    {
        public Command SubmitCommand { get; }

        public CredentialModel credential = new CredentialModel();
        public ApplicantJobApplicationModel applicantJobApplication { get; set; }

        public JobApplyViewModel()
        {
            //How to access the monkey cache
            Console.WriteLine("CRED");
            var json = string.Empty;
            json = Barrel.Current.Get<string>("loginCredential");
            credential = JsonConvert.DeserializeObject<CredentialModel>(json);
            Console.WriteLine(credential.token);

            DateTime localDate = DateTime.Now;
            ApplyDate = localDate;

            //int.TryParse(PassedJobID, out var result);
            Console.WriteLine("Job ID hasil passing di view model job apply view model:");
            Console.WriteLine(JobId);

            SubmitCommand = new Command(Submit);

            GetDefaultApplicantData();
        }

        private int jobId;
        public int JobId
        {
            get => jobId;
            set => SetProperty(ref jobId, value);
        }

        private int applicantId;
        public int ApplicantId
        {
            get => applicantId;
            set => SetProperty(ref applicantId, value);
        }

        private DateTime applyDate;
        public DateTime ApplyDate 
        {
            get => applyDate;
            set => SetProperty(ref applyDate, value);
        }

        private string status;
        public string Status 
        {
            get => status;
            set => SetProperty(ref status, value);
        }

        private string requirementLink;
        public string RequirementLink
        {
            get => requirementLink;
            set => SetProperty(ref requirementLink, value);
        }

        private string applicantName;
        public string ApplicantName
        {
            get => applicantName;
            set => SetProperty(ref applicantName, value);
        }

        private string applicantEmail;
        public string ApplicantEmail
        {
            get => applicantEmail;
            set => SetProperty(ref applicantEmail, value);
        }

        private string applicantTelp;
        public string ApplicantTelp
        {
            get => applicantTelp;
            set => SetProperty(ref applicantTelp, value);
        }

        async void GetDefaultApplicantData()
        {
            Console.WriteLine("GetDefaultApplicantData");
            applicantJobApplication = await AtsService.GetApplicantApplicationData(credential.token, credential.user_id.ToString());
            if (applicantJobApplication != null)
            {
                Console.WriteLine("Default data :");
                Console.WriteLine(applicantJobApplication.full_name);
                Console.WriteLine(applicantJobApplication.email);
                Console.WriteLine(applicantJobApplication.phone_number);
                // Set the binding property
                ApplicantName = applicantJobApplication.full_name;
                ApplicantEmail = applicantJobApplication.email;
                ApplicantTelp = applicantJobApplication.phone_number;
            }
            else
            {
                Console.WriteLine("Default data : EMPTY");
            }
        }

        async void Submit()
        {
            Console.WriteLine("Submitting");
            Console.WriteLine(RequirementLink);

            Status = "Interview";
            var jobApplication = new JobApplicationAdd
            {
                job_id = jobId,
                apply_date = applyDate.ToString("dd/MM/yyyy"),
                requirement_link = requirementLink,
                status = status,
                applicant_name = applicantName,
                applicant_email = applicantEmail,
                applicant_telp = applicantTelp,
            };

            Console.WriteLine(jobApplication.job_id);
            Console.WriteLine(jobApplication.apply_date);
            Console.WriteLine(jobApplication.requirement_link);
            Console.WriteLine(jobApplication.status);
            Console.WriteLine(jobApplication.applicant_email);
            Console.WriteLine(jobApplication.applicant_name);
            Console.WriteLine(jobApplication.applicant_telp);


            var applyResp = await AtsService.AddJobApplication(jobApplication, credential.token);
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
