using LinaqBudget.Helpers;
using System;
using System.Windows;
using System.Windows.Input;

namespace LinaqBudget.ViewModels
{
    public class AddCategoryViewModel:BaseModel
    {
        public Category ResultAccount;
        public AddCategoryViewModel()
        {
            ResultAccount = new Category();
            ResultAccount.CreationDate = DateTime.Now;
            OkCmd = new RelayCommand(OkExe);
            CancelCmd = new RelayCommand(CancelExe);
        }

        public AddCategoryViewModel(Category category)
        {
            ResultAccount = category;
            Designation = category.Designation;
            Description = category.Description; 
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

        private void CancelExe(object obj)
        {
            if (obj is Window win)
                win.Close();
        }

        private void OkExe(object obj)
        {
            ResultAccount.Designation = Designation;
            ResultAccount.Description = Description; 

            if (obj is Window win)
                win.Close();
        }
    }
}
