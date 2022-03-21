using ApplicantTrackingSystem.Models;
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

        static string BaseUrl = "YOUR URL";

        static HttpClient client;

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

        public static async Task AddJobApplication(JobApplication jobApplication)
        {

            var json = JsonConvert.SerializeObject(jobApplication);
            var content =
                new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Coffee", content);

            if (!response.IsSuccessStatusCode)
            {

            }
        }
    }
}
