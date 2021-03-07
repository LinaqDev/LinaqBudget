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
        void DeleteTransaction(Transaction transaction); 
        void DeleteTransactionById(string id); 
        Transaction GetSingleTransactionById(string id);
        List<Transaction> GetAllTransactions();
        List<Transaction> GetTransactionsForAccount(Account account);
        List<Transaction> GetTransactionsForCategory(Category category);

        void AddAccount(Account account);
        void DeleteAccount(Account account);
        void DeleteAccountById(string id);
        Account GetSingleAccountById(string id);
        List<Account> GetAllAccounts();

        void AddCategory(Category category);
        void DeleteCategory(Category category);
        void DeleteCategoryById(string id);
        Category GetSingleCategoryById(string id);
        List<Category> GetAllCategories();
    }
}
