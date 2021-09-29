using System;
using System.Linq;
using System.Globalization;
using Xamarin.Forms;
using Taxi.Data;
using Taxi.ViewModels;

namespace Taxi.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage(int month, int year)
        {
            string Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
            GetMonthlySum(month, year);
            InitializeComponent();
            Title = $"Kompletter Monat {Month}";
            BindingContext = new ItemDetailViewModel();
        }
        private async void GetMonthlySum(int GetMonth, int GetYear)
        {
            TaxiFahrpreisDatabase Database = await TaxiFahrpreisDatabase.Instance;
            string monthlydate;
            string Month = GetMonth.ToString();
            string Year = GetYear.ToString();
            if (Month.ToString().Length == 1)
            {
                monthlydate = ".0" + Month + "." + Year;
            }
            else
            {
                monthlydate = "." + Month + "." + Year;
            }
            var Fahrgeld = await Database.GetMonthlySum("FahrPreis", monthlydate);
            var Trinkgeld = await Database.GetMonthlySum("Trinkgeld", monthlydate);
            var Kredit = await Database.GetMonthlySum("Kredit", monthlydate);

            decimal FahrgeldSum = Fahrgeld.Select(g => g.Fahrpreis).Sum();
            decimal TrinkgelSum = Trinkgeld.Select(g => g.Trinkgeld).Sum();
            decimal KreditSum = Kredit.Select(g => g.Kredit).Sum();
            decimal NettoSum = (FahrgeldSum + KreditSum) * 0.4m;
            decimal NettoTKSum = NettoSum + TrinkgelSum;
            decimal ZuZahlen = FahrgeldSum - NettoSum;

            FahrgeldLabel.Text = FahrgeldSum.ToString();
            TrinkgeldLabel.Text = TrinkgelSum.ToString();
            KreditLabel.Text = KreditSum.ToString();
            Nettolabel.Text = NettoSum.ToString();
            Nettotklabel.Text = NettoTKSum.ToString();
            ZuZahlenLabel.Text = ZuZahlen.ToString();
        }
    }
}