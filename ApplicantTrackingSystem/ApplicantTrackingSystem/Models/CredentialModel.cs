using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicantTrackingSystem.Models
{
    public class CredentialModel
    {
        public int user_id { get; set; }
        public string email { get; set; }
        public string full_name { get; set; }
        public string token { get; set; }
    }
}
