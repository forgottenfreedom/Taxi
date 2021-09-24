using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Taxi.Views;

namespace Taxi.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Main";
        }

        private string datum = LoginPage.Schichttag;
        public string Datum
        {
            get => datum;
            set => SetProperty(ref datum, value);
        }
    }
}