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

namespace ApplicantTrackingSystem.ViewModels
{
    [QueryProperty(nameof(JobId), nameof(JobId))]
    [QueryProperty(nameof(ApplicantId), nameof(ApplicantId))]
    internal class JobApplyViewModel : ViewModelBase
    {
        public Command SubmitCommand { get; }
        public JobApplyViewModel()
        {
            DateTime localDate = DateTime.Now;
            ApplyDate = localDate;

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

            await AtsService.AddJobApplication(jobApplication);
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
