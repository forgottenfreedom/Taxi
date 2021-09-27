using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Taxi.Data;
using Taxi.Models;
using System.Linq;
using System.Collections.Generic;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace Taxi.Views
{
    public partial class AboutPage : ContentPage, INotifyPropertyChanged
    {
        public string Datum;
        public AboutPage()
        {
            Datum = Preferences.Get("Schichttag", "Nicht Angemeldet!");
            UpdateList();
            InitializeComponent();
        }
        async void NormalButton_Clicked(object sender, EventArgs e)
        {
            string Fahrpreis = null;
            string Gesamtgeld = await DisplayPromptAsync(title: "Gesamt", message: "Geld erhalten?", keyboard: Keyboard.Telephone);
            if (Gesamtgeld != null)
            {
                Fahrpreis = await DisplayPromptAsync(title: "Fahrpreis", message: "Fahrpreis?", keyboard: Keyboard.Telephone);
                if (Fahrpreis == null) return;
            }
            else return;
            
            decimal Trinkgeld = decimal.Parse(Gesamtgeld) - decimal.Parse(Fahrpreis);
            TaxiFahrpreisDatabase Database = await TaxiFahrpreisDatabase.Instance;
            await Database.SaveItemAsync(new TaxiFahrpreis
            {
                Fahrpreis = decimal.Parse(Fahrpreis),
                Trinkgeld = Trinkgeld,
                Kredit = 0,
                Schichttag = Datum,
            }); ;
            UpdateList();
            await this.DisplayToastAsync("Saved!", 4000);
        }
        async void KreditButton_Clicked(object sender, EventArgs e)
        {
            string KreditBtn = await DisplayActionSheet("ActionSheet: Was machen?", "Cancel", null, "Dialyse", "AST");
            if (KreditBtn == "Dialyse")
            {
                string Kredit = await DisplayPromptAsync(title: "Kredit", message: "Kredit?", keyboard: Keyboard.Telephone);
                TaxiFahrpreisDatabase Database = await TaxiFahrpreisDatabase.Instance;
                await Database.SaveItemAsync(new TaxiFahrpreis
                {
                    Fahrpreis = 0,
                    Trinkgeld = 0,
                    Kredit = decimal.Parse(Kredit),
                    Schichttag = Datum,
                });
            }
            if (KreditBtn == "AST")
            {
                string ASTTaxameter = await DisplayPromptAsync(title: "AST Taxameter", message: "Wieviel?", keyboard: Keyboard.Telephone);
                if (ASTTaxameter != null)
                {
                    string ASTFahrpreis = await DisplayActionSheet("ActionSheet: Geld erhalten?", "Ja", "Nein");
                    if (ASTFahrpreis == "Ja")
                    {
                        string ASTPauschale = await DisplayPromptAsync(title: "Fahrpreis", message: "Wieviel?", keyboard: Keyboard.Telephone);
                        decimal ASTKredit = decimal.Parse(ASTTaxameter) - decimal.Parse(ASTPauschale);
                        TaxiFahrpreisDatabase Database = await TaxiFahrpreisDatabase.Instance;
                        await Database.SaveItemAsync(new TaxiFahrpreis
                        {
                            Fahrpreis = decimal.Parse(ASTPauschale),
                            Trinkgeld = 0,
                            Kredit = ASTKredit,
                            Schichttag = Datum,
                        });
                    }
                    if (ASTFahrpreis == "Nein")
                    {
                        TaxiFahrpreisDatabase Database = await TaxiFahrpreisDatabase.Instance;
                        await Database.SaveItemAsync(new TaxiFahrpreis
                        {
                            Fahrpreis = 0,
                            Trinkgeld = 0,
                            Kredit = decimal.Parse(ASTTaxameter),
                            Schichttag = Datum,
                        });
                    }
                    if (ASTFahrpreis == null) return;
                }
                else return;
            }
            if (KreditBtn == null)
            {
                return;
            }
            UpdateList();
            await this.DisplayToastAsync("Saved!", 4000);
        }
        async void LeerButton_Clicked(object sender, EventArgs e)
        {
            string grattler = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Email", "Twitter", "Facebook");
            UpdateList();
        }
        public async void UpdateList()
        {
            bool Angemeldet = Preferences.Get("Login", false);
            DatumLabel.Text = Datum;

            if (Angemeldet)
            {
                TaxiFahrpreisDatabase Database = await TaxiFahrpreisDatabase.Instance;
                var Fahrgeld = await Database.GetFahrpreisAsync("FahrPreis", Datum);
                var Trinkgeld = await Database.GetFahrpreisAsync("Trinkgeld", Datum);
                var Kredit = await Database.GetFahrpreisAsync("Kredit", Datum);

                decimal FahrgeldSum = Fahrgeld.Select(g => g.Fahrpreis).Sum();
                decimal TrinkgelSum = Trinkgeld.Select(g => g.Trinkgeld).Sum();
                decimal KreditSum = Kredit.Select(g => g.Kredit).Sum();

                FahrgeldLabel.Text = FahrgeldSum.ToString();
                TrinkgeldLabel.Text = TrinkgelSum.ToString();
                KreditLabel.Text = KreditSum.ToString();
            }
            else return;

        }
        protected override void OnAppearing()
        {
            Datum = Preferences.Get("Schichttag", "Nicht Angemeldet!");
            Console.WriteLine(Datum);
            UpdateList();
        }
    }
}