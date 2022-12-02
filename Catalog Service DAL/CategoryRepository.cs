using Catalog_Service_BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog_Service_DAL
{
    public class CategoryRepository : ICategoryRepository
    {
        public void Add(Category category)
        {
            using (var context = new CatalogDbContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        public void Delete(Category category)
        {
            using (var context = new CatalogDbContext())
            {
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }

        public Category Get(Guid id)
        {
            using (var context = new CatalogDbContext())
            {
                return context.Categories.Find(id);
            }
        }

        public IEnumerable<Category> GetAll()
        {
            using (var context = new CatalogDbContext())
            {
                return context.Categories.ToList();
            }
        }

        public void Update(Category category)
        {
            using (var context = new CatalogDbContext())
            {
                context.Categories.Update(category);
                context.SaveChanges();
            }
        }
    }
}
