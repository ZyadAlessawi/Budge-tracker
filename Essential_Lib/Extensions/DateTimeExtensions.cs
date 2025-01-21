using Essential_Lib.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essential_Lib.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsDateGraterThanNow(this string eneteredDate)
        {
            string yy = string.Join("", eneteredDate.TakeLast(2));
            string MM = "0";
            string dd = "0";

            var remainingdigits = string.Join("", eneteredDate.SkipLast(2)).Length;

            if (remainingdigits == 1)
            {
                MM = "0" + string.Join("", eneteredDate.Take(1));
                dd = int.Parse(MM) > 0 ? DateTime.DaysInMonth(int.Parse(yy), int.Parse(MM)).ToString() : "00";
            }
            else if (remainingdigits == 2)
            {
                MM = string.Join("", eneteredDate.Take(2));
                if (int.Parse(MM) > 12)
                {
                    MM = "0" + string.Join("", eneteredDate.Skip(1).Take(1));
                    dd = "0" + string.Join("", eneteredDate.Take(1));
                }
                else
                {
                    dd = DateTime.DaysInMonth(int.Parse(yy), int.Parse(MM)).ToString();
                }
            }
            else if (remainingdigits == 3)
            {
                dd = string.Join("", eneteredDate.Take(2));
                MM = "0" + string.Join("", eneteredDate.Skip(2).Take(1));

                if (int.Parse(MM) > 0 && int.Parse(MM) < 13 && int.Parse(dd) > DateTime.DaysInMonth(int.Parse(yy), int.Parse(MM)))
                {
                    dd = DateTime.DaysInMonth(int.Parse(yy), int.Parse(MM)).ToString();
                }
            }
            else if (remainingdigits == 4)
            {
                dd = string.Join("", eneteredDate.Take(2));
                MM = string.Join("", eneteredDate.Skip(2).Take(2));

                if (int.Parse(dd) > DateTime.DaysInMonth(int.Parse(yy), int.Parse(MM)))
                {
                    dd = DateTime.DaysInMonth(int.Parse(yy), int.Parse(MM)).ToString();
                }
                if (int.Parse(MM) > 12)
                {
                    MM = "12";
                }
            }

            var enteredDate = int.Parse(yy.ToString() + MM.ToString() + dd.ToString());
            var date = int.Parse(DateTime.Today.ToString("yy/MM/dd").Replace("-", "").Replace("/", ""));

            if (enteredDate > date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static DateTime StringToDateConverter(this string? text)
        {
            if (text == null)
            {
                return DateTime.Now;
            }
            else
            {
                if (text.Length == 8 && DateTime.TryParseExact(text, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out DateTime result))
                {
                    return result;
                }
                else
                {
                    if (DateTime.TryParse(text, out DateTime result2))
                        return result2;
                    return DateTime.Now;
                }

            }
        }
        public static DateTime TicksToDateConverter(this long value)
        {
            try
            {

                return (new DateTime(value, DateTimeKind.Utc)).ToLocalTime();
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }
        }
        public static string TimeSpanFormater(this TimeSpan value)
        {
            var formats = new List<string>();
            if (value.Days > 0) formats.Add("dd");
            if (value.Hours > 0) formats.Add("hh");
            if (value.Minutes > 0) formats.Add("mm");
            if (value.Seconds > 0) formats.Add("ss");

            return string.Join(@"\:", formats);

        }
        public static string TimeSpanToStringWithAutoFormater(this TimeSpan value)
        {
            List<string> builder = [];
            if (value.Days > 0)
                builder.Add(" D   :   ");
            if (value.Hours > 0)
                builder.Add(" H   :   ");
            if (value.Minutes > 0)
                builder.Add(" M   :   ");
            if (value.Seconds > 0)
                builder.Add(" S");
            var s = value.ToString(value.TimeSpanFormater());
            var split = s.Split(":");
            StringBuilder builder1 = new();
            if (split.Length > 0)
            {
                for (var i = 0; i < split.Length; i++)
                {
                    builder1.Append(split[i]).Append(builder[i]);
                }
            }


            return builder1.ToString();

        }

        public static bool TicksIsBetweenDateRange(this long value, DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null)
                return false;
            var date = value.TicksToDateConverter();
            return date >= startDate && date <= endDate;
        }
        public static string DateTimeFromNowToMaxOneDayString(this DateTime value)
        {
            TimeSpan difference = DateTime.Now - value;
            return difference.TotalMinutes switch
            {
                < 5 => "Now".Localize(),
                < 60 and >= 5 => $"{Math.Round(difference.TotalMinutes, 0, MidpointRounding.AwayFromZero)} {"Minutes".Localize()} {"ago".Localize()}",
                < 1440 and >= 60 => $"{Math.Round(difference.TotalHours, 0, MidpointRounding.AwayFromZero)} {"Hours".Localize()} {"ago".Localize()}",
                < 2880 and >= 1440 => $"1 {"day".Localize()} {"ago".Localize()}",
                _ => value.ToString("g"),
            };
        }
        public static string DateTimeFromNowToMonthsYearsString(this DateTime value)
        {
            TimeSpan difference = DateTime.Now - value;
            var months = difference.TotalDays / 30.4;
            var reminder = Math.Round(months % 12, 0, MidpointRounding.AwayFromZero);
            return months switch
            {
                < 12 and >= 1 => $"{Math.Round(months, 0, MidpointRounding.AwayFromZero)} {"Months".Localize()}",
                < 1 and > 0 => $"1 {"Months".Localize()}",
                <= 0 => "Now".Localize(),
                _ => $"{(int)(months / 12)} {"years".Localize()}" + (reminder > 0 ? $" {"and".Localize()} {reminder} {"Months".Localize()}" : ""),
            };
        }

        public static TimeSpan StringToTimeSpanConverter(this string value)
        {
            if (value == null)
            {
                return TimeSpan.Zero;
            }
            else
            {
                if (TimeSpan.TryParseExact(value, "h:mm tt", CultureInfo.InvariantCulture, TimeSpanStyles.AssumeNegative, out TimeSpan timeSpan))
                {
                    return timeSpan;
                }
                else if (value.Split(":").Length == 2)
                {
                    return new TimeSpan(int.Parse(value.Split(":")[0]), int.Parse(value.Split(":")[1]), 0);
                }
                else
                {
                    return new TimeSpan();
                }
                //    try
                //    {
                //        timeSpan = TimeSpan.ParseExact(value, "h:mm tt", CultureInfo.InvariantCulture);
                //    }
                //    catch (FormatException)
                //    {
                //        timeSpan = TimeSpan.ParseExact(value, "h:mm", CultureInfo.InvariantCulture);
                //    }

                ////TimeSpan timeSpan = dateTime.TimeOfDay;
                //return timeSpan;   
            }
        }
        public static string StringToTimeSpanToStringConverter(this string value)
        {
            if (value == null)
            {
                return DateTime.Now.ToString("t");
            }
            else
            {
                TimeSpan timeSpan = TimeSpan.ParseExact(value, "h:mm tt", CultureInfo.InvariantCulture);
                return timeSpan.ToString("t");
            }
        }
    }

}
