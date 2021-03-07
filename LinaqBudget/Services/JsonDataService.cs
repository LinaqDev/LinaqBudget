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

        /// <summary>
        /// 
        /// </summary>
        public JsonDataService()
        {
            InitDirectories();
            accounts = LoadAccountsData();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitDirectories()
        {
            Log.Information("Initializing directories for JsonDataService");

            dataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "LinaqBudget", "Data");
            accountstDataFilePath = Path.Combine(dataDirectory, "accounts.dat");

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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category GetSingleCategoryById(string id)
        {
            throw new NotImplementedException();
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


        private List<Account> accounts { get; set; }

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
            throw new NotImplementedException();
        }

        public void DeleteCategoryById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
