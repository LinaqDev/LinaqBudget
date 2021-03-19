using LinaqBudget.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LinaqBudget.ViewModels
{
    public class AddAccountViewModel : BaseModel
    {
        public Account ResultAccount;
        public bool Canceled = true;
        public AddAccountViewModel()
        {
            ResultAccount = new Account();
            ResultAccount.CreationDate = DateTime.Now;
            Designation = "New Account";

            OkCmd = new RelayCommand(OkExe);
            CancelCmd = new RelayCommand(CancelExe);
        }

        public AddAccountViewModel(Account account)
        {
            ResultAccount = account;
            Designation = account.Designation;
            Description = account.Description;
            Balance = account.Balance;

            OkCmd = new RelayCommand(OkExe);
            CancelCmd = new RelayCommand(CancelExe);
        }

        public ICommand OkCmd { get; set; }
        public ICommand CancelCmd { get; set; }


        private string _designation;
        public string Designation
        {
            get => _designation;
            set
            {
                _designation = value;
                RaisePropertyChanged(nameof(Designation));
            }
        }

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

        private double _balance;

        public double Balance
        {
            get => _balance;
            set
            {
                _balance = value;
                RaisePropertyChanged(nameof(Balance));
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
            ResultAccount.Designation = Designation;
            ResultAccount.Description = Description; 

            if (obj is Window win)
                win.Close();
        }
    }
}
