using System;
using System.Linq;
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

            int totoalDay = (end - start).Days+1;

            var amount = budgets
                             .Where(x => x.YearMonth == start.ToString("yyyyMM"))
                             .Sum(a=>a.Amount);

            return amount /30* totoalDay;
        }
    }
}