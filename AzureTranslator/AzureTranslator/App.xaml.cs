using AzureTranslator.Services;
using AzureTranslator.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AzureTranslator
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<AuthenticationService>();
            DependencyService.Register<TextTranslationService>();
           MainPage = new TranslatorPage();
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
