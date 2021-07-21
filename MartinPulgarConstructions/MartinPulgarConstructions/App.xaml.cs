using System;
using MartinPulgarConstruction.SDKs;
using MartinPulgarConstruction.Service;
using MartinPulgarConstructions.Views;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MartinPulgarConstructions
{
    public partial class App : Application
    {
        internal static IServiceProvider ServiceProvider { get; set; }

        public App()
        {
            InitializeComponent();

            SetupServices();

            MainPage = new NewDiaryPage();
            
        }

        private void SetupServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IDiaryService>(new DiaryService());
            ServiceProvider = services.BuildServiceProvider();
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
