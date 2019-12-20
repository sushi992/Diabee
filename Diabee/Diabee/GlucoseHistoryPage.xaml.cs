using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diabee
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GlucoseHistoryPage : ContentPage
    {
        public GlucoseHistoryPage()
        {
            InitializeComponent(); 
            BindingContext = new GlucoseHistoryViewModel();
        }
    }
}