using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diabee
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeightReadingPage : ContentPage
    {
        public WeightReadingPage()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.Android)
            {
                Padding = new Thickness(10, 20, 0, 0);
            }

            BindingContext = new DiabeeMainPageViewModel();
        }
    }
}