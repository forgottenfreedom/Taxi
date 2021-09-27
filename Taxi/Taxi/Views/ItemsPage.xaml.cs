using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Models;
using Taxi.ViewModels;
using Taxi.Views;
using Taxi.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Taxi.Views
{
    public partial class ItemsPage : ContentPage
    {
        public ItemsPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            TaxiFahrpreisDatabase Database = await TaxiFahrpreisDatabase.Instance;
            var _dates = await Database.GetDatesAsync();
            foreach (var item in _dates)
            {
                Console.WriteLine(item);
            }
        }

    }
}