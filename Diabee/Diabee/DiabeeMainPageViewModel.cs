using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Diabee
{
    public class DiabeeMainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string labelText = "Diabee";
        public string LabelText
        {
            get { return labelText; }
            set
            {
                labelText = value;
                OnPropertyChanged(nameof(LabelText));
            }
        }

        private DateTime currentDateText = DateTime.Now.Date;
        public DateTime CurrentSelectedDate
        {
            get { return currentDateText; }
            set
            {
                currentDateText = value;
                OnPropertyChanged(nameof(CurrentSelectedDate));
            }
        }

        public DateTime CurrentDate
        {
            get { return currentDateText; }
            set
            {
                currentDateText = value;
                OnPropertyChanged(nameof(CurrentDate));
            }
        }

        private DateTime minimumDateText = new DateTime(1970, 1, 1);
        public DateTime MinimumDate
        {
            get { return minimumDateText; }
            set
            {
                minimumDateText = value;
                OnPropertyChanged(nameof(MinimumDate));
            }
        }

        private TimeSpan currentTimeText = DateTime.Now.TimeOfDay;
        public TimeSpan CurrentTime
        {
            get { return currentTimeText; }
            set
            {
                currentTimeText = value;
                OnPropertyChanged(nameof(CurrentTime));
            }
        }

        private double sugarLevelBeforePoint;
        public double SugarLevelBeforePoint
        {
            get { return sugarLevelBeforePoint; }
            set
            {
                sugarLevelBeforePoint = value;
                OnPropertyChanged(nameof(SugarLevelBeforePoint));
                ChangeFinalGlucoseValue();
            }
        }

        private double sugarLevelAfterPoint;
        public double SugarLevelAfterPoint
        {
            get { return sugarLevelAfterPoint; }
            set
            {
                sugarLevelAfterPoint = value;
                OnPropertyChanged(nameof(SugarLevelAfterPoint));
                ChangeFinalGlucoseValue();
            }
        }

        private double finalGlucoseValue = 0f;
        public double FinalGlucoseValue
        {
            get { return finalGlucoseValue; }
            set
            {
                finalGlucoseValue = value;
                OnPropertyChanged(nameof(FinalGlucoseValue));
            }
        }

        private void ChangeFinalGlucoseValue()
        {
            int sugarValBeforePoint = (int)Math.Round(SugarLevelBeforePoint, MidpointRounding.AwayFromZero);
            double sugarValAfterPoint = Math.Round(SugarLevelAfterPoint, MidpointRounding.AwayFromZero); // 2dp this

            // Extract values after point if not 0
            if (sugarValAfterPoint != 0)
            {
                sugarValAfterPoint = Math.Truncate(sugarValAfterPoint);
            }

            string finalValInString = sugarValBeforePoint.ToString() + "." + sugarValAfterPoint.ToString();

            FinalGlucoseValue = double.Parse(finalValInString);
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
