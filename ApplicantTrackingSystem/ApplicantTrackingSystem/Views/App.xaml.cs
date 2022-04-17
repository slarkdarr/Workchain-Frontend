﻿using MonkeyCache.FileStore;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Lato-Black.ttf", Alias="LatoBlack")]
[assembly: ExportFont("Lato-Bold.ttf", Alias = "LatoBold")]
[assembly: ExportFont("Lato-Regular.ttf", Alias = "Lato")]
namespace ApplicantTrackingSystem
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Barrel.ApplicationId = AppInfo.PackageName;
            MainPage = new AppShell();
            //MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
