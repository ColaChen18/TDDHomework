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
        [Test()]
        public void QueryTest()
        {
            var budgetService = new BudgetService(new FakeBudgetService());
            var actual = budgetService.Query(new DateTime(2020, 04, 01), new DateTime(2020, 04, 01));
            Assert.AreEqual(1000, actual);
        }
    }

    public class FakeBudgetService : IBudgetRepo
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