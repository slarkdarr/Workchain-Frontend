using ApplicantTrackingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApplicantTrackingSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(PassedApplication), nameof(PassedApplication))]
    public partial class ApplicationDetailPage : ContentPage
    {
        public string PassedApplication { get; set; }
        public ApplicationDetailViewModel vm = new ApplicationDetailViewModel();

        public ApplicationDetailPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = vm;
            Console.WriteLine("APPLICANT HASIL PASSING Detail page");
            Console.WriteLine(PassedApplication);
            vm.ApplicationId = PassedApplication;
            vm.LoadCommand.Execute(null);
        }
    }
}
