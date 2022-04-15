using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Npgsql;
using ApplicantTrackingSystem.Services;

namespace ApplicantTrackingSystem.ViewModels
{
    public class LoginApplicantViewModel : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
        public Action DisplayValidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string email;
        private string password;

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }

        public ICommand SubmitCommand { protected set; get; }
        public ICommand RegistationCommand { protected set; get; }

        public LoginApplicantViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
            RegistationCommand = new Command(OnRegistrationClicked);
        }

        

        async public void OnSubmit()
        {
            await Shell.Current.GoToAsync("//MenuApplicant");

            var loginResp = await AtsService.PostLogin(Email, Password, "applicant");

            if (loginResp != null)
            {
                Console.WriteLine("TOKEN :");
                Console.WriteLine(loginResp.token);
                Console.WriteLine("Full Name :");
                Console.WriteLine(loginResp.full_name);

                // Navigasi ke halaman job catalog page
                //var route = $"{nameof(JobCatalogPage)}";
                //await Shell.Current.GoToAsync("Menu");
                await Shell.Current.GoToAsync("//MenuApplicant");
            }
            else
            {
                Console.WriteLine("EMPTYY");
                DisplayInvalidLoginPrompt();
            }
        }

        async public void OnRegistrationClicked()
        {
            // Navigasi ke halaman registrasi
            Console.WriteLine("View Model - Navigating to registration page");
            var route = $"{nameof(RegistrationApplicantPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}