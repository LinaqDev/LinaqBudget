using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinaqBudget
{
    public class Transaction
    {
        public string Id { get; set; }
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int Type { get; set; }
    }
}
