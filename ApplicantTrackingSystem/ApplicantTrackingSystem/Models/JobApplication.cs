using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicantTrackingSystem.Models
{
    public class JobApplication
    {
        public int job_id { get; set; }
        public string apply_date { get; set; }
        public string requirement_link { get; set; }
        public string status { get; set; }
        public string applicant_name { get; set; }
        public string applicant_email { get; set; }
        public string applicant_telp { get; set; }
    }
}
