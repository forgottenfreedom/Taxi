using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Taxi.Views;
using System.ComponentModel;

namespace Taxi.ViewModels
{
    public class AboutViewModel : BaseViewModel, INotifyPropertyChanged
    {
        
        //private string datum = LoginPage.Schichttag;
        //public string Datum
        //{
        //    get => datum;
        //    set => SetProperty(ref datum, value);
        //}
        public AboutViewModel()
        {
            Title = "Main";
        }
    }
}