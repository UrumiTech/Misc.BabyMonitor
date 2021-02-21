using System.ComponentModel;
using UrumiTech.BabyMonitor.ViewModels;
using Xamarin.Forms;

namespace UrumiTech.BabyMonitor.Views
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