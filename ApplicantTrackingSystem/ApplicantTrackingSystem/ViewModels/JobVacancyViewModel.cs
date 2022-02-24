using ApplicantTrackingSystem.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicantTrackingSystem.ViewModels
{
    public class JobVacancyViewModel : ViewModelBase
    {
        public ObservableRangeCollection<JobVacancy> JobVacancy { get; set; }
        public ObservableRangeCollection<JobVacancy> JobVacancyQueryResult { get; set; }
        public ObservableRangeCollection<JobVacancy> PreviousJobVacancy { get; set; }
        public Command SearchCommand { get; }
        public Command LoadFilter { get; }

        public JobVacancyViewModel()
        {
            JobVacancy = new ObservableRangeCollection<JobVacancy>();
            JobVacancyQueryResult = new ObservableRangeCollection<JobVacancy>();
            PreviousJobVacancy = new ObservableRangeCollection<JobVacancy>();

            //Search();

            SearchCommand = new Command(Search);
            LoadFilter = new Command(Filter);
        }

        string searchKeyword;
        public string SearchKeyword { get => searchKeyword; set => SetProperty(ref searchKeyword, value); }

        string jobTypeFilterSelection;
        public string JobTypeFilterSelection
        {
            get => jobTypeFilterSelection;
            //set => SetProperty(ref jobTypeFilterSelection, value);
            set => SetProperty(ref jobTypeFilterSelection, value);
        }

        string cityFilterSelection;
        public string CityFilterSelection
        {
            get => cityFilterSelection;
            set => SetProperty(ref cityFilterSelection, value);
        }

        string startFilterSelection;
        public string StartFilterSelection
        {
            get => startFilterSelection;
            set => SetProperty(ref startFilterSelection, value);
        }

        string endFilterSelection;
        public string EndFilterSelection
        {
            get => endFilterSelection;
            set => SetProperty(ref endFilterSelection, value);
        }


        private void Filter()
        {
            Console.WriteLine("Masuk filter");

            //Selanjutnya implementasi nilai Filter berasal dari input pengguna

            //JobTypeFilterSelection = "Programming";
            //startFilterSelection = "2022-01-02";
            //endFilterSelection = "2022-02-03";
            //cityFilterSelection = "Jakarta";

            //Getting all the original result from query process
            JobVacancy.Clear();
            foreach (JobVacancy job in JobVacancyQueryResult)
            {
                JobVacancy.Add(job);
            }

            if (jobTypeFilterSelection != null)
            {
                PreviousJobVacancy.Clear();
                foreach (JobVacancy job in JobVacancy)
                {
                    PreviousJobVacancy.Add(job);
                }

                JobVacancy.Clear();

                //Job type filtering
                foreach (JobVacancy job in PreviousJobVacancy)
                {
                    if (job.Job_Type == jobTypeFilterSelection)
                    {
                        Console.WriteLine(job.Job_Type);
                        JobVacancy.Add(job);
                    }

                }
            }

            if (startFilterSelection != null)
            {
                Console.WriteLine("Filter start date");
                PreviousJobVacancy.Clear();
                foreach (JobVacancy job in JobVacancy)
                {
                    PreviousJobVacancy.Add(job);
                }

                JobVacancy.Clear();

                //Job start recrutiment date filtering
                foreach (JobVacancy job in PreviousJobVacancy)
                {
                    if (DateTime.Compare(DateTime.Parse(job.Start_Recruitment_Date), DateTime.Parse(startFilterSelection)) >= 0)
                    {
                        Console.WriteLine(job.Start_Recruitment_Date);
                        JobVacancy.Add(job);
                    }

                }
            }

            if (endFilterSelection != null)
            {
                Console.WriteLine("Filter end date");
                PreviousJobVacancy.Clear();
                foreach (JobVacancy job in JobVacancy)
                {
                    PreviousJobVacancy.Add(job);
                }

                JobVacancy.Clear();

                //Job end recrutiment date filtering
                foreach (JobVacancy job in PreviousJobVacancy)
                {
                    if (DateTime.Compare(DateTime.Parse(job.End_Recruitment_Date), DateTime.Parse(endFilterSelection)) <= 0)
                    {
                        Console.WriteLine(job.End_Recruitment_Date);
                        JobVacancy.Add(job);
                    }

                }
            }

            if (cityFilterSelection != null)
            {
                Console.WriteLine("Filter City");
                PreviousJobVacancy.Clear();
                foreach (JobVacancy job in JobVacancy)
                {
                    PreviousJobVacancy.Add(job);
                }

                JobVacancy.Clear();

                //Job city filtering
                foreach (JobVacancy job in PreviousJobVacancy)
                {
                    if (job.City == cityFilterSelection)
                    {
                        Console.WriteLine(job.City);
                        JobVacancy.Add(job);
                    }

                }
            }
        }

        async void Search()
        {
            //Selanjutnya implementasi nilai SeachKeyword berupa input pengguna
            //SearchKeyword = "Software Engineer Developer";

            JobVacancy.Clear();
            JobVacancyQueryResult.Clear();
            PreviousJobVacancy.Clear();
            JobTypeFilterSelection = null;
            StartFilterSelection = null;
            EndFilterSelection = null;
            CityFilterSelection = null;

            if(SearchKeyword == null)
            {
                return;
            }

            var connString = "Host=ats-kelompok24.cwk6nsvvdnxx.ap-southeast-3.rds.amazonaws.com;Username=postgres;Password=kelompok24;Database=ats";

            await using var conn = new NpgsqlConnection(connString);

            await conn.OpenAsync();

            string query = @"SELECT job_id, company.company_id, company_name, job_name, start_recruitment_date, end_recruitment_date, job_type, job_opening.description, company.city, salary, profile_picture,
            REGEXP_MATCHES(job_name, @job_name) Regex_Job
            FROM job_opening
            INNER JOIN company ON job_opening.company_id = company.company_id ;";

            String[] separator = { " " };
            String[] strlist = SearchKeyword.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine("Job name splitted ");
            Console.WriteLine(strlist[0]);

            string searchQuery = SearchKeyword;
            foreach (String s in strlist)
            {
                searchQuery = searchQuery + "|" + s;
            }

            Console.WriteLine(searchQuery);


            try
            {
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("job_name", searchQuery);
                    cmd.Prepare();

                    Console.WriteLine("Here now");
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Console.WriteLine(reader.GetInt64(1));
                            Console.WriteLine(reader.GetString(2));
                            Console.WriteLine(reader.GetDate(4));
                            Console.WriteLine(reader.GetDate(5));

                            var Job_ID = reader.GetInt16(0);
                            var Company_ID = reader.GetInt16(1);
                            var Company_Name = reader.GetString(2);
                            var Job_Name = reader.GetString(3);
                            var Start_Recruitment_Date = reader.GetDate(4).ToString();
                            var End_Recruitment_Date = reader.GetDate(5).ToString();
                            var Job_Type = reader.GetString(6);
                            var Description = reader.GetString(7);
                            var City = reader.GetString(8);
                            var Salary = reader.GetInt64(9);
                            var Profile_Picture = reader.GetString(10);

                            JobVacancyQueryResult.Add(new JobVacancy { Job_ID = Job_ID, Company_ID = Company_ID, City = City, Company_Name = Company_Name, Job_Name = Job_Name, Start_Recruitment_Date = Start_Recruitment_Date, End_Recruitment_Date = End_Recruitment_Date, Job_Type = Job_Type, Description = Description, Salary=Salary, Profile_Picture=Profile_Picture });
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                conn.Close();
            }

            foreach (JobVacancy job in JobVacancyQueryResult)
            {
                JobVacancy.Add(job);
            }
        }

    }
}
