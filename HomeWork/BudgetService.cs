﻿using System;
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
                var days = DateTime.DaysInMonth(start.Year, start.Month);
                var amount = GetBudget(start).Amount;
                var dailyBudgetOfStart = amount / days;
                var daysInStartMonth = DateTime.DaysInMonth(start.Year, start.Month) - start.Day + 1;
                int startAmount = daysInStartMonth * dailyBudgetOfStart;
                
                int middleAmount = 0;
                for (int i = 1; i < end.Month - start.Month; i++)
                {
                    var middleDate = start.AddMonths(i);

                    var days1 = DateTime.DaysInMonth(middleDate.Year, middleDate.Month);
                    var amount1 = GetBudget(middleDate).Amount;
                    // var amount1 = _budgetRepo.GetAll().FirstOrDefault(x => x.YearMonth == middleDate.ToString("yyyyMM")).Amount;
                    var dailyBudgetOfMiddle = amount1 / days1;
                    var daysInMiddleMonth = DateTime.DaysInMonth(middleDate.Year, middleDate.Month);
                    middleAmount += daysInMiddleMonth * dailyBudgetOfMiddle;
                }

                var days2 = DateTime.DaysInMonth(end.Year, end.Month);
                var amount2 = GetBudget(end).Amount;
                // var amount2 = _budgetRepo.GetAll().FirstOrDefault(x => x.YearMonth == end.ToString("yyyyMM")).Amount;
                var dailyBudgetOfEnd = amount2 / days2;
                var daysInEndMonth = (end.Day);
                int endAmount = daysInEndMonth * dailyBudgetOfEnd;

                return startAmount + middleAmount + endAmount;
            }

            int totalDay = (end - start).Days + 1;
            var days3 = DateTime.DaysInMonth(start.Year, start.Month);
            var amount3 = _budgetRepo.GetAll().FirstOrDefault(x => x.YearMonth == start.ToString("yyyyMM")).Amount;
            return amount3 / days3 * totalDay;
        }

        private Budget GetBudget(DateTime start)
        {
            return _budgetRepo.GetAll().FirstOrDefault(x => x.YearMonth == start.ToString("yyyyMM"));
        }
    }
}