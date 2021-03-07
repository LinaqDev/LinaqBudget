using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinaqBudget.ViewModels
{
    public class MainViewModel : BaseModel
    {
        public string AppName => "Linaq Budget";
        public string AppCaption => $"{AppName} ({AppVersion})";
        public string AppVersion => $"v{string.Join(".", Assembly.GetAssembly(typeof(App)).GetName().Version.ToString().Split('.').Take(3))}";

        public MainViewModel()
        {

        }
    }
}
