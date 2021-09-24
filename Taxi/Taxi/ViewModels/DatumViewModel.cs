using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Taxi.Views;
using System.ComponentModel;

namespace Taxi.ViewModels
{
    public class DatumViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _datum = string.Empty;
        public DatumViewModel()
        {

        }
        public string Datum
        {
            get
            {
                return _datum;
            }
            set
            {
                _datum = value;
                OnPropertyChanged("Datum");
            }
        }
    }
}