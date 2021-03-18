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
            Account = new Account();
            Category = new Category();
            CreationDate = DateTime.Now;
        }

        public string Id { get; set; }
        public string AccountId { get; set; }
        public Account Account { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int Type { get; set; }
        public double Amount { get; set; }
    }
}
