using AppNET.Domain.Entities;
using AppNET.Domain.Interfaces;
using AppNET.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNET.App
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;
        public ProductService()
        {
            _repository = IOCContainer.Resolve<IRepository<Product>>();
        }
        public void Create(int id, string name, int stok, decimal price, int categoryId, DateTime createdDate)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Ürün bulunamadi");

            Product product = new Product();
            product.Id = id;
            product.Name = name;
            product.Stock = stok;
            product.Price = price;
            product.CategoryId = categoryId;
            product.CreatedDate = DateTime.Now;
            //  product.UpdatedDate= DateTime.Now;
            _repository.Add(product);
        }

        public bool Delete(int productId)
        {
            return _repository.Remove(productId);
        }

        public IReadOnlyCollection<Product> GetAll()
        {
            return _repository.GetList().ToList().AsReadOnly();
        }

        public Product Update(int productId, string newName, int newStok, decimal newPrice, int categoryId, DateTime createdDate, DateTime updatedDate)
        {
            Product product = new Product();
            product.Id = productId;
            product.Name = newName;
            product.Stock = newStok;
            product.Price = newPrice;
            product.CategoryId = categoryId;
            product.CreatedDate = createdDate;
            product.UpdatedDate = DateTime.Now;


            return _repository.Update(productId, product);
        }
    }
}
