using System;
using System.Collections.Generic;
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

    public partial class LoginPage : ContentPage
    {
        public static string Schichttag;
        public LoginPage()
        {
            InitializeComponent();
            Schichttag = "???";

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
            AboutPage ap = new AboutPage();
            ap.UpdateList();
        }
    }
}