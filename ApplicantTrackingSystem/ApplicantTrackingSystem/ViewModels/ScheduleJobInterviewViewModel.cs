﻿using System;
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
        public ICommand SubmitCommand { protected set; get; }
        public ICommand SaveCommand { protected set; get; }

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

        //public DateTime InterviewStartTime
        //{
        //    get { return startTime; }
        //    set { SetProperty(ref startTime, value); }
        //}
        
        //public DateTime InterviewEndTime
        //{
        //    get { return endTime; }
        //    set { SetProperty(ref endTime, value); }
        //}

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


        //async public void OnSubmit()
        //{
        //    var connString = "Host=ec2-3-219-204-29.compute-1.amazonaws.com;Database=d7p6gej9knqefg;Username=ptyxepvslwevdw;Password=2cff69469572cf04b3e738727d1503ccd0e05efc9b1d73f9ac6061954f094771";

        //    await using var conn = new NpgsqlConnection(connString);
        //    Console.WriteLine("connecting");
        //    await conn.OpenAsync();
        //    Console.WriteLine("connected");

        //    using (var cmd1 = new NpgsqlCommand("INSERT INTO job_opening (company_id, job_name, start_recruitment_date, end_recruitment_date, job_type, description, salary) VALUES (1, @job_name, @start_recruitment_date, @end_recruitment_date, @job_type, @description, @salary)", conn))
        //    {
        //        cmd1.Parameters.AddWithValue("start_recruitment_date", date);
        //        cmd1.Parameters.AddWithValue("end_recruitment_date", time);
        //        cmd1.Parameters.AddWithValue("job_type", jobType);
        //        cmd1.Parameters.AddWithValue("description", description);
        //        cmd1.Parameters.AddWithValue("salary", int.Parse(salary));
        //        await cmd1.ExecuteNonQueryAsync();
        //    };
        //    Console.WriteLine("Successfully inserted Job Application");

        //}
    }
}
