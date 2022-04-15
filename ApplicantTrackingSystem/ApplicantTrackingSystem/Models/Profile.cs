using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicantTrackingSystem.Models
{
    public class Profile
    {
        public int user_id { get; set; }
        public string full_name { get; set; }
        public string email { get; set; }
        public string profile_picture { get; set; }
        public string birthdate { get; set; }
        public string phone_number { get; set; }
        public string gender { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string headline { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string type { get; set; }
    }
}
