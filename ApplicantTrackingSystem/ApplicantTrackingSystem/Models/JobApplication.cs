using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicantTrackingSystem.Models
{
    public class JobApplication
    {
        public int application_id { get; set; }
        public int job_id { get; set; }
        public int applicant_id { get; set; }
        public string apply_date { get; set; }
        public string requirement_link { get; set; }
        public string status { get; set; }
        public string applicant_name { get; set; }
        public string applicant_email { get; set; }
        public string applicant_telp { get; set; }
        public string interview_date { get; set; }
        public string interview_time { get; set; }
        public string interview_link { get; set; }
        public string job_name { get; set; }
        public string company_name { get; set; }
        public string company_picture { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string applicant_picture { get; set; }
    }
}
