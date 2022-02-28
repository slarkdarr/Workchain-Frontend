﻿using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Npgsql;
using System.Runtime.CompilerServices;

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
            set { SetProperty(ref startDate, value); }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set { SetProperty(ref endDate, value); }
        }

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
            SubmitCommand = new Command(OnSubmit);
        }

        async public void OnSubmit()
        {
            var connString = "Host=ec2-3-219-204-29.compute-1.amazonaws.com;Database=d7p6gej9knqefg;Username=ptyxepvslwevdw;Password=2cff69469572cf04b3e738727d1503ccd0e05efc9b1d73f9ac6061954f094771";

            await using var conn = new NpgsqlConnection(connString);
            Console.WriteLine("connecting");
            await conn.OpenAsync();
            Console.WriteLine("connected");

            using (var cmd1 = new NpgsqlCommand("INSERT INTO job_opening (company_id, job_name, start_recruitment_date, end_recruitment_date, job_type, description, salary) VALUES (1, @job_name, @start_recruitment_date, @end_recruitment_date, @job_type, @description, @salary)", conn))
            {
                cmd1.Parameters.AddWithValue("job_name", jobName);
                cmd1.Parameters.AddWithValue("start_recruitment_date", startDate);
                cmd1.Parameters.AddWithValue("end_recruitment_date", endDate);
                cmd1.Parameters.AddWithValue("job_type", jobType);
                cmd1.Parameters.AddWithValue("description", description);
                cmd1.Parameters.AddWithValue("salary", int.Parse(salary));
                await cmd1.ExecuteNonQueryAsync();
            };
            Console.WriteLine("Successfully inserted Job Application");

        }
    }
}