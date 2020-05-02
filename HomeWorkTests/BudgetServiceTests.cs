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
            var actual = budgetService.Query(new DateTime(2020, 04, 01), new DateTime(2020, 04, 01));
            Assert.AreEqual(1000, actual);
        }

        [Test()]
        public void April_MultiDay()
        {
            var actual = budgetService.Query(new DateTime(2020, 04, 01), new DateTime(2020, 04, 05));
            Assert.AreEqual(5000, actual);
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