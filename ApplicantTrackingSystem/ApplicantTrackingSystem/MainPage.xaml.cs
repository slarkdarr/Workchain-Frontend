﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ApplicantTrackingSystem
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void LoginButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginApplicantPage());
        }

        private void LoginCompanyButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginCompanyPage());
        }
    }
}
