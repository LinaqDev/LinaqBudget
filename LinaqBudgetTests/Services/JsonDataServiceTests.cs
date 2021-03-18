using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinaqBudget.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinaqBudget.Interfaces;
using System.IO;

namespace LinaqBudget.Services.Tests
{
    [TestClass()]
    public class JsonDataServiceTests
    {
        string outputDir = "tests";

        [TestMethod()]
        public void JsonDataServiceTest()
        {
            var dataService = new JsonDataService(outputDir);
            Assert.IsNotNull(dataService);
        }

        private IDataService CreateTestDataService()
        {
            if (Directory.Exists(outputDir))
                Directory.Delete(outputDir,true);

            return new JsonDataService(outputDir);
        }

        [TestMethod()]
        public void GetAllAccountsTest()
        {
            var dataService = CreateTestDataService();
            var allAccounts = dataService.GetAllAccounts();
            Assert.IsNotNull(allAccounts);
        }

        [TestMethod()]
        public void GetAllCategoriesTest()
        {
            var dataService = CreateTestDataService();
            var allCategories = dataService.GetAllAccounts();
            Assert.IsNotNull(allCategories);
        }

        [TestMethod()]
        public void GetAllTransactionsTest()
        {
            var dataService = CreateTestDataService();
            var allTransactions = dataService.GetAllTransactions();
            Assert.IsNotNull(allTransactions);
        }

        [TestMethod()]
        public void AddAccountTest()
        {
            var dataService = CreateTestDataService();
            var newAccount = new Account();
            dataService.AddAccount(newAccount);
            Assert.AreEqual(dataService.GetAllAccounts().Count, 1);
        }

        [TestMethod()]
        public void AddCategoryTest()
        {
            var dataService = CreateTestDataService();
            var newCategory = new Category();
            dataService.AddCategory(newCategory);
            Assert.AreEqual(dataService.GetAllCategories().Count, 1);
        }

        [TestMethod()]
        public void AddTransactionTest()
        {
            var dataService = CreateTestDataService();
            var newTransaction = new Transaction();
            dataService.AddTransaction(newTransaction);
            Assert.AreEqual(dataService.GetAllTransactions().Count, 1);
        }
    }
}