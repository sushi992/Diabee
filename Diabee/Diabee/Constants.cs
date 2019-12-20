using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Diabee
{
    public static class Constants
    {
        public static readonly string WeightFileName = "WeightData.txt";
        public static readonly string GlucoseFileName = "GlucoseData.txt";

        public static ObservableCollection<GlucoseHistoryDataModel> ReadGlucoseData(string fileName)
        {
            ObservableCollection<GlucoseHistoryDataModel> glucoseData = new ObservableCollection<GlucoseHistoryDataModel>();

            try
            {
                var backingFile = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.Personal), fileName);

                if (backingFile == null || !File.Exists(backingFile))
                {
                    return new ObservableCollection<GlucoseHistoryDataModel>();
                }
                string FileData = string.Empty;
                using (var reader = new StreamReader(backingFile, true))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        FileData = line;
                        var time = FileData.Split(',').Reverse().ElementAt(1);
                        var glucoseVal = FileData.Split(',').Last();
                        var date = FileData.Split(new[] { "," + time }, StringSplitOptions.None)[0];
                        GlucoseHistoryDataModel glucoseHistoryDataModel = new GlucoseHistoryDataModel()
                        {
                            HistoryDate = DateTime.Parse(date).ToLongDateString(),
                            HistoryTime = TimeSpan.Parse(time),
                            GlucoseValue = glucoseVal
                        };

                        glucoseData.Add(glucoseHistoryDataModel);
                    }
                }

                // Sort glucose data by date and time before returning
                var sortedByDate = glucoseData
                    .OrderByDescending(x => Convert.ToDateTime(x.HistoryDate))
                    .ThenByDescending(y => y.HistoryTime)
                    .Reverse();
                ObservableCollection<GlucoseHistoryDataModel> sortedObsvCollection = new 
                    ObservableCollection<GlucoseHistoryDataModel>(sortedByDate);

                return sortedObsvCollection;
            }
            catch
            {
                return new ObservableCollection<GlucoseHistoryDataModel>();
            }
        }

        public static ObservableCollection<WeightHistoryDataModel> ReadWeightData(string fileName)
        {
            ObservableCollection<WeightHistoryDataModel> weightData = new ObservableCollection<WeightHistoryDataModel>();
            try
            {
                var backingFile = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.Personal), fileName);

                if (backingFile == null || !File.Exists(backingFile))
                {
                    return new ObservableCollection<WeightHistoryDataModel>();
                }
                string FileData = string.Empty;
                using (var reader = new StreamReader(backingFile, true))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        FileData = line;
                        var time = FileData.Split(',').Reverse().ElementAt(1);
                        var weightVal = FileData.Split(',').Last();
                        var date = FileData.Split(new[] { "," + time }, StringSplitOptions.None)[0];
                        WeightHistoryDataModel weightHistoryDataModel = new WeightHistoryDataModel()
                        {
                            HistoryDate = DateTime.Parse(date).ToLongDateString(),
                            HistoryTime = TimeSpan.Parse(time),
                            WeightValue = weightVal
                        };

                        weightData.Add(weightHistoryDataModel);
                    }
                }

                // Sort weight data by date and time before returning
                var sortedByDate = weightData
                .OrderByDescending(x => Convert.ToDateTime(x.HistoryDate))
                .ThenByDescending(y => y.HistoryTime)
                .Reverse();
                ObservableCollection<WeightHistoryDataModel> sortedObsvCollection = new
                    ObservableCollection<WeightHistoryDataModel>(sortedByDate);

                return sortedObsvCollection;
            }
            catch
            {
                return new ObservableCollection<WeightHistoryDataModel>();
            }
        }

        public static bool DeleteDataFiles()
        {
            try
            {
                // Glucose data
                var glucoseFile = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.Personal), GlucoseFileName);

                if (glucoseFile != null && File.Exists(glucoseFile))
                {
                    // delete the file
                    File.Delete(glucoseFile);
                }

                // Weight data
                var weightFile = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.Personal), WeightFileName);

                if (weightFile != null && File.Exists(weightFile))
                {
                    // delete the file
                    File.Delete(weightFile);
                }

                return true;
            }
            catch
            {
                // some error happened... return...
                return false;
            }
        }
    }
}
