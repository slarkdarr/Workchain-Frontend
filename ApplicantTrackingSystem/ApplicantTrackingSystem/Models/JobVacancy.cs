using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicantTrackingSystem.Models
{
    public class JobVacancy
    {
        public int Job_ID { get; set; }
        public int Company_ID { get; set; }
        public string Company_Name { get; set; }
        public string City { get; set; }
        public string Job_Name { get; set; }
        public string Start_Recruitment_Date { get; set; }
        public string End_Recruitment_Date { get; set; }
        public string Job_Type { get; set; }
        public string Description { get; set; }
        public Int64 Salary { get; set; }
        public string Profile_Picture { get; set; }
    }
}
