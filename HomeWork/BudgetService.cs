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

            return _budgetRepo
                .GetAll()
                .Sum(budget => budget.GetPeriodAmount(new Period(start, end)));
        }

        private Budget GetBudget(DateTime queryDate)
        {
            var budgetRepo = _budgetRepo;
            return budgetRepo.GetAll().FirstOrDefault(x => x.YearMonth == queryDate.ToString("yyyyMM"));
        }
    }
}