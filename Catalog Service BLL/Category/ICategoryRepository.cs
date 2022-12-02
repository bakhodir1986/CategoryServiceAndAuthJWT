using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog_Service_BLL
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Category Get(Guid id);
        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);
    }
}
