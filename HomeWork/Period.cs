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

        private DateTime Start { get; }
        private DateTime End { get; }

        public int OverlapDays(Period period)
        {
            var periodEnd = End <= period.End ? End : period.End;
            var periodStart = period.Start <= Start ? Start : period.Start;

            return periodEnd >= periodStart ? (periodEnd - periodStart).Days + 1 : 0;
        }
    }
}