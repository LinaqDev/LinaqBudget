using LinaqBudget.Helpers;
using LinaqBudget.Interfaces;
using LinaqBudget.Services;
using LinaqBudget.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LinaqBudget.ViewModels
{
    public class MainViewModel : BaseModel
    {
        public string AppName => "Linaq Budget";
        public string AppCaption => $"{AppName} ({AppVersion})";
        public string AppVersion => $"v{string.Join(".", Assembly.GetAssembly(typeof(App)).GetName().Version.ToString().Split('.').Take(3))}";

        private readonly IDataService dataService;
        private string lastTransactionCategoryId;
        private string lastTransactionAccountId;

        public MainViewModel()
        {
            dataService = new JsonDataService();

            lastTransactionAccountId = Properties.Settings.Default.LastTransactionAccountId;
            lastTransactionCategoryId = Properties.Settings.Default.LastTransactionCategoryId;

            DateFrom = DateTime.MinValue;
            DateTo = DateTime.MaxValue;

            RefreshAccounts();
            RefreshCategories();
            RefreshTransactions();

            AddAccountCmd = new RelayCommand(AddAccountExe);
            DeleteAccountCmd = new RelayCommand(DeleteAccountExe, (x) => SelectedAccount != null);

            AddCategoryCmd = new RelayCommand(AddCategoryExe);
            DeleteCategoryCmd = new RelayCommand(DeleteCategoryExe);

            AddTransactionCmd = new RelayCommand(AddTransactionExe, (x) => Accounts.Count > 0 && Categories.Count > 0);
            DeleteTransactionCmd = new RelayCommand(DeleteTransactionExe);
        }

        public ICommand ShowHideLeftPanelCmd { get; set; }
        public ICommand AddAccountCmd { get; set; }
        public ICommand DeleteAccountCmd { get; set; }
        public ICommand AddCategoryCmd { get; set; }
        public ICommand DeleteCategoryCmd { get; set; }
        public ICommand AddTransactionCmd { get; set; }
        public ICommand DeleteTransactionCmd { get; set; }


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

        private ObservableCollection<Transaction> _transactions;

        public ObservableCollection<Transaction> Transactions
        {
            get { return _transactions; }
            set
            {
                _transactions = value;
                RaisePropertyChanged(nameof(Transactions));
            }
        }

        private DateTime _dateFrom;
        public DateTime DateFrom
        {
            get => _dateFrom;
            set
            {
                _dateFrom = value;
                RaisePropertyChanged(nameof(DateFrom));
            }
        }

        private DateTime _dateTo;
        public DateTime DateTo
        {
            get => _dateTo;
            set
            {
                _dateTo = value;
                RaisePropertyChanged(nameof(DateTo));
            }
        }

        private void RefreshAccounts()
        {
            Accounts = new ObservableCollection<Account>(dataService.GetAllAccounts());
        }

        private void RefreshCategories()
        {
            Categories = new ObservableCollection<Category>(dataService.GetAllCategories());
        }

        private void RefreshTransactions()
        {
            RefreshAccounts(); 
            Transactions = new ObservableCollection<Transaction>(dataService.GetAllTransactions());
        
        }

        private void AddAccountExe(object obj)
        {
            var dc = new AddAccountViewModel();
            var win = new AddAccountWin() { DataContext = dc };

            win.Owner = App.Current.MainWindow;

            win.ShowDialog();

            if (dc.Canceled)
                return;

            if (dc.ResultAccount != null)
            {
                dataService.AddAccount(dc.ResultAccount);
                RefreshAccounts();
            }
        }

        private void AddCategoryExe(object obj)
        {
            var dc = new AddCategoryViewModel();
            var win = new AddCategoryWin() { DataContext = dc };

            win.Owner = App.Current.MainWindow;

            win.ShowDialog();

            if (dc.Canceled)
                return;

            if (dc.ResultCategory != null)
            {
                dataService.AddCategory(dc.ResultCategory);
                RefreshCategories();
            }
        }

        private void AddTransactionExe(object obj)
        {
            if (string.IsNullOrWhiteSpace(lastTransactionAccountId))
                lastTransactionAccountId = Accounts.First().Id;

            if (string.IsNullOrWhiteSpace(lastTransactionCategoryId))
                lastTransactionCategoryId = Categories.First().Id;

            var dc = new AddTransactionViewModel(lastTransactionAccountId, lastTransactionCategoryId, dataService);
            var win = new AddTransactionWin() { DataContext = dc };

            win.Owner = App.Current.MainWindow;

            win.ShowDialog();

            if (dc.Canceled)
                return;

            if (dc.ResultTransaction != null)
            {
                lastTransactionCategoryId = dc.ResultTransaction.CategoryId;
                lastTransactionAccountId = dc.ResultTransaction.AccountId;
                dataService.AddTransaction(dc.ResultTransaction);
                RefreshTransactions();
            }
        }

        private void DeleteAccountExe(object obj)
        {
            if (obj is Account acc)
            {
                dataService.DeleteAccountById(acc.Id);
                RefreshAccounts();
            }
        }

        private void DeleteCategoryExe(object obj)
        {
            if (obj is Category category)
            {
                dataService.DeleteCategoryById(category.Id);
                RefreshCategories();
            }
        }


        private void DeleteTransactionExe(object obj)
        {
            if (obj is Transaction transaction)
            {
                dataService.DeleteTransactionById(transaction.Id);
                RefreshTransactions();
            }
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.LastTransactionAccountId = lastTransactionAccountId;
            Properties.Settings.Default.LastTransactionCategoryId = lastTransactionCategoryId;
            Properties.Settings.Default.Save();
        }
    }
}
