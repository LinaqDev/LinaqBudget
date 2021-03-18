using LinaqBudget.Helpers;
using LinaqBudget.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LinaqBudget.ViewModels
{
    public class AddTransactionViewModel:BaseModel
    {
        private readonly IDataService dataService;
        public Transaction ResultTransaction;
        public bool Canceled = true;

        public AddTransactionViewModel(string AccountId, string CategoryId, IDataService dataService)
        {
            this.dataService = dataService;
            ResultTransaction = new Transaction();
            ResultTransaction.AccountId = AccountId;
            ResultTransaction.CategoryId = CategoryId;
            OkCmd = new RelayCommand(OkExe);
            CancelCmd = new RelayCommand(CancelExe);

            Accounts = new ObservableCollection<Account>(dataService.GetAllAccounts());
            if (Accounts.Count == 0)
                throw new ArgumentOutOfRangeException("Account can not be empty"); 
            SelectedAccount = Accounts.FirstOrDefault(x => x.Id == AccountId); 
            if (SelectedAccount == null)
                SelectedAccount = Accounts.First();


            Categories = new ObservableCollection<Category>(dataService.GetAllCategories()); 
            if (Categories.Count == 0)
                throw new ArgumentOutOfRangeException("Categories can not be empty");
            SelectedCategory = Categories.FirstOrDefault(x => x.Id == CategoryId); 
            if (SelectedCategory == null)
                SelectedCategory = Categories.First();
        }
         

        public ICommand OkCmd { get; set; }
        public ICommand CancelCmd { get; set; }

        private string _description;

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        private double _amount;

        public double Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                RaisePropertyChanged(nameof(Amount));
            }
        }

        private ObservableCollection<Account> _accounts;

        public ObservableCollection<Account> Accounts
        {
            get { return _accounts; }
            set
            {
                _accounts = value;
                RaisePropertyChanged(nameof(Accounts));
            }
        }

        private Account _selectedAccount;
        public Account SelectedAccount
        {
            get => _selectedAccount;
            set
            {
                _selectedAccount = value;
                RaisePropertyChanged(nameof(SelectedAccount));
            }
        }

        private ObservableCollection<Category> _categories;

        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                RaisePropertyChanged(nameof(Categories));
            }
        }

        public List<string> TransactionTypes => new List<string>() { "Expense", "Income" };

        private int _selectedTransactionType = 0;
        public int SelectedTransactionType
        {
            get => _selectedTransactionType;
            set
            {
                _selectedTransactionType = value;
                RaisePropertyChanged(nameof(SelectedTransactionType));
            }
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                RaisePropertyChanged(nameof(SelectedCategory));
            }
        }

        private void CancelExe(object obj)
        {
            Canceled = true;
            if (obj is Window win)
                win.Close();
        }

        private void OkExe(object obj)
        {
            Canceled = false;
            ResultTransaction.Description = Description;
            ResultTransaction.Amount = Amount;
            ResultTransaction.AccountId = SelectedAccount.Id;
            ResultTransaction.Account = SelectedAccount;
            ResultTransaction.CategoryId = SelectedCategory.Id;
            ResultTransaction.Category = SelectedCategory;
            ResultTransaction.Type = SelectedTransactionType;

            if (obj is Window win)
                win.Close();
        }


    }
}
