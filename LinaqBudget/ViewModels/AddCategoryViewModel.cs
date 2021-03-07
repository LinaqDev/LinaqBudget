using LinaqBudget.Helpers;
using System;
using System.Windows;
using System.Windows.Input;

namespace LinaqBudget.ViewModels
{
    public class AddCategoryViewModel:BaseModel
    {
        public bool Canceled = true;
        public Category ResultCategory;
        public AddCategoryViewModel()
        {
            ResultCategory = new Category();
            Designation = "New Category";
            ResultCategory.CreationDate = DateTime.Now;
            OkCmd = new RelayCommand(OkExe);
            CancelCmd = new RelayCommand(CancelExe);
        }

        public AddCategoryViewModel(Category category)
        {
            ResultCategory = category;
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
            Canceled = true;
            if (obj is Window win)
                win.Close();
        }

        private void OkExe(object obj)
        {
            Canceled = false;
            ResultCategory.Designation = Designation;
            ResultCategory.Description = Description; 

            if (obj is Window win)
                win.Close();
        }
    }
}
