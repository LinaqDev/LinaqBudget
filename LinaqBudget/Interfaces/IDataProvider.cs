using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinaqBudget.Interfaces
{
    public interface IDataProvider
    {
        Transaction GetSingleTransactionById(string id);
        List<Transaction> GetAllTransactions();
        List<Transaction> GetTransactionsForAccount(Account account);
        List<Transaction> GetTransactionsForCategory(Category category);
        Account GetSingleAccountById(string id);
        List<Account> GetAllAccounts();
        Category GetSingleCategoryById(string id);
        List<Category> GetAllCategories();
    }
}
