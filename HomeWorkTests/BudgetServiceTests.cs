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
        private BudgetService budgetService;

        [SetUp]
        public void Setup()
        {
            budgetService = new BudgetService(new FakeBudgetRepo());
        }

        [Test()]
        public void April_OneDay()
        {
            BudgetShouldBe(1000, new DateTime(2020, 04, 01), new DateTime(2020, 04, 01));
        }

        private void BudgetShouldBe(int expected, DateTime start, DateTime end)
        {
            Assert.AreEqual(expected, budgetService.Query(start, end));
        }

        [Test()]
        public void April_MultiDay()
        {
         
            BudgetShouldBe(5000,new DateTime(2020,04,01),new DateTime(2020,04,05) );
        }

        [Test()]
        public void April_OneMonth()
        {

            BudgetShouldBe(30000, new DateTime(2020, 04, 01), new DateTime(2020, 04, 30));
        }
    }

    public class FakeBudgetRepo : IBudgetRepo
    {
        public List<Budget> GetAll()
        {
            return new List<Budget>()
            {
                new Budget()
                {
                    YearMonth = "202004",
                    Amount =  30000
                }
            };
        }
    }
}