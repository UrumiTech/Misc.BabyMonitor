using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UrumiTech.BabyMonitor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraPage : ContentPage
    {
        public delegate void PhotoResultEventHandler(PhotoResultEventArgs result);
        public event PhotoResultEventHandler OnPhotoResult;
        public CameraPage()
        {
            InitializeComponent();
        }
    }
}