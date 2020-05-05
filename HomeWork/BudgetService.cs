﻿using System;
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

            if (end.Month - start.Month > 0)
            {
                int middleAmount = 0;
                for (int i =0; i < end.Month - start.Month+1; i++)
                {
                    var middleDate = start.AddMonths(i);

                    var daysInMiddleMonth = DateTime.DaysInMonth(middleDate.Year, middleDate.Month);
                    var amountOfMiddleMonth = GetBudget(middleDate.ToString("yyyyMM")).Amount;
                    var queryDaysInMiddle = QueryDaysInPeriod(new Period(start, end), middleDate);
                    middleAmount += queryDaysInMiddle * (amountOfMiddleMonth / daysInMiddleMonth);
                }
                return  middleAmount ;
            }

            var daysInMonth = DateTime.DaysInMonth(start.Year, start.Month);
            var amountOfMonth = GetBudget(start.ToString("yyyyMM")).Amount;
            var queryDays = QueryDaysInPeriod(new Period(start, end), start);
            return queryDays * (amountOfMonth / daysInMonth);
        }

        private int QueryDaysInPeriod(Period period, DateTime date)
        {
            var budgetFirstDay = new DateTime(date.Year, date.Month, 1);
            var budgetLastDay = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
            var periodEnd = period.End <= budgetLastDay ? period.End : budgetLastDay;
            var periodStart = budgetFirstDay <= period.Start ? period.Start : budgetFirstDay;
            return (periodEnd - periodStart).Days + 1;
        }

        private Budget GetBudget(string yearMonth)
        {
            var budgetRepo = _budgetRepo;
            return budgetRepo.GetAll().FirstOrDefault(x => x.YearMonth == yearMonth);
        }
    }
}