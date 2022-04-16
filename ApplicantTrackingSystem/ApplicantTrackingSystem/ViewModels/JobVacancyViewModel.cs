using ApplicantTrackingSystem.Models;
using ApplicantTrackingSystem.Services;
using MonkeyCache.FileStore;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;
using System.ComponentModel;

namespace ApplicantTrackingSystem.ViewModels
{
    public class JobVacancyViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public CredentialModel credential = new CredentialModel(); 
        public ObservableRangeCollection<JobVacancy> JobVacancy { get; set; }
        public ObservableRangeCollection<JobVacancy> JobVacancyQueryResult { get; set; }
        public ObservableRangeCollection<JobVacancy> PreviousJobVacancy { get; set; }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public MvvmHelpers.Commands.Command SearchCommand { get; }
        public MvvmHelpers.Commands.Command LoadFilter { get; }
        public MvvmHelpers.Commands.Command FetchAllCommand { get; }
        public MvvmHelpers.Commands.Command FetchByIdCommand { get; }

        public AsyncCommand<object> SelectedCommand { get; }

        private string currJobName;
        private string currCompanyName;
        private string currSalary;
        private string currDescription;
        private string currJobId;

        public string CurrJobName
        {
            get { return currJobName; }
            set
            {
                currJobName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CurrJobName"));
            }
        }

        public string CurrCompanyName
        {
            get { return currCompanyName; }
            set
            {
                currCompanyName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CurrCompanyName"));
            }
        }

        public string CurrSalary
        {
            get { return currSalary; }
            set
            {
                currSalary = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CurrSalary"));
            }
        }

        public string CurrDescription
        {
            get { return currDescription; }
            set
            {
                currDescription = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CurrDescription"));
            }
        }

        public string CurrJobId
        {
            get { return currJobId; }
            set
            {
                currJobId = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CurrJobId"));
            }
        }

        public JobVacancyViewModel()
        {
            //How to access the monkey cache
            Console.WriteLine("CRED");
            var json = string.Empty;
            json = Barrel.Current.Get<string>("loginCredential");
            credential = JsonConvert.DeserializeObject<CredentialModel>(json);
            Console.WriteLine(credential.token);

            JobVacancy = new ObservableRangeCollection<JobVacancy>();
            JobVacancyQueryResult = new ObservableRangeCollection<JobVacancy>();
            PreviousJobVacancy = new ObservableRangeCollection<JobVacancy>();

            SearchCommand = new MvvmHelpers.Commands.Command(Search);
            LoadFilter = new MvvmHelpers.Commands.Command(Filter);
            FetchAllCommand = new MvvmHelpers.Commands.Command(FetchAll);
            FetchByIdCommand = new MvvmHelpers.Commands.Command(FetchById);

            SelectedCommand = new AsyncCommand<object>(Selected);

            //FetchAll();
        }

        JobVacancy selectedJob;
        public JobVacancy SelectedJob
        {
            get => selectedJob;
            set => SetProperty(ref selectedJob, value);
        }

        async Task Selected(object args)
        {
            var job = args as JobVacancy;
            if (job == null)
            {
                return;
            }
            Console.WriteLine("JOB SELECTED!!!!");
            SelectedJob = null;

            // Navigate to Job Detail Page
            var route = $"{nameof(DetailPage)}?PassedJob={job.Job_ID}";
            await Shell.Current.GoToAsync(route);

        }

        string searchKeyword;
        public string SearchKeyword { get => searchKeyword; set => SetProperty(ref searchKeyword, value); }

        string jobTypeFilterSelection;
        public string JobTypeFilterSelection
        {
            get => jobTypeFilterSelection;
            set => SetProperty(ref jobTypeFilterSelection, value);
        }

        string cityFilterSelection;
        public string CityFilterSelection
        {
            get => cityFilterSelection;
            set => SetProperty(ref cityFilterSelection, value);
        }

        string companyFilterSelection;
        public string CompanyFilterSelection
        {
            get => companyFilterSelection;
            set => SetProperty(ref companyFilterSelection, value);
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

            //Getting all the original result from query process
            JobVacancy.Clear();
            foreach (JobVacancy job in JobVacancyQueryResult)
            {
                JobVacancy.Add(job);
            }

            if (jobTypeFilterSelection != null)
            {
                Console.WriteLine("Masuk filter JOB TYPE");
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
                        //Console.WriteLine(job.Job_Type);
                        JobVacancy.Add(job);
                    }

                }
            }

            if (companyFilterSelection != null)
            {
                Console.WriteLine("Masuk filter company name");
                PreviousJobVacancy.Clear();
                foreach (JobVacancy job in JobVacancy)
                {
                    PreviousJobVacancy.Add(job);
                }

                JobVacancy.Clear();

                //Job type filtering
                foreach (JobVacancy job in PreviousJobVacancy)
                {
                    if (job.Company_Name == companyFilterSelection)
                    {
                        //Console.WriteLine(job.Company_Name);
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
                        //Console.WriteLine(job.Start_Recruitment_Date);
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
                        //Console.WriteLine(job.End_Recruitment_Date);
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
                    if (job.Company_City == cityFilterSelection)
                    {
                        //Console.WriteLine(job.Company_City);
                        JobVacancy.Add(job);
                    }

                }
            }
        }

        async void Search()
        {
            Console.WriteLine("Searching by keyword");

            JobVacancy.Clear();
            JobVacancyQueryResult.Clear();
            PreviousJobVacancy.Clear();
            JobTypeFilterSelection = null;
            StartFilterSelection = null;
            EndFilterSelection = null;
            CityFilterSelection = null;

            if (searchKeyword == null || searchKeyword == "")
            {
                FetchAll();
                return;
            }

            JobVacancyQueryResult = await AtsService.searchJobOpening(credential.token, searchKeyword);
            if (JobVacancyQueryResult != null)
            {
                Console.WriteLine("Ada Fetch All berdasarkan keyword:");
                foreach (JobVacancy job in JobVacancyQueryResult)
                {
                    JobVacancy.Add(job);
                }
            }
            else
            {
                Console.WriteLine("EMPTYY");
            }
        }

        
        async void FetchAll()
        {
            JobVacancyQueryResult = await AtsService.getJobOpening(credential.token);
            if (JobVacancyQueryResult != null)
            {
                Console.WriteLine("Ada Fetch All :");
                foreach (JobVacancy job in JobVacancyQueryResult)
                {
                    JobVacancy.Add(job);
                }
            }
            else
            {
                Console.WriteLine("EMPTYY");
            }
        }

    }
}
