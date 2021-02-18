using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;

namespace DOMAIN.Services
{
    public class ProductService
    {
        private readonly IProductRepository productRepository;
        private readonly ProductValidator productValidator;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
            this.productValidator = new ProductValidator();
        }

        public async Task<IEnumerable<Product>> FindProducts(
            string name,
            string description,
            DateTime initial,
            DateTime final)
        {
            return (await this.productRepository.FindAllAsync(
                item => (name == null || item.Name.Contains(name))
                        && (description == null || item.Description.Contains(description))
                        && (initial == DateTime.MinValue
                            || final == DateTime.MinValue
                            || initial <= item.CreationDate
                            && item.CreationDate <= final))).OrderBy(item => item.Name);
        }

        public async Task Add(Product product)
        {
            if (!this.productValidator.IsValidProduct(product))
                throw new InvalidOperationException(this.productValidator.ErrorMessage);

            product.CreationDate = DateTime.Now;
            await this.productRepository.AddAsyn(product);
        }

        public async Task<Product> UpdateProduct(int id, Product productChanged)
        {
            var product = await this.productRepository.GetAsync(id);

            if (product == null)
                throw new InvalidOperationException($"Product with Id={id} not found");

            if (!this.productValidator.IsValidProduct(productChanged))
                throw new InvalidOperationException(this.productValidator.ErrorMessage);

            product.Name = productChanged.Name;
            product.Description = productChanged.Description;
            product.Price = productChanged.Price;

            return await this.productRepository.UpdateAsyn(product, product.Id);
        }
        
        private class ProductValidator
        {
            public string ErrorMessage { get; private set; }

            public bool IsValidProduct(Product product)
            {
                if (product == null)
                {
                    ErrorMessage = "Product is null";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(product.Name))
                {
                    ErrorMessage = "Invalid Product name";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(product.Description))
                {
                    ErrorMessage = "Invalid Product Description";
                    return false;
                }

                if (product.Price <= 0)
                {
                    ErrorMessage = "Invalid Product Price";
                    return false;
                }

                ErrorMessage = "";
                return true;
            }
        }

        public void Dispose()
        {
            this.productRepository.Dispose();
        }
    }
}