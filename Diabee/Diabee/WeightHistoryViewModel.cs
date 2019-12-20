using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace Diabee
{
    public class WeightHistoryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<WeightHistoryDataModel> weightData;
        public ObservableCollection<WeightHistoryDataModel> WeightData
        {
            get
            {
                return weightData;
            }
            set
            {
                weightData = value; OnPropertyChanged(nameof(WeightData));
            }
        }

        public WeightHistoryViewModel()
        {
            WeightData = Constants.ReadWeightData(Constants.WeightFileName);
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
