using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diabee
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GlucoseReadingPage : ContentPage
    {
        public GlucoseReadingPage()
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