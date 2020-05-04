using System;
using System.Linq;

namespace HomeWork
{
    public class BudgetService
    {
        private IBudgetRepo _budgetRepo;

        public BudgetService(IBudgetRepo budgetRepo)
        {
            _budgetRepo = budgetRepo;
        }

        public decimal Query(DateTime start, DateTime end)
        {
            if (start > end)
                return 0;

            if (end.Month - start.Month >= 1)
            {
                var dailyBudgetOfStart = GetDailyBudget(start);
                int startAmount = (DateTime.DaysInMonth(start.Year, start.Month) - start.Day + 1) * dailyBudgetOfStart;
                var dailyBudgetOfEnd = GetDailyBudget(end);
                int endAmount = (end.Day) * dailyBudgetOfEnd;

                return startAmount + endAmount;
            }

            int totalDay = (end - start).Days + 1;
            return GetDailyBudget(start) * totalDay;
        }

        private int GetDailyBudget(DateTime date)
        {
            var days = DateTime.DaysInMonth(date.Year, date.Month);
            var amount = _budgetRepo.GetAll().FirstOrDefault(x => x.YearMonth == date.ToString("yyyyMM")).Amount;
            return amount / days;
        }
    }
}