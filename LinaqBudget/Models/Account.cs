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
            Transactions = new List<Transaction>();
        }

        public string Id { get; set; }
        public string Designation { get; set; }
        public string Description { get; set; }
        public double Balance => Transactions.Where(x => x.Type == 1).Select(x => x.Amount).Sum() - Transactions.Where(x => x.Type == 0).Select(x => x.Amount).Sum();
        public DateTime CreationDate { get; set; }
        public int Type { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
