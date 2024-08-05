using System;
using System.Globalization;

namespace time_reservation.Helper
{
 

    public static class PersianDateHelper
    {
        public static string ToPersianDateString(DateTime date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            int year = persianCalendar.GetYear(date);
            int month = persianCalendar.GetMonth(date);
            int day = persianCalendar.GetDayOfMonth(date);

            return $"{year}/{month:D2}/{day:D2}";
        }

        public static string GetPersianDayOfWeek(DateTime date)
        {
            string[] persianDays = { "شنبه", "یک‌شنبه", "دوشنبه", "سه‌شنبه", "چهارشنبه", "پنج‌شنبه", "جمعه" };
            return persianDays[(int)date.DayOfWeek];
        }
    }

}
