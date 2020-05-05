using System;

namespace HomeWork
{
    public class Budget
    {
        public string YearMonth { get; set; }

        public int Amount { get; set; }

        public decimal GetDaysInMonth()
        {
            decimal daysInMonth = DateTime.DaysInMonth(
                DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null).Year,
                DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null).Month);
            return daysInMonth;
        }

        public decimal GetDailyAmount()
        {
            var dailyAmount = Amount / GetDaysInMonth();
            return dailyAmount;
        }

        public DateTime BudgetFirstDay()
        {
            var budgetFirstDay = new DateTime(DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null).Year,
                DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null).Month, 1);
            return budgetFirstDay;
        }

        public DateTime BudgetLastDay()
        {
            var budgetLastDay = new DateTime(DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null).Year,
                DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null).Month,
                DateTime.DaysInMonth(DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null).Year,
                    DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null).Month));
            return budgetLastDay;
        }

        public decimal GetPeriodAmount(Period period)
        {
            return period.OverlapDays(new Period(BudgetFirstDay(), BudgetLastDay())) * GetDailyAmount();
        }
    }
}