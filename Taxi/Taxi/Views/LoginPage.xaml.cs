using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Taxi.Data;
using Taxi.Models;

namespace Taxi.Views
{

    public partial class LoginPage : ContentPage, INotifyPropertyChanged
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        async void LoginButton_Clicked(object sender, EventArgs e)
        {
            DateTime thisDay = DateTime.Today;
            string Schichttag = thisDay.ToString("d");
            TaxiFahrpreisDatabase Database = await TaxiFahrpreisDatabase.Instance;
            await Database.SaveItemAsync(new TaxiFahrpreis
            {
                Schichttag = Schichttag
            });
            collectionView.ItemsSource = await Database.GetTrinkgeldAsync(Schichttag);
            Preferences.Set("Login", true);
            Preferences.Set("Schichttag", Schichttag);
        }
        async void LogoutButton_Clicked(object sender, EventArgs e)
        {
            Preferences.Remove("Login");
            Preferences.Remove("Schichttag");
            await DisplayAlert("Abmeldung", "Erfolgreich Abgemeldet!", "OK");
        }
    }
}