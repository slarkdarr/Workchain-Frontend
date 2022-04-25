using ApplicantTrackingSystem.Models;
using MonkeyCache.FileStore;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingSystem.Services
{
    public static class AtsService
    {
        //static string Baseurl = DeviceInfo.Platform == DevicePlatform.Android ?
        //                                    "http://10.0.2.2:5000" : "http://localhost:5000";

        public static string BaseUrl = "https://ats-kelompok24.herokuapp.com";

        public static HttpClient client;

        static AtsService()
        {
            try
            {
                client = new HttpClient
                {
                    BaseAddress = new Uri(BaseUrl)
                };
            }
            catch
            {

            }
        }

        public static void removeHeader(string headerName)
        {
            try
            {
                client.DefaultRequestHeaders.Remove(headerName);
                Console.WriteLine("Header removed");
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }


        public static async Task<CredentialModel> PostLogin(string email, string password, string type)
        {
            var login = new Login
            {
                email = email,
                password = password,
                type = type
            };
            Console.WriteLine("DI LOGIN ATS SERVICE");
            Console.WriteLine(email);
            Console.WriteLine(password);

            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("User/login", content);

            var contentResp = await response.Content.ReadAsStringAsync();
            var credential = JsonConvert.DeserializeObject<CredentialModel>(contentResp);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("IS SUCCESS STATUS CODE");
                Console.WriteLine(response);
                Barrel.Current.EmptyAll();
                Barrel.Current.Add("loginCredential", contentResp, TimeSpan.FromMinutes(30));
                return credential;
            }
            else
            {
                Console.WriteLine("IS NOT SUCCESS STATUS CODE");
                Console.WriteLine(response);
                return null;
            }
        }

        public static async Task<RegistrationModel> PostRegister(string email, string full_name,
            string password, string phone_number, string type)
        {
            var register = new Register
            {
                email = email,
                full_name = full_name,
                password = password,
                phone_number = phone_number,
                type = type
            };

            var json = JsonConvert.SerializeObject(register);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("User/register", content);
            var contentResp = await response.Content.ReadAsStringAsync();
            var jsonResp = JsonConvert.DeserializeObject<RegistrationModel>(contentResp);

            return jsonResp;

        }

        public static async Task<ObservableRangeCollection<JobVacancy>> getJobOpening(string token)
        {
            try
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            //client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var json = await client.GetStringAsync("JobOpening");
            var jobVacancies = JsonConvert.DeserializeObject<ObservableRangeCollection<JobVacancy>>(json);
            Console.WriteLine(json);
            return jobVacancies;
        }

        public static async Task<ObservableRangeCollection<JobVacancy>> searchJobOpening(string token, string keyword)
        {
            try
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            //client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var endpoint = "JobOpening/keyword/" + keyword;
            var json = await client.GetStringAsync(endpoint);
            var jobVacancies = JsonConvert.DeserializeObject<ObservableRangeCollection<JobVacancy>>(json);
            return jobVacancies;
        }

        public static async Task<JobVacancy> getJobOpeningDetail(string token, string jobID)
        {
            try
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            //client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var endpoint = "JobOpening/" + jobID;
            var json = await client.GetStringAsync(endpoint);
            var jobVacancies = JsonConvert.DeserializeObject<ObservableRangeCollection<JobVacancy>>(json);
            var jobVacancy = new JobVacancy();
            foreach (JobVacancy job in jobVacancies)
            {
                jobVacancy = job;
            }
            return jobVacancy;
        }

        public static async Task<ObservableRangeCollection<JobVacancy>> GetJobOpeningById(string token, string id)
        {
            try
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            //client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var json = await client.GetStringAsync("JobOpening/" + id);
            var jobVacancy = JsonConvert.DeserializeObject<ObservableRangeCollection<JobVacancy>>(json);
            return jobVacancy;
        }

        public static async Task<string> AddJobOpening(AddJobVacancy jobVacancy, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
            catch (Exception ex)
            {
                //throw ex;
            }

            //client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var json = JsonConvert.SerializeObject(jobVacancy);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("JobOpening/add", content);
            var contentResp = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("IS SUCCESS STATUS CODE");
                Console.WriteLine(response);
                return contentResp;
            }
            else
            {
                Console.WriteLine("IS NOT SUCCESS STATUS CODE");
                Console.WriteLine(response);
                Console.WriteLine(contentResp);
                return null;
            }
        }

        public static async Task<ObservableRangeCollection<JobApplication>> GetJobApplication(string token)
        {

            try
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
            catch
            {
                //Do nothing
            }
            var json = await client.GetStringAsync("JobApplication/applicant");
            Console.WriteLine(json);
            var jobApplications = JsonConvert.DeserializeObject<ObservableRangeCollection<JobApplication>>(json);
            return jobApplications;
        }

        public static async Task<ObservableRangeCollection<JobApplication>> GetJobApplicationById(string token, string id)
        {

            try
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
            catch
            {
                //Do nothing
            }
            var json = await client.GetStringAsync("JobApplication/" + id);
            Console.WriteLine(json);
            var jobApplication = JsonConvert.DeserializeObject<ObservableRangeCollection<JobApplication>>(json);
            return jobApplication;
        }

        public static async Task<ObservableRangeCollection<JobApplication>> GetApplicants(string token)
        {

            try
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
            catch
            {
                //Do nothing
            }
            var json = await client.GetStringAsync("JobApplication/company");
            Console.WriteLine(json);
            var jobApplications = JsonConvert.DeserializeObject<ObservableRangeCollection<JobApplication>>(json);
            return jobApplications;
        }

        public static async Task<string> AddJobApplication(JobApplicationAdd jobApplication, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
            catch (Exception ex)
            {
                //throw ex;
            }

            //client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var json = JsonConvert.SerializeObject(jobApplication);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("JobApplication/add", content);
            var contentResp = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("IS SUCCESS STATUS CODE");
                Console.WriteLine(response);
                return contentResp;
            }
            else
            {
                Console.WriteLine("IS NOT SUCCESS STATUS CODE");
                Console.WriteLine(response);
                Console.WriteLine(contentResp);
                return null;
            }
        }

        public static async Task<ApplicantJobApplicationModel> GetApplicantApplicationData(string token, string used_id)
        {

            try
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
            catch
            {
                //Do nothing
            }
            var endpoint = "User/" + used_id;
            var json = await client.GetStringAsync(endpoint);
            Console.WriteLine(json);
            var applicantData = JsonConvert.DeserializeObject<ApplicantJobApplicationModel>(json);
            return applicantData;
        }

        public static async Task<string> PutUpdateJobApplication(UpdateJobApplication jobApplication, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
            catch (Exception ex)
            {
                //throw ex;
            }

            //client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var json = JsonConvert.SerializeObject(jobApplication);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("JobApplication", content);
            var contentResp = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("IS SUCCESS STATUS CODE");
                Console.WriteLine(response);
                return contentResp;
            }
            else
            {
                Console.WriteLine("IS NOT SUCCESS STATUS CODE");
                Console.WriteLine(response);
                Console.WriteLine(contentResp);
                return null;
            }
        }

        public static async Task<UploadFileModel> PostUploadFile(string token, MultipartFormDataContent content)
        {
            try
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
            catch (Exception ex)
            {
                //throw ex;
            }

            var response = await client.PostAsync("Upload/file", content);
            var contentResp = await response.Content.ReadAsStringAsync();
            var jsonResp = JsonConvert.DeserializeObject<UploadFileModel>(contentResp);

            return jsonResp;

        }

        public static async Task<Profile> GetProfile(string token, int user_id)
        {
            try
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            
            string Profile = "User/" + user_id.ToString();
            var json = await client.GetStringAsync(Profile);
            var profileResponse = JsonConvert.DeserializeObject<Profile>(json);
            //Console.WriteLine(profileResponse.birthdate);

            return profileResponse;
        }

        public static async Task<string> UpdateProfile(Profile profile, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
            catch (Exception ex)
            {
                //throw ex;
            }

            //client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var json = JsonConvert.SerializeObject(profile);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("User/", content);
            var contentResp = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("IS SUCCESS STATUS CODE");
                Console.WriteLine(response);
                return contentResp;
            }
            else
            {
                Console.WriteLine("IS NOT SUCCESS STATUS CODE");
                Console.WriteLine(response);
                Console.WriteLine(contentResp);
                return null;
            }

        }

        public static async Task<UploadImageModel> UploadPicture(MultipartFormDataContent content, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
            catch (Exception ex)
            {
                //throw ex;
            }

            var response = await client.PostAsync("Upload/picture", content);
            var contentResp = await response.Content.ReadAsStringAsync();
            var jsonResp = JsonConvert.DeserializeObject<UploadImageModel>(contentResp);
            
            return jsonResp;

        }
    }
}
