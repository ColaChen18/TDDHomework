using System;

namespace HomeWork
{
    public class Budget
    {
        public string YearMonth { get; set; }

        public int Amount { get; set; }

        public int QueryDaysInPeriod(Period period)
        {
            var date = DateTime.ParseExact(this.YearMonth + "01", "yyyyMMdd", null);
            var budgetFirstDay = new DateTime(date.Year, date.Month, 1);
            var budgetLastDay = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
            var periodEnd = period.End <= budgetLastDay ? period.End : budgetLastDay;
            var periodStart = budgetFirstDay <= period.Start ? period.Start : budgetFirstDay;
            return (periodEnd - periodStart).Days + 1;
        }
    }
}