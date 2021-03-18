using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinaqBudget
{
    public class Account
    {
        public Account()
        {
            Id = Guid.NewGuid().ToString();
            CreationDate = DateTime.Now;
        }

        public string Id { get; set; }
        public string Designation { get; set; }
        public string Description { get; set; }
        public double Balance { get; set; }
        public DateTime CreationDate { get; set; }
        public int Type { get; set; }
    }
}
