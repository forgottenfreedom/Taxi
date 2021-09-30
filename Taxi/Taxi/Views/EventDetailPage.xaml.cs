using System;
using System.Linq;
using System.Globalization;
using Xamarin.Forms;
using Taxi.Data;
using Taxi.ViewModels;

namespace Taxi.Views
{
    public partial class EventDetailPage : ContentPage
    {
        public EventDetailPage(string date, decimal money)
        {
            GetMonthlySum(date);
            InitializeComponent();
            Title = "Details";
            BindingContext = new EventDetailViewModel();
            Header.Text = "Netto:" + money.ToString();
        }
        private async void GetMonthlySum(string date)
        {
            TaxiFahrpreisDatabase Database = await TaxiFahrpreisDatabase.Instance;

            var test = await Database.GetListStuff(date);
           Gratttler.ItemsSource = await Database.GetListStuff(date);
        }
    }
}