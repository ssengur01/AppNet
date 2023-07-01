using AppNET.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;    
using System.Text;
using System.Threading.Tasks;
using AppNET.Infrastructure.IOToTXT;
using AppNET.Domain.Interfaces;
using AppNET.Infrastructure;
using System.Xml.Linq;

namespace AppNET.App
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;
        public CategoryService()
        {
            _repository=IOCContainer.Resolve<IRepository<Category>>();
        }
        public void Create(int id, string name)
        {
            if(string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Kategori adı boş olamaz");

            var oldCategory=_repository.GetList().FirstOrDefault(c => c.Name == name);
            if (oldCategory != null)
                return;

            
            Category category= new Category();
            category.Id = id;
            category.Name = name.ToUpper();
            _repository.Add(category);
        }

        public bool Delete(int categoryId)
        {
            return _repository.Remove(categoryId);
        }

        public IReadOnlyCollection<Category> GetAll()
        {
            return _repository.GetList().ToList().AsReadOnly();
        }
        public Category GetById(int id)
        {
            return _repository.GetList().FirstOrDefault(c => c.Id == id);
        }
        public int GetIdFromName(string categoryName)
        {
            return _repository.GetList().FirstOrDefault(c => c.Name == categoryName).Id;
        }

        public Category Update(int categoryId, string newCategoryName)
        {
            if (string.IsNullOrEmpty(newCategoryName))
                throw new ArgumentNullException("Kategori adı boş olamaz");

            var category= new Category();
            category.Id = categoryId;
            category.Name = newCategoryName.ToUpper(); 
            return _repository.Update(categoryId, category);
            
        }
    }
}
