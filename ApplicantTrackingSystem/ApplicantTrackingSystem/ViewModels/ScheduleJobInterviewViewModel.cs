using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Npgsql;
using System.Runtime.CompilerServices;

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

        public ScheduleJobInterviewViewModel()
        {
            Console.WriteLine("Constructor Schedule Job view model");

            Console.WriteLine("Applicant ID hasil passing di view model Schedule Job view model:");
            Console.WriteLine(ApplicantId);

            SubmitCommand = new Command(OnAccept);
        }

        private string applicantId;
        public string ApplicantId
        {
            get => applicantId;
            set => SetProperty(ref applicantId, value);
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

        public DateTime InterviewStartTime
        {
            get { return startTime; }
            set { SetProperty(ref startTime, value); }
        }
        
        public DateTime InterviewEndTime
        {
            get { return endTime; }
            set { SetProperty(ref endTime, value); }
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
            Console.WriteLine(ApplicantId);
            await Application.Current.MainPage.DisplayAlert("Applicant ID", ApplicantId, "OK");
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
