using LinaqBudget.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinaqBudget.Providers
{
    public class JsonDataProvider : IDataProvider
    {
        public List<Account> GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public List<Transaction> GetAllTransactions()
        {
            throw new NotImplementedException();
        }

        public Account GetSingleAccountById(string id)
        {
            throw new NotImplementedException();
        }

        public Category GetSingleCategoryById(string id)
        {
            throw new NotImplementedException();
        }

        public Transaction GetSingleTransactionById(string id)
        {
            throw new NotImplementedException();
        }

        public List<Transaction> GetTransactionsForAccount(Account account)
        {
            throw new NotImplementedException();
        }

        public List<Transaction> GetTransactionsForCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
