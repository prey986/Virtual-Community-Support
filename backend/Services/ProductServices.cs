using backend.Models;
using System.Xml.Linq;

namespace backend.Services
{
    public class ProductServices
    {
        public interface IProductService
        {
            List<Product> GetAll();
            Product? GetById(int id);
            Product Add(Product product);
            Product? Update(int id, Product product);
            bool Delete(int id);
        }

        public class ProductService : IProductService
        {
            private static List<Product> _products = new List<Product>();
            private static int _nextId = 1;

            public List<Product> GetAll() => _products;

            public Product? GetById(int id) =>
                _products.FirstOrDefault(p => p.Id == id);

            public Product Add(Product product)
            {
                product.Id = _nextId++;
                _products.Add(product);
                return product;
            }

            public Product? Update(int id, Product product)
            {
                var existing = GetById(id);
                if (existing == null) return null;

                existing.Productname = product.Productname;
                existing.Price = product.Price;
                return existing;
            }

            public bool Delete(int id)
            {
                var product = GetById(id);
                if (product == null) return false;
                _products.Remove(product);
                return true;
            }
        }
    }
}
