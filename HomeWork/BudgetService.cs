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
                var queryDaysInPeriod = QueryDaysInPeriod(start, end, budget);
                result +=  queryDaysInPeriod * budget.GetDailyAmount();
            }

            return result;
        }

        private static int QueryDaysInPeriod(DateTime start, DateTime end, Budget budget)
        {
            var queryDaysInPeriod = budget.QueryDaysInPeriod(new Period(start, end));
            return queryDaysInPeriod;
        }

        private Budget GetBudget(DateTime queryDate)
        {
            var budgetRepo = _budgetRepo;
            return budgetRepo.GetAll().FirstOrDefault(x => x.YearMonth == queryDate.ToString("yyyyMM"));
        }
    }
}