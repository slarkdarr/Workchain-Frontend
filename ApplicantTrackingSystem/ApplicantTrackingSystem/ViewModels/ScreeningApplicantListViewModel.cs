using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Npgsql;
using System.Runtime.CompilerServices;

namespace ApplicantTrackingSystem.ViewModels
{
    internal class ScreeningApplicantListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //private int jobId;
        //private int applicantId;
        //private int companyId;
        private string status;
        private string applicantName;
        private string applicantPicture;
        private string jobName;

        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Status"));
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

        async public void OnSubmit()
        {
            /*
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
            */
            Console.WriteLine("Successfully inserted Job Application");

        }
    }
}
