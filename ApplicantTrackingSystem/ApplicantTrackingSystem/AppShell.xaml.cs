using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApplicantTrackingSystem
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Registering route for page
            Routing.RegisterRoute(nameof(JobCatalogPage), typeof(JobCatalogPage));
            Routing.RegisterRoute(nameof(RegistrationApplicantPage), typeof(RegistrationApplicantPage));
            Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
            Routing.RegisterRoute(nameof(JobApplyPage), typeof(JobApplyPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(JobApplicationProgressPage), typeof(JobApplicationProgressPage));

            Routing.RegisterRoute(nameof(RegistrationCompanyPage), typeof(RegistrationCompanyPage));
            Routing.RegisterRoute(nameof(JobVacancyPost), typeof(JobVacancyPost));


        }
    }
}