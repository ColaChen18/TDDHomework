using System;
using System.Collections.Generic;
using System.Linq;
using HomeWork;
using NUnit.Framework;

namespace HomeWorkTests
{
    [TestFixture]
    public class BudgetServiceTests
    {
        [SetUp]
        public void Setup()
        {
            _fakeRepo = new FakeBudgetRepo();
            _budgetService = new BudgetService(_fakeRepo);
        }

        private BudgetService _budgetService;
        private FakeBudgetRepo _fakeRepo;

        private void GiveBudgets(params Budget[] budget)
        {
            _fakeRepo.SetBudgets(budget.ToList());
        }

        private void BudgetShouldBe(int expected, DateTime start, DateTime end)
        {
            Assert.AreEqual(expected, _budgetService.Query(start, end));
        }

        [Test]
        public void April_MultiDay()
        {
            GiveBudgets(
                new Budget {YearMonth = "202004", Amount = 30000});
            BudgetShouldBe(5000, new DateTime(2020, 04, 01), new DateTime(2020, 04, 05));
        }

        [Test]
        public void April_MultiMonth()
        {
            GiveBudgets(
                new Budget {YearMonth = "202002", Amount = 2900},
                new Budget {YearMonth = "202003", Amount = 310}
            );
            BudgetShouldBe(3050, new DateTime(2020, 02, 01), new DateTime(2020, 03, 15));
        }

        [Test]
        public void April_OneDay()
        {
            GiveBudgets(
                new Budget {YearMonth = "202004", Amount = 30000});
            BudgetShouldBe(1000, new DateTime(2020, 04, 01), new DateTime(2020, 04, 01));
        }

        [Test]
        public void April_OneMonth()
        {
            GiveBudgets(
                new Budget {YearMonth = "202004", Amount = 60000});
            BudgetShouldBe(60000, new DateTime(2020, 04, 01), new DateTime(2020, 04, 30));
        }

        [Test]
        public void Reverse_Date()
        {
            GiveBudgets(
                new Budget {YearMonth = "202002", Amount = 2900},
                new Budget {YearMonth = "202003", Amount = 310}
            );
            BudgetShouldBe(0, new DateTime(2020, 03, 01), new DateTime(2020, 02, 01));
        }
        
        [Test]
        public void March_31_April_1()
        {
            GiveBudgets(
                new Budget {YearMonth = "202003", Amount = 310},
                new Budget {YearMonth = "202004", Amount = 30}
            );
            BudgetShouldBe(11, new DateTime(2020, 03, 31), new DateTime(2020, 04, 01));
        }

        [Test]
        public void March_31_May_1()
        {
            GiveBudgets(
                new Budget {YearMonth = "202003", Amount = 310},
                new Budget {YearMonth = "202004", Amount = 30},
                new Budget {YearMonth = "202005", Amount = 3100}
            );
            BudgetShouldBe(10+30+100, new DateTime(2020, 03, 31), new DateTime(2020, 05, 01));
        }
        
        [Test]
        public void March_5_March_10_But_Only_April_with_Budget()
        {
            GiveBudgets(
                new Budget {YearMonth = "202003", Amount = 0},
                new Budget {YearMonth = "202004", Amount = 30}
            );
            BudgetShouldBe(0, new DateTime(2020, 03, 5), new DateTime(2020,3, 10));
        }
        
        
        [Test]
        public void May_5_May_10_But_Only_April_with_Budget()
        {
            GiveBudgets(
                new Budget {YearMonth = "202004", Amount = 30},
                new Budget {YearMonth = "202005", Amount = 0}
            );
            BudgetShouldBe(0, new DateTime(2020, 05, 5), new DateTime(2020,5, 10));
        }
        
    }


    public class FakeBudgetRepo : IBudgetRepo
    {
        private List<Budget> _budgetList;

        public List<Budget> GetAll()
        {
            return _budgetList;
        }

        public void SetBudgets(List<Budget> budgets)
        {
            _budgetList = budgets;
        }
    }
}