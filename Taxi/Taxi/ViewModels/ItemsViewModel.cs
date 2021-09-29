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
        public List<string> dates = new List<string>();

        public ItemsViewModel()
        {
            Task.Run(async () => { await GiveMeDbThings(); });
            foreach (var gratttler in dates)
            {
                Console.WriteLine(gratttler);
                Console.WriteLine("Test");
            };
            Console.WriteLine("this should be below");
            Console.WriteLine(dates);
            //var task = Grattler();
            //List<string> geier = task.Result;
            //foreach(var item in geier)
            //{
            //    Events.Add(DateTime.Now.AddDays(-1), new List<EventModel>(GenerateEvents(1, "Test")));
            //}
            // testing all kinds of adding events
            // when initializing collection
            Events = new EventCollection();
            //{
            //    [DateTime.Now.AddDays(-3)] = new List<EventModel>(GenerateEvents(10, "Cool")),
            //};

            // with add method
            //Events.Add(DateTime.Now.AddDays(-1), new List<EventModel>(GenerateEvents(5, "Cool")));

            // with indexer
            Events[DateTime.Parse("27.09.2021")] = new List<EventModel>(GenerateEvents(1, "Boring"));
        }

        private async Task GiveMeDbThings()
        {
            Console.WriteLine("Was Passiert mit mir?");
            TaxiFahrpreisDatabase Database = await TaxiFahrpreisDatabase.Instance;
            var getdates = await Database.GetDatesAsync();
            foreach (var item in getdates)
            {
                string schichttag = item.Schichttag;
                dates.Add(schichttag);
                Console.WriteLine("this should be on top");
                Console.WriteLine(schichttag);
            }
        }



        private IEnumerable<EventModel> GenerateEvents(int count, string name)
        {
            return Enumerable.Range(1, count).Select(x => new EventModel
            {
                Name = $"{name} event",
                Description = $"This is {name} event{x}'s description!"
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
                await App.Current.MainPage.DisplayAlert(eventModel.Name, eventModel.Description, "Ok");
            }
        }
    }
}
