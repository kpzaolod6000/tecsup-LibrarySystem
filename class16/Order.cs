using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class16
{
    public interface IOrder
    {
        Guid Id { get; }
        IReadOnlyList<Product> Items { get; }
        double Total { get; }
        bool IsCancelled { get; }
        void Add(Product product);
        void Remove(Product product);
        void Cancel();
    }

    public class Order : IOrder
    {
        private readonly List<Product> _items = new List<Product>();
        public Guid Id { get; } = Guid.NewGuid();
        public IReadOnlyList<Product> Items => _items.AsReadOnly();
        public bool IsCancelled { get; private set; }
        public double Total => _items.Sum(p => p.Price);
        public Order() => IsCancelled = false;

        public void Add(Product product)
        {
            if (IsCancelled || product.Stock <= 0) return;
            _items.Add(product);
            product.Stock--;
        }

        public void Remove(Product product)
        {
            if (IsCancelled) return;
            _items.Remove(product);
            product.Stock++;
        }

        public void Cancel()
        {
            if (IsCancelled) return;
            IsCancelled = true;
            foreach (var p in _items) p.Stock++;
            _items.Clear();
            Console.WriteLine("Orden cancelada y stock repuesto.");
        }
    }

}
