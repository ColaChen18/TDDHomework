using System;

namespace HomeWork
{
    public class Budget
    {
        public string YearMonth { get; set; }

        public int Amount { get; set; }

        private decimal GetDaysInMonth()
        {
            decimal daysInMonth = DateTime.DaysInMonth(
                DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null).Year,
                DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null).Month);
            return daysInMonth;
        }

        private decimal GetDailyAmount()
        {
            var dailyAmount = Amount / GetDaysInMonth();
            return dailyAmount;
        }

        private DateTime FirstDay()
        {
            var firstDay = new DateTime(DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null).Year,
                DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null).Month, 1);
            return firstDay;
        }

        private DateTime LastDay()
        {
            var lastDay = new DateTime(DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null).Year,
                DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null).Month,
                DateTime.DaysInMonth(DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null).Year,
                    DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null).Month));
            return lastDay;
        }

        public decimal GetPeriodAmount(Period period)
        {
            return period.OverlapDays(new Period(FirstDay(), LastDay())) * GetDailyAmount();
        }
    }
}