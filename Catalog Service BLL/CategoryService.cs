using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog_Service_BLL
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IItemRepository itemRepository;

        public CategoryService(ICategoryRepository categoryRepository,
            IItemRepository itemRepository)
        {
            this.categoryRepository = categoryRepository;
            this.itemRepository = itemRepository;
        }

        public IEnumerable<Category> GetCategories()
        {
            return categoryRepository.GetAll();
        }

        public IEnumerable<Item> GetItems(Guid categoryId, int page)
        {
            if (categoryId == Guid.Empty) throw new ArgumentNullException("categoryId");
            return itemRepository.GetItemsByCategory(categoryId, page);
        }

        public void AddCategory(Category _category)
        {
            if (_category == null) throw new ArgumentNullException("_category");

            Category category = new()
            {
                Id = Guid.NewGuid(),
                Name = _category.Name,
                Image = _category.Image,
                Parent = categoryRepository.Get(_category.Parent.Id)
            };

            categoryRepository.Add(category);
        }

        public void AddItem(Guid categotyId, Item item)
        {
            item.Id = Guid.NewGuid();
            item.Category = categoryRepository.Get(categotyId);

            itemRepository.Add(item);
        }

        public void UpdateCategory(Category category)
        {
            if (category == null) throw new ArgumentNullException("category");
            var selectedCategory = categoryRepository.Get(category.Id);
            selectedCategory.Name = category.Name;
            selectedCategory.Parent = category.Parent;

            categoryRepository.Update(selectedCategory);
        }

        public void UpdateItem(Item item)
        {
            var selectedItem = itemRepository.GetById(item.Id);
            if (selectedItem == null) return;

            selectedItem.Name = item.Name;
            selectedItem.Image = item.Image;
            selectedItem.Price = item.Price;
            selectedItem.Description = item.Description;
            selectedItem.Amount = item.Amount;

            itemRepository.Update(selectedItem);
        }

        public void DeleteItem(Guid itemId)
        {
            if (itemId == Guid.Empty) return;

            var selectedItem = itemRepository.GetById(itemId);

            itemRepository.Delete(selectedItem);
        }

        public void DeleteCategory(Guid categoryId)
        {
            if (categoryId == Guid.Empty) return;

            var selectedCategory = categoryRepository.Get(categoryId);

            var selectedItems = itemRepository.GetAllItemsByCategory(categoryId);

            foreach (var item in selectedItems)
            {
                itemRepository.Delete(item);
            }

            categoryRepository.Delete(selectedCategory);
        }
    }
}
