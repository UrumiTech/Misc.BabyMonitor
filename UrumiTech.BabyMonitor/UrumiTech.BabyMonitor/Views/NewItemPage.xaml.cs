using System;
using System.Collections.Generic;
using System.ComponentModel;
using UrumiTech.BabyMonitor.Models;
using UrumiTech.BabyMonitor.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UrumiTech.BabyMonitor.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}