using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Diabee
{
    public class DiabeeMainPageViewModel : INotifyPropertyChanged
    {
        public DiabeeMainPageViewModel()
        {
            SubmitGlucoseCommand = new Command(
                execute: () =>
                {
                    SubmitGlucoseReading();
                });

            SubmitWeightCommand = new Command(
            execute: () =>
            {
                SubmitWeightReading();
            });
        }

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

        private double weightLevelBeforePoint;
        public double WeightLevelBeforePoint
        {
            get { return weightLevelBeforePoint; }
            set
            {
                weightLevelBeforePoint = value;
                OnPropertyChanged(nameof(WeightLevelBeforePoint));
                ChangeFinalWeightValue();
            }
        }

        private double weightLevelAfterPoint;
        public double WeightLevelAfterPoint
        {
            get { return weightLevelAfterPoint; }
            set
            {
                weightLevelAfterPoint = value;
                OnPropertyChanged(nameof(WeightLevelAfterPoint));
                ChangeFinalWeightValue();
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

        private double finalWeightValue = 0f;
        public double FinalWeightValue
        {
            get { return finalWeightValue; }
            set
            {
                finalWeightValue = value;
                OnPropertyChanged(nameof(FinalWeightValue));
            }
        }

        public ICommand SubmitGlucoseCommand { private set; get; }
        public ICommand SubmitWeightCommand { private set; get; }

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

        private void ChangeFinalWeightValue()
        {
            int weightValBeforePoint = (int)Math.Round(WeightLevelBeforePoint, MidpointRounding.AwayFromZero);
            double weightValAfterPoint = Math.Round(WeightLevelAfterPoint, MidpointRounding.AwayFromZero); // 2dp this

            // Extract values after point if not 0
            if (weightValAfterPoint != 0)
            {
                weightValAfterPoint = Math.Truncate(weightValAfterPoint);
            }

            string finalValInString = weightValBeforePoint.ToString() + "." + weightValAfterPoint.ToString();

            FinalWeightValue = double.Parse(finalValInString);
        }

        private void SubmitGlucoseReading()
        {
            Task.Run(async () => await SaveData(FinalGlucoseValue.ToString(), Constants.GlucoseFileName));
            DependencyService.Get<IMessage>().ShortAlert("Saved!");
        }

        private void SubmitWeightReading()
        {
            Task.Run(async () => await SaveData(FinalWeightValue.ToString(), Constants.WeightFileName));
            DependencyService.Get<IMessage>().ShortAlert("Saved!");
        }

        public async Task SaveData(string valueToSave, string filename)
        {
            valueToSave = CurrentDate.ToLongDateString() + "," +
                CurrentTime.Hours + ":" + CurrentTime.Minutes + ":" + CurrentTime.Seconds + "," +
                valueToSave;
            try
            {
                // Can't actually see the file generated as it's internal but doesn't matter...
                var backingFile = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal), filename);
                using (var writer = File.AppendText(backingFile))
                {
                    await writer.WriteLineAsync(valueToSave);
                }
            }
            catch
            {
                return;
            }
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
