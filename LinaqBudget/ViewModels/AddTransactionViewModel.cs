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
    public class AddTransactionViewModel:BaseModel
    {
        public Transaction ResultTransaction;
        public AddTransactionViewModel(string AccountId, string CategoryId)
        {
            ResultTransaction = new Transaction();
            ResultTransaction.AccountId = AccountId;
            ResultTransaction.CategoryId = CategoryId;
            OkCmd = new RelayCommand(OkExe);
            CancelCmd = new RelayCommand(CancelExe);
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


        private void CancelExe(object obj)
        {
            if (obj is Window win)
                win.Close();
        }

        private void OkExe(object obj)
        { 
            ResultTransaction.Description = Description; 

            if (obj is Window win)
                win.Close();
        }
    }
}
