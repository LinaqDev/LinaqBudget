﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinaqBudget
{
    public class Category
    {
        public Category()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string Designation { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
