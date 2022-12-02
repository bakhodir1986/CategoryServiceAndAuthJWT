using Catalog_Service_BLL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog_Service_DAL
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext() : base()
        {

        }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Item>? Items { get; set; }
    }
}
