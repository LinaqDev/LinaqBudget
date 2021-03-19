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
        private string transactionsDataFilePath;

        private List<Account> _accounts;
        private List<Account> Accounts
        {
            get => _accounts;
            set
            {
                _accounts = value;
            }
        }

        private List<Category> Categories { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JsonDataService(string dataDirPath = null)
        {
            if (dataDirPath == null)
                dataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "LinaqBudget", "Data");
            else
                dataDirectory = dataDirPath;

            InitDirectories();
            Accounts = LoadAccountsData();
            Categories = LoadCategoriesData();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitDirectories()
        {
            Log.Information("Initializing directories for JsonDataService");

            accountstDataFilePath = Path.Combine(dataDirectory, "accounts.dat");
            categoriesDataFilePath = Path.Combine(dataDirectory, "categories.dat");
            transactionsDataFilePath = Path.Combine(dataDirectory, "transactions.dat");

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
            Accounts.Add(account);
            SaveAccounts();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        public void AddCategory(Category category)
        {
            Log.Information("Adding new category {0}", category.Designation);
            Categories.Add(category);
            SaveCategories();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        public void AddTransaction(Transaction transaction)
        {
            Log.Information("Adding new transactions {0}", transaction.Description);
            transaction.Account.Transactions.Add(transaction);
            transaction.Category.Transactions.Add(transaction);
            SaveTransactions();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Account> GetAllAccounts()
        {
            if (Accounts == null)
                Accounts = LoadAccountsData();

            return Accounts;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Category> GetAllCategories()
        {
            if (Categories == null)
                Categories = LoadCategoriesData();


            return Categories;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Transaction> GetAllTransactions()
        {
            var result = new List<Transaction>();

            foreach (var item in Accounts)
            {
                foreach (var t in item.Transactions)
                {
                    t.Account = Accounts.FirstOrDefault(x=>x.Id == t.AccountId);
                    t.Category = Categories.FirstOrDefault(x=>x.Id == t.CategoryId);
                    result.Add(t);
                }
            } 

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Account GetSingleAccountById(string id)
        {
            return Accounts.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category GetSingleCategoryById(string id)
        {
            return Categories.FirstOrDefault(x => x.Id == id);
        }
 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public List<Transaction> GetTransactionsForAccount(Account account)
        {
            return account.Transactions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<Transaction> GetTransactionsForCategory(Category category)
        {
            return category.Transactions;
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<Category> LoadCategoriesData()
        {
            Log.Information("Loading categories data...");

            if (File.Exists(categoriesDataFilePath))
            {
                Log.Information("categories loaded from file.");
                var content = File.ReadAllText(categoriesDataFilePath);
                return JsonConvert.DeserializeObject<List<Category>>(content);
            }

            Log.Information("categories data file does not exist.");
            return new List<Category>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<Transaction> LoadTransactionsData()
        {
            Log.Information("Loading transactions data...");

            if (File.Exists(transactionsDataFilePath))
            {
                Log.Information("transactions loaded from file.");
                var content = File.ReadAllText(transactionsDataFilePath);
                var result = JsonConvert.DeserializeObject<List<Transaction>>(content);

                foreach (var item in result)
                {
                    item.Account = Accounts.FirstOrDefault(x => x.Id == item.AccountId);
                    item.Category = Categories.FirstOrDefault(x => x.Id == item.CategoryId);
                }

                return result;
            }

            Log.Information("transactions data file does not exist.");
            return new List<Transaction>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        public void DeleteTransaction(Transaction transaction)
        {
            transaction.Account.Transactions.Remove(transaction);
            transaction.Category.Transactions.Remove(transaction);
            SaveTransactions();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTransactionById(string id)
        {
            var acc = Accounts.FirstOrDefault(x => x.Transactions.FirstOrDefault(y => y.Id == id) != null);
            var cat = Categories.FirstOrDefault(x => x.Transactions.FirstOrDefault(y => y.Id == id) != null);
            acc.Transactions.Remove(acc.Transactions.FirstOrDefault(x => x.Id == id)); 
            cat.Transactions.Remove(cat.Transactions.FirstOrDefault(x => x.Id == id)); 
            SaveTransactions();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        public void DeleteAccount(Account account)
        {
            var toRemove = Accounts.FirstOrDefault(x => x.Id == account.Id);
            Accounts.Remove(toRemove);
            SaveAccounts();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteAccountById(string id)
        {
            var toRemove = Accounts.FirstOrDefault(x => x.Id == id);
            Accounts.Remove(toRemove);
            SaveAccounts();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        public void DeleteCategory(Category category)
        {
            var toRemove = Categories.FirstOrDefault(x => x.Id == category.Id);
            Categories.Remove(toRemove);
            SaveCategories();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCategoryById(string id)
        {
            var toRemove = Categories.FirstOrDefault(x => x.Id == id);
            Categories.Remove(toRemove);
            SaveCategories();
        }

        /// <summary>
        /// 
        /// </summary>
        private void SaveAccounts()
        {
            Log.Information("Saving accounts data...");
            var content = JsonConvert.SerializeObject(Accounts, Formatting.Indented);
            File.WriteAllText(accountstDataFilePath, content);
        }

        /// <summary>
        /// 
        /// </summary>
        private void SaveCategories()
        {
            Log.Information("Saving categories data...");
            var content = JsonConvert.SerializeObject(Categories, Formatting.Indented);
            File.WriteAllText(categoriesDataFilePath, content);
        }
        /// <summary>
        /// 
        /// </summary>
        private void SaveTransactions()
        {
            SaveAccounts();
        }
    }
}

