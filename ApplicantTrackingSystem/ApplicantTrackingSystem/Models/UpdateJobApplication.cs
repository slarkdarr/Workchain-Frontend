using System;
namespace ApplicantTrackingSystem.Models
{
    public class UpdateJobApplication
    {
        public int application_id { get; set; }
        public string status { get; set; }
        public string interview_date { get; set; }
        public string interview_time { get; set; }
        public string interview_link { get; set; }

    }
}
