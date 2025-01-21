using Essential_Lib.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essential_Lib.Converters
{
    public class DateTimeToTicksConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return DateTime.UtcNow.Ticks;
            }
            if (value is DateTime dt)
                return dt.ToFileTime();

            else
                return DateTime.UtcNow.Ticks;

        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is long ticks)
            {
                return ticks.TicksToDateConverter();
            }
            return DateTime.Now;
        }
    }
    public class StringToDateConverter : IValueConverter
    {
        bool IsTicks = false;
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return DateTime.Now;
            }
            else if (value is string text)
                return text.StringToDateConverter();

            else if (value is long ticks && ticks > 0)
            {
                IsTicks = true;
                return $"{parameter}" == "TS" ? TimeSpan.FromTicks(ticks).TimeSpanToStringWithAutoFormater() : ticks.TicksToDateConverter();
            }
            else
                return DateTime.Now;

        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is DateTime dt)
            {
                if (IsTicks)
                {
                    return $"{parameter}" == "TS" ? Binding.DoNothing : (dt).Ticks;
                }
                var date = dt;

                return date.ToString("yyyyMMdd");
            }
            return Binding.DoNothing;
        }
    }
    public class StringToTimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return TimeSpan.Zero;
            }
            else
            {
                var text = (string)value;
                if (TimeSpan.TryParseExact(text, "h:mm tt", CultureInfo.InvariantCulture, TimeSpanStyles.AssumeNegative, out TimeSpan timeSpan))
                {
                    return timeSpan;
                }
                else if (TimeSpan.TryParseExact(text, "h:mm", CultureInfo.InvariantCulture, TimeSpanStyles.AssumeNegative, out TimeSpan timeSpan2))
                {
                    return timeSpan2;
                }
                else
                {
                    return new TimeSpan();
                }


                //TimeSpan timeSpan;
                //try
                //{
                //    timeSpan = TimeSpan.ParseExact(text, "h:mm tt", CultureInfo.InvariantCulture);
                //}
                //catch (FormatException)
                //{
                //    timeSpan = TimeSpan.ParseExact(text, "h:mm", CultureInfo.InvariantCulture);
                //}

                ////TimeSpan timeSpan = dateTime.TimeOfDay;
                //return timeSpan;
                ////DateTime dateTime = DateTime.ParseExact(text, "h:mm tt", CultureInfo.InvariantCulture);
                ////TimeSpan timeSpan = dateTime.TimeOfDay;
                ////return timeSpan;   
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
            //return value;
        }
    }


}

