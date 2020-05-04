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
                var daysInStartMonth = DateTime.DaysInMonth(start.Year, start.Month) - start.Day + 1;
                int startAmount = daysInStartMonth * dailyBudgetOfStart;
                
                int middleAmount = 0;
                for (int i = 1; i < end.Month - start.Month; i++)
                {
                    var middleDate = start.AddMonths(i);
                    
                    var dailyBudgetOfMiddle = GetDailyBudget(middleDate);
                    var daysInMiddleMonth = DateTime.DaysInMonth(middleDate.Year, middleDate.Month);
                    middleAmount += daysInMiddleMonth * dailyBudgetOfMiddle;
                }

                var dailyBudgetOfEnd = GetDailyBudget(end);
                var daysInEndMonth = (end.Day);
                int endAmount = daysInEndMonth * dailyBudgetOfEnd;

                return startAmount + middleAmount + endAmount;
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