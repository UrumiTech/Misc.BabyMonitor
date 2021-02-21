using System;
using System.Collections.Generic;
using UrumiTech.BabyMonitor.ViewModels;
using UrumiTech.BabyMonitor.Views;
using Xamarin.Forms;

namespace UrumiTech.BabyMonitor
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
