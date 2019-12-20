using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diabee
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeightHistoryPage : ContentPage
    {
        public WeightHistoryPage()
        {
            InitializeComponent();
            BindingContext = new WeightHistoryViewModel();
        }
    }
}