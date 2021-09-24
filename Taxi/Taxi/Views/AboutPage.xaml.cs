using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Taxi.Data;
using Taxi.Models;
using System.Linq;
using System.Collections.Generic;
using Xamarin.CommunityToolkit.Extensions;
using System.Threading.Tasks;

namespace Taxi.Views
{
    public partial class AboutPage : ContentPage
    {
        public string getDatum;
        public AboutPage()
        {
            InitializeComponent();
            UpdateList();
            getDatum = LoginPage.Schichttag;
        }
        async void NormalButton_Clicked(object sender, EventArgs e)
        {
            string Fahrpreis = null;
            string Gesamtgeld = await DisplayPromptAsync(title: "Gesamt", message: "Geld erhalten?", keyboard: Keyboard.Telephone);
            if (Gesamtgeld != null)
            {
                Fahrpreis = await DisplayPromptAsync(title: "Fahrpreis", message: "Fahrpreis?", keyboard: Keyboard.Telephone);
            }
            decimal Trinkgeld = decimal.Parse(Gesamtgeld) - decimal.Parse(Fahrpreis);
            TaxiFahrpreisDatabase Database = await TaxiFahrpreisDatabase.Instance;
            await Database.SaveItemAsync(new TaxiFahrpreis
            {
                Fahrpreis = decimal.Parse(Fahrpreis),
                Trinkgeld = Trinkgeld
            });
            UpdateList();
            await this.DisplayToastAsync("Saved!", 4000);
            //collectionView.ItemsSource = grattler;
        }
        async void KreditButton_Clicked(object sender, EventArgs e)
        {
            string Kredit = await DisplayPromptAsync(title: "Kredit", message: "Kredit?", keyboard: Keyboard.Telephone);
            TaxiFahrpreisDatabase Database = await TaxiFahrpreisDatabase.Instance;
            await Database.SaveItemAsync(new TaxiFahrpreis
            {
                Kredit = decimal.Parse(Kredit)
            });
            UpdateList();
        }
        async void LeerButton_Clicked(object sender, EventArgs e)
        {
            string grattler = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Email", "Twitter", "Facebook");
            getDatum = grattler;
            UpdateList();
        }
        public async void UpdateList()
        {
            TaxiFahrpreisDatabase Database = await TaxiFahrpreisDatabase.Instance;
            var Fahrgeld = await Database.GetFahrpreisAsync("FahrPreis");
            var Trinkgeld = await Database.GetFahrpreisAsync("Trinkgeld");
            var Kredit = await Database.GetFahrpreisAsync("Kredit");

            decimal FahrgeldSum = Fahrgeld.Select(g => g.Fahrpreis).Sum();
            decimal TrinkgelSum = Trinkgeld.Select(g => g.Trinkgeld).Sum();
            decimal KreditSum = Kredit.Select(g => g.Kredit).Sum();

            FahrgeldLabel.Text = FahrgeldSum.ToString();
            TrinkgeldLabel.Text = TrinkgelSum.ToString();
            KreditLabel.Text = KreditSum.ToString();
            Datum.Text = getDatum;

        }
    }
}