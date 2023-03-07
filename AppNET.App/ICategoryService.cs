using AppNET.Domain.Entities;
using AppNET.Domain.Interfaces;

namespace AppNET.App
{
    public interface ICategoryService
    {
        void Create(int id, string name);
        bool Delete(int categoryId);
        IReadOnlyCollection<Category> GetAll();
        Category Update(int categoryId, string newCategoryName);
    }
}
