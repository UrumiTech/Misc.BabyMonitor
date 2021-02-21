using System;
using UrumiTech.BabyMonitor.Services;
using UrumiTech.BabyMonitor.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UrumiTech.BabyMonitor
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
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
