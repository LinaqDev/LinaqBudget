using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinaqBudget.Interfaces
{
    public interface IDataService
    {
        void AddTransaction(Transaction transaction);
        Transaction GetSingleTransactionById(string id);
        List<Transaction> GetAllTransactions();
        List<Transaction> GetTransactionsForAccount(Account account);
        List<Transaction> GetTransactionsForCategory(Category category);

        void AddAccount(Account account);
        Account GetSingleAccountById(string id);
        List<Account> GetAllAccounts();

        void AddCategory(Category category);
        Category GetSingleCategoryById(string id);
        List<Category> GetAllCategories();
    }
}
