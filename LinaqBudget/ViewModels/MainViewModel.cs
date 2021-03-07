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
using System.Windows.Input;

namespace LinaqBudget.ViewModels
{
    public class MainViewModel : BaseModel
    {
        public string AppName => "Linaq Budget";
        public string AppCaption => $"{AppName} ({AppVersion})";
        public string AppVersion => $"v{string.Join(".", Assembly.GetAssembly(typeof(App)).GetName().Version.ToString().Split('.').Take(3))}";

        private readonly IDataService dataService;

        public MainViewModel()
        {
            dataService = new JsonDataService();
            RefreshAccounts();

            AddAccountCmd = new RelayCommand(AddAccountExe);
            DeleteAccountCmd = new RelayCommand(DeleteAccountExe);

        }

        public ICommand AddAccountCmd { get; set; }
        public ICommand DeleteAccountCmd { get; set; }


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

        private void RefreshAccounts()
        {
            Accounts = new ObservableCollection<Account>(dataService.GetAllAccounts());
        }

        private void AddAccountExe(object obj)
        {
            var dc = new AddAccountViewModel();
            var win = new AddAccountWin() { DataContext = dc };

            win.ShowDialog();

            if (dc.Canceled)
                return;

            if (dc.ResultAccount != null)
            {
                dataService.AddAccount(dc.ResultAccount);
                RefreshAccounts();
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
    }
}
