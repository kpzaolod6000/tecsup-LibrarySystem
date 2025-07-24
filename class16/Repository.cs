using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class16
{
    public interface IProductRepository
    {
        void Create(Product product);
        Product GetById(Guid id);
        List<Product> GetAll();
        void Update(Product product);
    }

    public class ProductRepository : IProductRepository
    {
        private List<Product> _storage = new List<Product>();

        public void Create(Product product)
        {
            _storage.Add(product);
            Console.WriteLine($"Producto creado: {product.Name} (Id={product.Id}, Stock={product.Stock})");
        }
        public List<Product> GetAll()
        {
            return _storage;
        }

        public void Update(Product product)
        {
            var existing = GetById(product.Id);
            if (existing == null) return;
            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Stock = product.Stock;
            Console.WriteLine($"Producto actualizado: {existing.Name} (Id={existing.Id}, Stock={existing.Stock})");
        }

        public Product GetById(Guid id) => _storage.FirstOrDefault(p => p.Id == id);
    }
}
