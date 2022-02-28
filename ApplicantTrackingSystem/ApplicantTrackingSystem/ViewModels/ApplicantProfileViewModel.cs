using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Npgsql;

namespace ApplicantTrackingSystem.ViewModels
{
    public class ApplicantProfileViewModel : INotifyPropertyChanged
    {
        //public Action DisplayInvalidProfilePrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string applicant_name;
        private string email;
        private string password;
        private string profile_picture;
        private DateTime birthdate;
        private string phone;
        private string gender;
        private string country;
        private string city;
        private string headline;
        private string description;
        private string status;

        public string ApplicantName
        {
            get { return applicant_name; }
            set
            {
                applicant_name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ApplicantName"));
            }
        }

        public string Email
        {
            get { return email; }
            /*
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
            */
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

        public string ProfilePicture
        {
            get { return profile_picture; }
            set
            {
                ProfilePicture = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ProfilePicture"));
            }
        }

        public DateTime BirthDate
        {
            get { return birthdate; }
            set
            {
                birthdate = value;
                PropertyChanged(this, new PropertyChangedEventArgs("BirthDate"));
            }
        }

        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Phone"));
            }
        }

        public string Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Gender"));
            }
        }

        public string Country
        {
            get { return country; }
            set
            {
                country = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Country"));
            }
        }

        public string City
        {
            get { return city; }
            set
            {
                city = value;
                PropertyChanged(this, new PropertyChangedEventArgs("City"));
            }
        }

        public string Headline
        {
            get { return headline; }
            set
            {
                headline = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Headline"));
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

        public string Status
        {
            get { return status; }
            /*
            set
            {
                status = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            }
            */
        }

        public ICommand SubmitCommand { protected set; get; }

        public ApplicantProfileViewModel()
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

            string query = "UPDATE applicant SET applicant_name = @applicant_name, password = @password, profile_picture = @profile_picture, birthdate = @birthdate, phone = @phone, gender = @gender, country = @country, city = @city, headline = @headline, description = @description, status = @status WHERE email=@email";
            try
            {
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("applicant_name", applicant_name);
                    cmd.Parameters.AddWithValue("password", password);
                    cmd.Parameters.AddWithValue("profile_picture", profile_picture);
                    cmd.Parameters.AddWithValue("birthdate", birthdate);
                    cmd.Parameters.AddWithValue("phone", phone);
                    cmd.Parameters.AddWithValue("gender", gender);
                    cmd.Parameters.AddWithValue("country", country);
                    cmd.Parameters.AddWithValue("city", city);
                    cmd.Parameters.AddWithValue("headline", headline);
                    cmd.Parameters.AddWithValue("description", description);
                    cmd.Parameters.AddWithValue("status", status);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Prepare();

                    cmd.ExecuteNonQueryAsync();
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