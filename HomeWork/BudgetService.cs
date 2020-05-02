using System;
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

            return 1000;
        }
    }
}