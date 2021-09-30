using Xamarin.Plugin.Calendar.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Taxi.Models;
using Taxi.Data;
using Taxi.Views;

namespace Taxi.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ICommand TodayCommand => new Command(() => {
            Year = DateTime.Today.Year;
            Month = DateTime.Today.Month;
            SelectedDate = DateTime.Today;
        });
        public ICommand EventSelectedCommand => new Command(async (item) => await ExecuteEventSelectedCommand(item));
        public ICommand Navigate => new Command(async () => await DoNavigate());
        public ICommand PageAppearingCommand => new Command(async () => await GiveMeDbThings());
        public ItemsViewModel()
        {
            Task.Run(async () => { await GiveMeDbThings(); });
            Events = new EventCollection();
        }
        public async Task GiveMeDbThings()
        {
            TaxiFahrpreisDatabase Database = await TaxiFahrpreisDatabase.Instance;

            var getdates = await Database.GetDatesAsync();
            foreach (var item in getdates)
            {
                string schichttag = item.Schichttag;
                var Fahrgeld = await Database.GetFahrpreisAsync("FahrPreis", schichttag);
                var Trinkgeld = await Database.GetFahrpreisAsync("Trinkgeld", schichttag);
                var Kredit = await Database.GetFahrpreisAsync("Kredit", schichttag);

                decimal FahrgeldSum = Fahrgeld.Select(g => g.Fahrpreis).Sum();
                decimal TrinkgelSum = Trinkgeld.Select(g => g.Trinkgeld).Sum();
                decimal KreditSum = Kredit.Select(g => g.Kredit).Sum();

                Events[DateTime.Parse(schichttag)] = new List<EventModel>(GenerateEvents(1, FahrgeldSum, TrinkgelSum, KreditSum));
            }
        }



        private IEnumerable<EventModel> GenerateEvents(int count, decimal Fahrpreis, decimal Trinkgeld, decimal Kredit)
        {
            return Enumerable.Range(1, count).Select(x => new EventModel
            {
                Fahrpreis = Fahrpreis,
                Trinkgeld = Trinkgeld,
                Kredit = Kredit,
            });
        }

        public EventCollection Events { get; }

        private int _month = DateTime.Today.Month;
        public int Month
        {
            get => _month;
            set => SetProperty(ref _month, value);
        }

        public int _year = DateTime.Today.Year;
        public int Year
        {
            get => _year;
            set => SetProperty(ref _year, value);
        }

        private DateTime? _selectedDate = DateTime.Today;
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        private DateTime _minimumDate = new DateTime(2019, 4, 29);
        public DateTime MinimumDate
        {
            get => _minimumDate;
            set => SetProperty(ref _minimumDate, value);
        }

        private DateTime _maximumDate = DateTime.Today.AddMonths(5);
        public DateTime MaximumDate
        {
            get => _maximumDate;
            set => SetProperty(ref _maximumDate, value);
        }

        private async Task ExecuteEventSelectedCommand(object item)
        {
            if (item is EventModel eventModel)
            {
                decimal Money = (eventModel.Fahrpreis + eventModel.Kredit) * 0.4m;

                string day = SelectedDate.ToString().Substring(0, 10);
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new EventDetailPage(day, Money));
            }
        }
        private async Task DoNavigate()
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new ItemDetailPage(Month, Year));
        }
    }
}
