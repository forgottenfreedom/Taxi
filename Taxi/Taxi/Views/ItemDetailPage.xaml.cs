using System.ComponentModel;
using Taxi.ViewModels;
using Xamarin.Forms;

namespace Taxi.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}