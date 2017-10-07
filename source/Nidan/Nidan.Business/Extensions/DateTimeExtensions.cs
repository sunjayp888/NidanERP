using System;
using System.Collections.Generic;

namespace Nidan.Business.Extensions
{
    public static class DateTimeExtensions
    {
        public static IEnumerable<DateTime> RangeTo(this DateTime from, DateTime to, Func<DateTime, DateTime> step = null)
        {
            if (step == null)
                step = x => x.AddDays(1);

            while (from <= to)
            {
                yield return from;
                from = step(from);
            }
        }

        public static IEnumerable<DateTime> RangeFrom(this DateTime to, DateTime from, Func<DateTime, DateTime> step = null)
        {
            return from.RangeTo(to, step);
        }

        public static string FiscalYear(this DateTime now)
        {
            return string.Concat(now.Year, "-", now.AddYears(1).Year);
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }

        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public static IEnumerable<DateTime> EachMonth(DateTime from, DateTime thru)
        {
            for (var month = from.Date; month.Date <= thru.Date || month.Month == thru.Month; month = month.AddMonths(1))
                yield return month;
        }

        public static IEnumerable<DateTime> EachDayTo(this DateTime dateFrom, DateTime dateTo)
        {
            return EachDay(dateFrom, dateTo);
        }

        public static IEnumerable<DateTime> EachMonthTo(this DateTime dateFrom, DateTime dateTo)
        {
            return EachMonth(dateFrom, dateTo);
        }
    }
}