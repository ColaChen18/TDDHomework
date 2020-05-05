using System;

namespace HomeWork
{
    public class Period
    {
        public Period(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public int OverlapDays(Budget budget)
        {
            var date = DateTime.ParseExact(budget.YearMonth + "01", "yyyyMMdd", null);
            var budgetFirstDay = new DateTime(date.Year, date.Month, 1);
            var budgetLastDay = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
            var periodEnd = End <= budgetLastDay ? End : budgetLastDay;
            var periodStart = budgetFirstDay <= Start ? Start : budgetFirstDay;
            var queryDaysInPeriod = (periodEnd - periodStart).Days + 1;
            return queryDaysInPeriod;
        }
    }
}