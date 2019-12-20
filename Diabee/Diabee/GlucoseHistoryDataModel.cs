using System;

namespace Diabee
{
    public class GlucoseHistoryDataModel
    {
        public string HistoryDate { get; set; }
        public TimeSpan HistoryTime { get; set; }
        public string GlucoseValue { get; set; }
    }
}
