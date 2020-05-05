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

            int result = 0;
            for (var i = 0; i < end.Month - start.Month + 1; i++)
            {
                var currentMonth = start.AddMonths(i);
                var budget = GetBudget(currentMonth.ToString("yyyyMM"));
                var daysInMonth = DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month);
                var amount = budget.Amount;
                var daysInPeriod = budget.QueryDaysInPeriod(new Period(start, end));
                result += daysInPeriod * (amount / daysInMonth);
            }

            return result;
        }

        private Budget GetBudget(string yearMonth)
        {
            var budgetRepo = _budgetRepo;
            return budgetRepo.GetAll().FirstOrDefault(x => x.YearMonth == yearMonth);
        }
    }
}