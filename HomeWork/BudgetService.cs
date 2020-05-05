using System;
using System.Linq;

namespace HomeWork
{
    public class BudgetService
    {
        private readonly IBudgetRepo _budgetRepo;

        public BudgetService(IBudgetRepo budgetRepo)
        {
            _budgetRepo = budgetRepo;
        }

        public decimal Query(DateTime start, DateTime end)
        {
            if (start > end)
                return 0;

            var result = 0m;
            for (var i = 0; i < end.Month - start.Month + 1; i++)
            {
                var month = start.AddMonths(i);
                var budget = GetBudget(month);
                var queryDaysInPeriod = QueryDaysInPeriod(budget, new Period(start, end));
                result +=  queryDaysInPeriod * budget.GetDailyAmount();
            }

            return result;
        }

        private static int QueryDaysInPeriod(Budget budget, Period period)
        {
            var date = DateTime.ParseExact(budget.YearMonth + "01", "yyyyMMdd", null);
            var budgetFirstDay = new DateTime(date.Year, date.Month, 1);
            var budgetLastDay = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
            var periodEnd = period.End <= budgetLastDay ? period.End : budgetLastDay;
            var periodStart = budgetFirstDay <= period.Start ? period.Start : budgetFirstDay;
            var queryDaysInPeriod = (periodEnd - periodStart).Days + 1;
            return queryDaysInPeriod;
        }

        private Budget GetBudget(DateTime queryDate)
        {
            var budgetRepo = _budgetRepo;
            return budgetRepo.GetAll().FirstOrDefault(x => x.YearMonth == queryDate.ToString("yyyyMM"));
        }
    }
}