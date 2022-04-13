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
            //await Shell.Current.GoToAsync("//MenuApplicant");

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

        async public void OnSubmitt()
        {

            var connString = "Host=ec2-3-219-204-29.compute-1.amazonaws.com;Database=d7p6gej9knqefg;Username=ptyxepvslwevdw;Password=2cff69469572cf04b3e738727d1503ccd0e05efc9b1d73f9ac6061954f094771";

            await using var conn = new NpgsqlConnection(connString);

            Console.WriteLine("Opening");
            await conn.OpenAsync();

            Console.WriteLine("Opened");

            bool dbExists;
            string query = "SELECT applicant_id FROM applicant WHERE email=@email AND password=@password";
            try
            {
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    Console.WriteLine(email);
                    Console.WriteLine(password);

                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("password", password);

                    Console.WriteLine("Here now");

                    cmd.Prepare();

                    Console.WriteLine("Here now");

                    dbExists = cmd.ExecuteScalar() != null;
                    Console.WriteLine(dbExists);
                }

                if (!dbExists)
                {
                    Console.WriteLine("Gaada");
                    DisplayInvalidLoginPrompt();

                }
                else
                {
                    Console.WriteLine("Login Successful!");
                    DisplayValidLoginPrompt();
                }





            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                conn.Close();
            }
        }
    }
}