using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diabee
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExportDataPage : ContentPage
    {
        public ExportDataPage()
        {
            InitializeComponent();

            BindingContext = new ExportDataPageViewModel();
        }
    }
}