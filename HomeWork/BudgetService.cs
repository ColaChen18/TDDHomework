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
                var budget = GetBudget(start.AddMonths(i).ToString("yyyyMM"));
                decimal daysInMonth = DateTime.DaysInMonth(
                    DateTime.ParseExact(budget.YearMonth + "01", "yyyyMMdd", null).Year,
                    DateTime.ParseExact(budget.YearMonth + "01", "yyyyMMdd", null).Month);
               // decimal daysInMonth = DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month);
                var amount = budget.Amount;
                var daysInPeriod = budget.QueryDaysInPeriod(new Period(start, end));
                var dailyAmount = amount / daysInMonth;
                result +=  daysInPeriod * dailyAmount;
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