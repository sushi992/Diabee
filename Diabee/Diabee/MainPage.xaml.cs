using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Diabee
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            Detail = new NavigationPage(new GlucoseReadingPage());

            IsPresented = false;
        }

        private void GlucoseReadingPage_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new GlucoseReadingPage());

            IsPresented = false;
        }

        private void WeightReadingPage_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new WeightReadingPage());

            IsPresented = false;
        }

        private void GlucoseHistoryPage_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new GlucoseHistoryPage());

            IsPresented = false;
        }

        private void WeightHistoryPage_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new WeightHistoryPage());

            IsPresented = false;
        }

        private void SettingsPage_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new SettingsPage());

            IsPresented = false;
        }
    }
}
