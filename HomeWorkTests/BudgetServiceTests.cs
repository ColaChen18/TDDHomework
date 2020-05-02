using NUnit.Framework;
using HomeWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Tests
{
    [TestFixture()]
    public class BudgetServiceTests
    {
        private BudgetService _budgetService;
        private FakeBudgetRepo _fakeRepo;

        [SetUp]
        public void Setup()
        {
            _fakeRepo = new FakeBudgetRepo();
            _budgetService = new BudgetService(_fakeRepo);
        }

        [Test()]
        public void April_OneDay()
        {
            _fakeRepo.SetBudgets(new List<Budget>()
            {
                new Budget()
                {
                    YearMonth = "202004",
                    Amount    =  30000
                }
            }); 
            BudgetShouldBe(1000, new DateTime(2020, 04, 01), new DateTime(2020, 04, 01));
        }

    

        [Test()]
        public void April_MultiDay()
        {
            _fakeRepo.SetBudgets(new List<Budget>()
            {
                new Budget()
                {
                    YearMonth = "202004",
                    Amount    =  30000
                }
            });
            BudgetShouldBe(5000,new DateTime(2020,04,01),new DateTime(2020,04,05) );
        }

        [Test()]
        public void April_OneMonth()
        {
            _fakeRepo.SetBudgets(new List<Budget>()
            {
                new Budget()
                {
                    YearMonth = "202004",
                    Amount    =  60000
                }
            });
            BudgetShouldBe(60000, new DateTime(2020, 04, 01), new DateTime(2020, 04, 30));
        }

        private void BudgetShouldBe(int expected, DateTime start, DateTime end)
        {

            Assert.AreEqual(expected, _budgetService.Query(start, end));
        }
    }

    public class FakeBudgetRepo : IBudgetRepo
    {
        private List<Budget> _budgetList;

        public void SetBudgets(List<Budget> budgets)
        {
            _budgetList = budgets;
        }

        public List<Budget> GetAll()
        {
            return _budgetList;
        }
    }
}