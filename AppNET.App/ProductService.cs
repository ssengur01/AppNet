using AppNET.Domain.Entities;
using AppNET.Domain.Interfaces;
using AppNET.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppNET.App
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        public ProductService()
        {
            _productRepository=IOCContainer.Resolve<IRepository<Product>>();
        }

        public void Create(int id, string name, int stock, int categoryId, decimal price)
        { 
               Product product = new Product();
            {
                 Id = id,
                Name = name,
                Stock = stock,
                CategoryId = categoryId,
                Price = price
                CreatedDate=DateTime.Now,
            };
              _productRepository.Add(product);
        }

        public void Create(int v1, object text, int v2, int v3, decimal v4)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int productId)
        {
            return _productRepository.Remove(productId);
        }

        public IReadOnlyCollection<Product> GetAll()
        {
           return _productRepository.GetList().ToList().AsReadOnly();
        }

        public Product Update(int productId, Product newProduct)
        {
           if(productId!=newProduct.Id)
            
                throw new ArgumentException("productId değerleri eşleşmiyor.");

                Product oldProduct = _productRepository.GetById(productId);

                if(oldProduct == null)
                
                    throw new Exception("Güncellenmek istenen ürün bulunamadı.");
           

            newProduct.UpdateDate= DateTime.Now;
            return _productRepository.Update(productId, newProduct);
        }
    }
}
