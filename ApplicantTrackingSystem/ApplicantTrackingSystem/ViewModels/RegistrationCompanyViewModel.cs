﻿using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using ApplicantTrackingSystem.Services;

namespace ApplicantTrackingSystem.ViewModels
{
    public class RegistrationCompanyViewModel : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string name;
        private string email;
        private string password;
        private string phone;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
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

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
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


        public ICommand SubmitCommand { protected set; get; }

        public RegistrationCompanyViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }

        async public void OnSubmit()
        {
            var registerResp = await AtsService.PostRegister(Email, Name, Password, Phone, "company");

            Console.WriteLine("REGISTRATION RESPONSE");
            if (registerResp.message == "Registration successful")
            {
                Email = "";
                Name = "";
                Password = "";
                Phone = "";
            }
            Console.WriteLine(registerResp.message);
        }

        async public void OnSubmitt()
        {
            bool dbExists;
            var connString = "Host=ec2-3-219-204-29.compute-1.amazonaws.com;Database=d7p6gej9knqefg;Username=ptyxepvslwevdw;Password=2cff69469572cf04b3e738727d1503ccd0e05efc9b1d73f9ac6061954f094771";

            await using var conn = new NpgsqlConnection(connString);
            Console.WriteLine("connecting");
            await conn.OpenAsync();
            Console.WriteLine("connected");

            string query = "SELECT company_id FROM company WHERE email=@email";
            try
            {
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Prepare();

                    dbExists = cmd.ExecuteScalar() != null;
                }

                if (dbExists)
                {
                    Console.WriteLine("Email already exists");
                    DisplayInvalidLoginPrompt();
                }
                else
                {
                    using (var cmd1 = new NpgsqlCommand("INSERT INTO company (company_name, email, password, phone) VALUES (@company_name, @email, @password, @phone)", conn))
                    {
                        cmd1.Parameters.AddWithValue("company_name", name);
                        cmd1.Parameters.AddWithValue("email", email);
                        cmd1.Parameters.AddWithValue("password", password);
                        cmd1.Parameters.AddWithValue("phone", phone);
                        await cmd1.ExecuteNonQueryAsync();
                    };
                    Console.WriteLine("Successfully inserted user");
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
