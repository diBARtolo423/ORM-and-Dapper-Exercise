using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Dapper
{
    public class Product
    {
        public Product()
        {
        }

        public int ProductID { get; set; }
        public string Name { get; set; }
        public object Price { get; set; }
        public object CategoryID { get; set; }
        public object OnSale { get; set; }
        public object StockLevel { get; set; }
    }
}
