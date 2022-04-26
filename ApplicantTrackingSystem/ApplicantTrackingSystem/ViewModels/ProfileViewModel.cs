using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Npgsql;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using ApplicantTrackingSystem.Models;
using ApplicantTrackingSystem.Services;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Collections.Generic;
using System.Net.Http;

namespace ApplicantTrackingSystem.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        public CredentialModel credential = new CredentialModel();
        public CredentialModel credentialOut = new CredentialModel();
        public Profile ProfileQueryResult { get; set; }
        public Command ImagePickerCommand { get; }
        public Command LogoutCommand { get; }

        //public Action DisplayInvalidProfilePrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string full_name;
        private string email;
        private string profile_picture;
        private DateTime birthdate;
        private DateTime minbirthdate = new DateTime(2000, 1, 1);
        private DateTime maxbirthdate = DateTime.Now;
        private string phone_number;
        private string gender;
        private string country;
        private string city;
        private string headline;
        private string description;
        private string type;
        private bool isVisible;

        public string FullName
        {
            get { return full_name; }
            set
            {
                full_name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("FullName"));
            }
        }

        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsVisible"));
            }
        }

        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Type"));
            }
        }

        public string Email
        {
            get { return email; }

            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }

        }

        public string ProfilePicture
        {
            get { return profile_picture; }
            set
            {
                profile_picture = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ProfilePicture"));
            }
        }

        public DateTime Birthdate
        {
            get { return birthdate; }
            set
            {
                birthdate = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Birthdate"));
            }
        }

        public DateTime MinBirthdate
        {
            get { return minbirthdate; }
        }

        public DateTime MaxBirthdate
        {
            get { return maxbirthdate; }
        }

        public string Phone
        {
            get { return phone_number; }
            set
            {
                phone_number = value;
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

        public ProfileViewModel()
        {
            //How to access the monkey cache
            Console.WriteLine("CRED");
            var json = string.Empty;
            json = Barrel.Current.Get<string>("loginCredential");
            credential = JsonConvert.DeserializeObject<CredentialModel>(json);
            FetchAll();

            SaveCommand = new Command(async () => await UpdateProfile());
            ImagePickerCommand = new Command(ImagePicker);
            LogoutCommand = new Command(Logout);

        }

        async void Logout()
        {
            Console.WriteLine("LOGOUT");
            //Remove existing credential
            Barrel.Current.Empty(key: "loginCredential");

            //Barrel.Current.Empty("loginCredential");
            //Barrel.Current.EmptyAll();
            //Barrel.Current.EmptyExpired();

            //AtsService.client =  new HttpClient
            //{
            //    BaseAddress = new Uri(AtsService.BaseUrl)
            //};

            // Removing header from httpClient
            AtsService.removeHeader("Authorization");

            // Check/try access the cache (should not exist)
            try
            {
                Console.WriteLine("CRED LOGOUT");
                var jsonOut = string.Empty;
                jsonOut = Barrel.Current.Get<string>("loginCredential");
                credentialOut = JsonConvert.DeserializeObject<CredentialModel>(jsonOut);
                Console.WriteLine("BARREL CONTENT: " + credentialOut.token);
            }
            catch (Exception ex)
            { 
                // Do nothing
            }

            // Navigate to Main Page by re-creating new MainPage
            (Application.Current).MainPage = new AppShell();

        }

        async void FetchAll()
        {
            var ProfileQueryResult = await AtsService.GetProfile(credential.token, credential.user_id);
            if (ProfileQueryResult != null)
            {
                //Console.WriteLine(ProfileQueryResult.birthdate);

                FullName = ProfileQueryResult.full_name;
                Headline = ProfileQueryResult.headline;
                Email = ProfileQueryResult.email;
                ProfilePicture = ProfileQueryResult.profile_picture;
                if (ProfileQueryResult.birthdate != null) {
                    Birthdate = DateTime.ParseExact(ProfileQueryResult.birthdate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }
                Phone = ProfileQueryResult.phone_number;
                Gender = ProfileQueryResult.gender;
                Country = ProfileQueryResult.country;
                City = ProfileQueryResult.city;
                Description = ProfileQueryResult.description;
                Type = ProfileQueryResult.type;
                IsVisible = Type == "applicant";

                Console.WriteLine("TYPE: " + ProfileQueryResult.type);
            }
            else
            {
                Console.WriteLine("EMPTYY");
            }
        }

        public ICommand SaveCommand { protected set; get; }

        private async Task UpdateProfile()
        {
            try
            {
                var profile = new Profile
                {
                    full_name = full_name,
                    headline = headline,
                    email = email,
                    profile_picture = profile_picture,
                    birthdate = birthdate.ToString("dd/MM/yyyy"),
                    phone_number = phone_number,
                    gender = gender,
                    country = country,
                    city = city,
                    description = description,
                };

                await AtsService.UpdateProfile(profile, credential.token);

                await Shell.Current.GoToAsync("..");

                //Console.WriteLine(profile.birthdate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async void ImagePicker()
        {
            var image = await MediaPicker.PickPhotoAsync();

            if (image == null)
            {
                return;
            }

            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(await image.OpenReadAsync()),
                "picture", image.FileName);

            var uploadResp = await AtsService.UploadPicture(content, credential.token);

            ProfilePicture = uploadResp.link;
            Console.WriteLine(uploadResp);
            Console.WriteLine("REQUIREMENT LINK: ");
            Console.WriteLine(uploadResp.link);

        }


        //async public void OnSubmit()
        //{
        //    var connString = "Host=ec2-3-219-204-29.compute-1.amazonaws.com;Database=d7p6gej9knqefg;Username=ptyxepvslwevdw;Password=2cff69469572cf04b3e738727d1503ccd0e05efc9b1d73f9ac6061954f094771";

        //    await using var conn = new NpgsqlConnection(connString);
        //    Console.WriteLine("connecting");
        //    await conn.OpenAsync();
        //    Console.WriteLine("connected");

        //    string query = "UPDATE user SET full_name = @full_name, password = @password, profile_picture = @profile_picture, birthdate = @birthdate, phone_number = @phone_number, gender = @gender, country = @country, city = @city, headline = @headline, description = @description, status = @status, type = @type WHERE email=@email";
        //    try
        //    {
        //        using (var cmd = new NpgsqlCommand(query, conn))
        //        {
        //            cmd.Parameters.AddWithValue("full_name", full_name);
        //            cmd.Parameters.AddWithValue("email", email);
        //            cmd.Parameters.AddWithValue("password", password);
        //            cmd.Parameters.AddWithValue("profile_picture", profile_picture);
        //            cmd.Parameters.AddWithValue("birthdate", birthdate);
        //            cmd.Parameters.AddWithValue("phone_number", phone_number);
        //            cmd.Parameters.AddWithValue("gender", gender);
        //            cmd.Parameters.AddWithValue("country", country);
        //            cmd.Parameters.AddWithValue("city", city);
        //            cmd.Parameters.AddWithValue("headline", headline);
        //            cmd.Parameters.AddWithValue("description", description);
        //            cmd.Parameters.AddWithValue("status", status);
        //            cmd.Parameters.AddWithValue("type", type);
        //            cmd.Prepare();

        //            cmd.ExecuteNonQueryAsync();
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        conn.Close();
        //    }
        //}
    }
}