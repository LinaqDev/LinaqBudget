using LinaqBudget.Helpers;
using LinaqBudget.Interfaces;
using LinaqBudget.Services;
using LinaqBudget.Views;
using System;
using System.Collections.Generic;
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
            AddAccountCmd = new RelayCommand(AddAccountExe);
        }

        public ICommand AddAccountCmd { get; set; }


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
            }
        }
    }
}
