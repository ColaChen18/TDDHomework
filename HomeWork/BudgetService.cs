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

            if (end.Month - start.Month >0)
            {
                var daysInStartMonth = DateTime.DaysInMonth(start.Year, start.Month);
                var amountOfStartMonth = GetBudget(start.ToString("yyyyMM")).Amount;
                var queryDaysInStart = GetQueryDaysInStart(start);
                int startAmount =  queryDaysInStart * (amountOfStartMonth / daysInStartMonth);
                
                int middleAmount = 0;
                for (int i = 1; i < end.Month - start.Month; i++)
                {
                    var middleDate = start.AddMonths(i);

                    var daysInMiddleMonth = DateTime.DaysInMonth(middleDate.Year, middleDate.Month);
                    var amountOfMiddleMonth = GetBudget(middleDate.ToString("yyyyMM")).Amount;
                    var queryDaysInMiddle = QueryDaysInMiddle(middleDate);
                    middleAmount += queryDaysInMiddle * (amountOfMiddleMonth / daysInMiddleMonth);
                }

                var daysInEndMonth = DateTime.DaysInMonth(end.Year, end.Month);
                var amountOfEndMonth = GetBudget(end.ToString("yyyyMM")).Amount;
                var queryDaysInEnd = QueryDaysInEnd(end);
                int endAmount = queryDaysInEnd * (amountOfEndMonth / daysInEndMonth);

                return startAmount + middleAmount + endAmount;
            }

            var daysInMonth = DateTime.DaysInMonth(start.Year, start.Month);
            var amountOfMonth = GetBudget(start.ToString("yyyyMM")).Amount;
            var queryDays = QueryDays(start, end);
            return queryDays * (amountOfMonth / daysInMonth) ;
        }

        private static int QueryDays(DateTime start, DateTime end)
        {
            int queryDays = (end - start).Days + 1;
            return queryDays;
        }

        private static int QueryDaysInEnd(DateTime end)
        {
            var queryDaysInEnd = (end.Day);
            return queryDaysInEnd;
        }

        private static int QueryDaysInMiddle(DateTime middleDate)
        {
            var queryDaysInMiddle = DateTime.DaysInMonth(middleDate.Year, middleDate.Month);
            return queryDaysInMiddle;
        }

        private static int GetQueryDaysInStart(DateTime start)
        {
            var queryDaysInStart = DateTime.DaysInMonth(start.Year, start.Month) - start.Day + 1;
            return queryDaysInStart;
        }

        private Budget GetBudget(string yearMonth)
        {
            var budgetRepo = _budgetRepo;
            return budgetRepo.GetAll().FirstOrDefault(x => x.YearMonth == yearMonth);
        }
    }
}