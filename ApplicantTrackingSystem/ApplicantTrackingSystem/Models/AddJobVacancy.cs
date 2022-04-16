using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicantTrackingSystem.Models
{
    public class AddJobVacancy
    {
        public int job_id { get; set; }
        public string job_name { get; set; }
        public string start_recruitment_date { get; set; }
        public string end_recruitment_date { get; set; }
        public string job_type { get; set; }
        public int salary { get; set; }
        public string description { get; set; }

    }
}
