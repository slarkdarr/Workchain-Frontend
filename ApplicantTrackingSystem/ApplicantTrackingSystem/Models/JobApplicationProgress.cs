using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicantTrackingSystem.Models
{
    public class JobApplicationProgress
    {
        public string Job_Name { get; set; }
        public string Status { get; set; }
        public string Company_Name { get; set; }
        public string Company_picture { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Apply_date { get; set; }
    }
}
