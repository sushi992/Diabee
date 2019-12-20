﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Diabee
{
    public class TimeSpanToTimeStringConverter : IValueConverter
    {
        public static TimeSpanToTimeStringConverter Instance = new TimeSpanToTimeStringConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan timeSpan = (TimeSpan)value;
            DateTime dateTime = DateTime.MinValue + timeSpan;

            // acknowledgement: based on some code from 
            // http://stackoverflow.com/questions/1292246/why-doesnt-datetime-toshorttimestring-respect-the-short-time-format-in-regio

            DateTimeFormatInfo dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
            string shortTimePattern
                = dateTimeFormat.LongTimePattern.Replace(":ss", String.Empty).Replace(":s", String.Empty);

            var timeToDisplay = dateTime.ToString(shortTimePattern);

            if (timeToDisplay.Contains("AM") || timeToDisplay.Contains("PM"))
            {
                return timeToDisplay;
            }

            if (timeToDisplay.StartsWith("0"))
            {
                timeToDisplay += "AM";
            }
            else
            {
                timeToDisplay += "PM";
            }

            return timeToDisplay;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("TimeSpanToTimeStringConverter.ConvertBack");
        }

    }
}
