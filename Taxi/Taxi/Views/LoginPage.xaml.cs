using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Taxi.Data;
using Taxi.Models;

namespace Taxi.Views
{

    public partial class LoginPage : ContentPage, INotifyPropertyChanged
    {
        public static string Schichttag = "Nicht Angemeldet!";
        public LoginPage()
        {
            InitializeComponent();
        }
        async void LoginButton_Clicked(object sender, EventArgs e)
        {
            DateTime thisDay = DateTime.Today;
            Schichttag = thisDay.ToString("d");
            TaxiFahrpreisDatabase Database = await TaxiFahrpreisDatabase.Instance;
            await Database.SaveItemAsync(new TaxiFahrpreis
            {
                Schichttag = Schichttag
            });
            collectionView.ItemsSource = await Database.GetTrinkgeldAsync(Schichttag);
            Console.WriteLine(Schichttag);
        }
    }
}