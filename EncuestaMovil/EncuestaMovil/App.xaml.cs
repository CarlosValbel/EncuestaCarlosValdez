
using EncuestaMovil.Views;
using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EncuestaMovil
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new PaginaPrincipal();
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
