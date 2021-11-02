using System.Collections.Generic;

namespace DataServiceLib
{
    public interface IDataService
    {
        IList<Category> GetCategories();
        Category GetCategory(int id);
        IList<Product> GetProducts(int page, int pageSize);
        Product GetProduct(int id);

        int NumberOfProducts();

        void CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(int id);
    }
}