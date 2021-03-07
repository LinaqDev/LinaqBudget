using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinaqBudget
{
    public class Account
    {
        public int Id { get; set; }
        public int Designation { get; set; }
        public int Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int Type { get; set; }
    }
}
