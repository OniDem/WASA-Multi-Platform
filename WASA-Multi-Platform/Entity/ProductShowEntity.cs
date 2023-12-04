using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASA_Multi_Platform.Entity
{
    internal class ProductShowEntity
    {
        public string Barcode {  get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public int Count { get; set; }
    }
}
