using System;
using System.Collections.Generic;
using System.Text;
using VStest.MyShop;

namespace VStest.MyShop
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
       public int SupplierID { get; set; }
        public Supplier Supplier { get; set; }
       public int CategoryID { get; set; }
        public Category Category { get; set; }

        public double Price { get; set; }
    }
}
