using System;
using System.Collections.Generic;
using ApplicantTrackingSystem.ViewModels;

using Xamarin.Forms;

namespace ApplicantTrackingSystem
{
    public partial class JobCatalogPage : ContentPage
    {
        public JobCatalogPage()
        {
            BindingContext = new JobVacancyViewModel();

            InitializeComponent();
            Console.WriteLine("WILL EXECUTE: ");
            Console.WriteLine(BindingContext);
            var vm = BindingContext as JobVacancyViewModel;

            vm.FetchAllCommand.Execute(null);
        }

        private void JobComponentTapped(object sender, EventArgs e)
        {
            Console.WriteLine("This is now a button sort offfff");
            Console.WriteLine(sender.GetType());
            Console.WriteLine(e.GetType());
        }

        private void FilterChosen(object sender, EventArgs e)
        {
            string[] filterNamesList = { "CityStack", "JobStack", "CompanyStack" };
            var entry = (Button)sender;
            var classId = entry.ClassId;
            var identifier = classId + "Stack";

            var element = (StackLayout)Filters.FindByName(identifier);
            bool lastState = element.IsVisible;
            Console.WriteLine(identifier);

            foreach (string stackName in filterNamesList)
            {
                var stack = (StackLayout)Filters.FindByName(stackName);
                stack.IsVisible = false;
            }

            if (!lastState)
            {
                element.IsVisible = true;
            }

        }
    }
}
