using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace B10810059_柯翰聲_LINQ
{
    internal class Product
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        internal static Product ParseRow(string row)
        {
            var data = row.Split(',');
            return new Product()
            {
                Id = data[0],
                Name = data[1],
                Quantity = Convert.ToInt32(data[2]),
                Price = Convert.ToDecimal(data[3]),
                Category = data[4]

            };
        }


    }
}



