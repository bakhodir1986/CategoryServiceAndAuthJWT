namespace Catalog_Service_BLL
{
    public interface ICategoryService
    {
        void AddCategory(Category _category);
        void AddItem(Guid categotyId, Item item);
        void DeleteCategory(Guid categoryId);
        void DeleteItem(Guid itemId);
        IEnumerable<Category> GetCategories();
        IEnumerable<Item> GetItems(Guid categoryId, int page);
        void UpdateCategory(Category category);
        void UpdateItem(Item item);
    }
}