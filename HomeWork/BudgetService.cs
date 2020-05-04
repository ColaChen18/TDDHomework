using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

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
            var budgets = _budgetRepo.GetAll();
            if (start > end)
                return 0;

            return GetBudget(start, end, budgets);
        }

        private decimal GetBudget(DateTime start, DateTime end, List<Budget> budgets)
        {
            var dayAmountDict = budgets.Select(x => new
            {
                x.YearMonth,
                DayAmount = x.Amount / DateTime.DaysInMonth(DateTime.ParseExact(x.YearMonth, "yyyyMM", null).Year,
                    DateTime.ParseExact(x.YearMonth, "yyyyMM", null).Month)
            }).ToDictionary(x => x.YearMonth, y => y.DayAmount);


            if (end.Month - start.Month >= 1)
            {
                int startAmount = (DateTime.DaysInMonth(start.Year, start.Month) - start.Day + 1) *
                                  dayAmountDict[start.ToString("yyyyMM")];
                int endAmount = (end.Day) * dayAmountDict[end.ToString("yyyyMM")];

                return startAmount + endAmount;
            }

            int totalDay = (end - start).Days + 1;

            var amount = budgets
                .Where(x => x.YearMonth == start.ToString("yyyyMM"))
                .Sum(a => a.Amount);

            return amount / DateTime.DaysInMonth(start.Year, start.Month) * totalDay;
        }
    }
}