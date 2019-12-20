using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace Diabee
{
    public class GlucoseHistoryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<GlucoseHistoryDataModel> glucoseData;
        public ObservableCollection<GlucoseHistoryDataModel> GlucoseData 
        {
            get 
            { 
                return glucoseData; 
            }
            set 
            { 
                glucoseData = value; OnPropertyChanged(nameof(GlucoseData)); 
            }
        }

        public GlucoseHistoryViewModel()
        {
            GlucoseData = Constants.ReadGlucoseData(Constants.GlucoseFileName);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                       new PropertyChangedEventArgs(propertyName));
            }

        }
    }
}
