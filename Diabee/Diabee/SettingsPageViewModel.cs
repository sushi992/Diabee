using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Diabee
{
    public class SettingsPageViewModel
    {
        public INavigation Navigation { set; get; }
        public ICommand ExportPageCommand { private set; get; }

        public ICommand EraseDataCommand { private set; get; }

        public ICommand OpenPrivacyPolicyCommand { private set; get; }

        public ICommand OpenTermsConditionsCommand { private set; get; }

        public SettingsPageViewModel(INavigation navigator)
        {
            Navigation = navigator;
            ExportPageCommand = new Command(
            execute: () =>
            {
                NavigateToExportPage();
            });

            EraseDataCommand = new Command(
            execute: () =>
            {
                EraseData();
            });

            OpenPrivacyPolicyCommand = new Command(
            execute: () =>
            {
                OpenPrivacyPolicy();
            });

            OpenTermsConditionsCommand = new Command(
            execute: () =>
            {
                OpenTermsAndConditions();
            });
        }

        private async void NavigateToExportPage()
        {
            await Navigation.PushAsync(new ExportDataPage());
        }

        private void OpenPrivacyPolicy()
        {
            Launcher.OpenAsync(new Uri("http://www.medixapps.com/privacy"));
        }

        private void OpenTermsAndConditions()
        {
            Launcher.OpenAsync(new Uri("http://www.medixapps.com/termsandconditions"));
        }

        private async void EraseData()
        {
            var action = await App.Current.MainPage.DisplayAlert("Erase?", "Are you sure you want to erase all data?", "Yes", "No");
            if (action)
            {
                // erase all data...
                if (Constants.DeleteDataFiles())
                {
                    // deletion successful
                    DependencyService.Get<IMessage>().ShortAlert("Data deleted!");
                }
            }
        }
    }
}
