using System;
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
            var connString = "Host=ats-kelompok24.cwk6nsvvdnxx.ap-southeast-3.rds.amazonaws.com;Username=postgres;Password=kelompok24;Database=ats";

            await using var conn = new NpgsqlConnection(connString);
            Console.WriteLine("connecting");
            await conn.OpenAsync();
            Console.WriteLine("connected");

            using (var cmd1 = new NpgsqlCommand("INSERT INTO job_opening (company_id, job_name, start_recruitment_date, end_recruitment_date, job_type, description) VALUES (1, @job_name, @start_recruitment_date, @end_recruitment_date, @job_type, @description)", conn))
            {
                cmd1.Parameters.AddWithValue("job_name", jobName);
                cmd1.Parameters.AddWithValue("start_recruitment_date", startDate);
                cmd1.Parameters.AddWithValue("end_recruitment_date", endDate);
                cmd1.Parameters.AddWithValue("job_type", jobType);
                cmd1.Parameters.AddWithValue("description", description);
                await cmd1.ExecuteNonQueryAsync();
            };
            Console.WriteLine("Successfully inserted Job Application");

        }
    }
}
