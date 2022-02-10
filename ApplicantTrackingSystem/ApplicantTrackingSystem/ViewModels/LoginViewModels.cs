using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Npgsql;

var connString = "Host=ats-kelompok24.cwk6nsvvdnxx.ap-southeast-3.rds.amazonaws.com;Username=postgres;Password=kelompok24;Database=ats";

await using var conn = new NpgsqlConnection(connString);
await conn.OpenAsync();

namespace ApplicantTrackingSystem.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
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

        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }

        public void OnSubmit()
        {
            bool dbExists;
            string query = "SELECT COUNT(*) FROM applicant WHERE email=@email AND password=@password";
            try
            {
                await using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("password", password);
                    cmd.Prepare();

                    dbExists = cmd.ExecuteScalar() != null;
                }

                if (!dbExists)
                {
                    DisplayInvalidLoginPrompt();
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