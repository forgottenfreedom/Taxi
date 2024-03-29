﻿using System;
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
            Preferences.Set("Login", true);
            Preferences.Set("CurrentDatum", Schichttag);
            await DisplayAlert("Anmeldung", "Erfolgreich Angemeldet!", "OK");
        }
        async void LogoutButton_Clicked(object sender, EventArgs e)
        {
            Preferences.Remove("Login");
            Preferences.Remove("CurrentDatum");
            await DisplayAlert("Abmeldung", "Erfolgreich Abgemeldet!", "OK");
        }
    }
}