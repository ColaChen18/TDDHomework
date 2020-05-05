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

        public int OverlapDays(Budget budget, Period period)
        {
            var budgetFirstDay = budget.BudgetFirstDay();
            var budgetLastDay = budget.BudgetLastDay();
            var periodEnd = End <= period.End ? End : period.End;
            var periodStart = period.Start <= Start ? Start : period.Start;
            var queryDaysInPeriod = (periodEnd - periodStart).Days + 1;
            return queryDaysInPeriod;
        }
    }
}