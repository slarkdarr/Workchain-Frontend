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
            Console.WriteLine("Job ID hasil passing di view model job apply:");
            Console.WriteLine(JobId);

            SubmitCommand = new Command(Submit);
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

        private string status;
        public string Status 
        { 
            get => status; 
            set => SetProperty(ref status, value); 
        }

        async void Submit()
        {
            Console.WriteLine("Job ID pas submit di view model job apply:");
            Console.WriteLine(JobId);
            
            Status = "Interview";

            // Hard code, will be removed
            RequirementLink = "Ini require";
            ApplicantName = "jorss";
            ApplicantEmail = "jorss@gmail.com";
            ApplicantTelp = "987";
            // Remove till here once the UI is connected to the view model

            var jobApplication = new JobApplication
            {
                job_id = jobId,
                apply_date = applyDate.ToString("dd/MM/yyyy"),
                requirement_link = requirementLink,
                status = status,
                applicant_name = applicantName,
                applicant_email = applicantEmail,
                applicant_telp = applicantTelp,
            };


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

        //async void Submit()
        //{
        //    var connString = "Host=ec2-3-219-204-29.compute-1.amazonaws.com;Database=d7p6gej9knqefg;Username=ptyxepvslwevdw;Password=2cff69469572cf04b3e738727d1503ccd0e05efc9b1d73f9ac6061954f094771";

        //    await using var conn = new NpgsqlConnection(connString);

        //    Console.WriteLine("Opening");
        //    await conn.OpenAsync();

        //    Console.WriteLine("Opened");


        //    string query = "INSERT INTO job_application (job_id, applicant_id, apply_date, requirement_link, status, applicant_name, applicat_email, applicant_telp) VALUES (@job_id, @applicant_id, @apply_date, @requirement_link, @status, @applicant_name, @applicat_email, @applicant_telp)";
        //    try
        //    {
        //        using (var cmd1 = new NpgsqlCommand(query, conn))
        //        {
        //            cmd1.Parameters.AddWithValue("job_id", JobId);
        //            cmd1.Parameters.AddWithValue("applicant_id", ApplicantId);
        //            cmd1.Parameters.AddWithValue("apply_date", ApplyDate);
        //            cmd1.Parameters.AddWithValue("requirement_link", RequirementLink);
        //            cmd1.Parameters.AddWithValue("status", Status);
        //            cmd1.Parameters.AddWithValue("applicant_name", ApplicantName);
        //            cmd1.Parameters.AddWithValue("applicat_email", ApplicantEmail);
        //            cmd1.Parameters.AddWithValue("applicant_telp", ApplicantTelp);
        //            await cmd1.ExecuteNonQueryAsync();
        //        };
        //        Console.WriteLine("Successfully inserted new job application");
        //    }

        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        conn.Close();
        //    }

        //}
    }
}
