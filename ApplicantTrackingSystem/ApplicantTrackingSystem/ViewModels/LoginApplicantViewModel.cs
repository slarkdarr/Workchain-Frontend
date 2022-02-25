using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Npgsql;

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

        public LoginApplicantViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }

        async public void OnSubmit()
        {

            var connString = "Host=ats-kelompok24.cwk6nsvvdnxx.ap-southeast-3.rds.amazonaws.com;Username=postgres;Password=kelompok24;Database=ats";
            //var connString = "Host=localhost;Database=postgres;Username=shifa";
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
                    
                } else
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