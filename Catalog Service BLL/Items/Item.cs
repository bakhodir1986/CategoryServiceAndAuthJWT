using Catalog_Service_Rest_Api.HATEOAS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog_Service_BLL
{
    public class Item
    {
        public Item()
        {
            Links = new List<LinkDto>();
        }
        public Guid Id { get; set; }
        public string Name
        {
            get { return Name; }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException("value");
                if (value.Length > 50) throw new ArgumentOutOfRangeException("value");
                Name = value;
            }
        }
         public string Description { get; set; }
         public string Image { get; set; }
         public Category Category { get { return Category; }
            set { 
                if (value == null) throw new ArgumentNullException("value");
                Category = value;
            } 
         }
         
         public decimal Price { get { return Price; } 
            set { 
                if (Price <= 0) throw new ArgumentOutOfRangeException("price");
                
                Price = value; 
            } 
         }
         
         public uint Amount { get; set; }

         public List<LinkDto> Links { get; set; }
    }
}
