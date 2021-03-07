using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinaqBudget
{
    public class Transaction
    {
        public Transaction()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string AccountId { get; set; }
        public string CategoryId { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int Type { get; set; }
        public double Amount { get; set; }
    }
}
