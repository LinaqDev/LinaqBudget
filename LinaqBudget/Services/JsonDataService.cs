using LinaqBudget.Interfaces;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinaqBudget.Services
{
    public class JsonDataService : IDataService
    {
        private string dataDirectory;
        private string accountstDataFilePath;
        private string categoriesDataFilePath;

        private List<Account> accounts { get; set; }
        private List<Category> categories { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JsonDataService()
        {
            InitDirectories();
            accounts = LoadAccountsData();
            categories = LoadCategoriesData();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitDirectories()
        {
            Log.Information("Initializing directories for JsonDataService");

            dataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "LinaqBudget", "Data");
            accountstDataFilePath = Path.Combine(dataDirectory, "accounts.dat");
            categoriesDataFilePath = Path.Combine(dataDirectory, "categories.dat");

            if (!Directory.Exists(dataDirectory))
            {
                Log.Information("Creating directory: '{0}'", dataDirectory);
                Directory.CreateDirectory(dataDirectory);
            }

            Log.Information("Initialization directories for JsonDataService completed sucessfully.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        public void AddAccount(Account account)
        {
            Log.Information("Adding new account {0}", account.Designation);
            accounts.Add(account);
            SaveAccounts();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        public void AddCategory(Category category)
        {
            Log.Information("Adding new category {0}", category.Designation);
            categories.Add(category);
            SaveAccounts();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        public void AddTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Account> GetAllAccounts()
        {
            if (accounts == null)
                accounts = LoadAccountsData();

            return accounts;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Category> GetAllCategories()
        {
            if (categories == null)
                categories = LoadCategoriesData();

            return categories;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Transaction> GetAllTransactions()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Account GetSingleAccountById(string id)
        {
            return accounts.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category GetSingleCategoryById(string id)
        {
            return categories.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Transaction GetSingleTransactionById(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public List<Transaction> GetTransactionsForAccount(Account account)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<Transaction> GetTransactionsForCategory(Category category)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<Account> LoadAccountsData()
        {
            Log.Information("Loading accounts data...");

            if (File.Exists(accountstDataFilePath))
            {
                Log.Information("accounts loaded from file.");
                var content = File.ReadAllText(accountstDataFilePath);
                return JsonConvert.DeserializeObject<List<Account>>(content);
            }

            Log.Information("accounts data file does not exist.");
            return new List<Account>();
        }



        private List<Category> LoadCategoriesData()
        {
            Log.Information("Loading categories data...");

            if (File.Exists(categoriesDataFilePath))
            {
                Log.Information("categories loaded from file.");
                var content = File.ReadAllText(accountstDataFilePath);
                return JsonConvert.DeserializeObject<List<Category>>(content);
            }

            Log.Information("categories data file does not exist.");
            return new List<Category>();
        }

        /// <summary>
        /// 
        /// </summary>
        private void SaveAccounts()
        {
            Log.Information("Saving accounts data...");
            var content = JsonConvert.SerializeObject(accounts, Formatting.Indented);
            File.WriteAllText(accountstDataFilePath, content);
        }

        public void DeleteTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public void DeleteTransactionById(string id)
        {
            throw new NotImplementedException();
        }

        public void DeleteAccount(Account account)
        {
            var toRemove = accounts.FirstOrDefault(x => x.Id == account.Id);
            accounts.Remove(toRemove);
            SaveAccounts();
        }

        public void DeleteAccountById(string id)
        {
            var toRemove = accounts.FirstOrDefault(x => x.Id == id);
            accounts.Remove(toRemove);
            SaveAccounts();
        }

        public void DeleteCategory(Category category)
        {
            var toRemove = categories.FirstOrDefault(x => x.Id == category.Id);
            categories.Remove(toRemove);
            SaveCategories();
        }


        public void DeleteCategoryById(string id)
        {
            var toRemove = categories.FirstOrDefault(x => x.Id == id);
            categories.Remove(toRemove);
            SaveCategories();
        }


        private void SaveCategories()
        {
            Log.Information("Saving categories data...");
            var content = JsonConvert.SerializeObject(accounts, Formatting.Indented);
            File.WriteAllText(accountstDataFilePath, content);
        }
    }
}
