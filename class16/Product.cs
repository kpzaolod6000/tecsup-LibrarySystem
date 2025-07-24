using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class16
{
    public abstract class Product
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }

        protected Product(string name, double price, int initialStock = 0)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Stock = initialStock;
        }

        public abstract void PrintDetails();
    }

    public class Book : Product
    {
        public string Author { get; set; }
        public string ISBN { get; set; }

        public Book(string name, double price, string author, string isbn, int stock = 0)
            : base(name, price, stock)
        {
            Author = author;
            ISBN = isbn;
        }

        public override void PrintDetails()
            => Console.WriteLine($"[Book] Id={Id}, Title={Name}, Author={Author}, ISBN={ISBN}, Price={Price:C}, Stock={Stock}");
    }

    public class Notebook : Product
    {
        public int Pages { get; set; }
        public string Size { get; set; }

        public Notebook(string name, double price, int pages, string size, int stock = 0)
            : base(name, price, stock)
        {
            Pages = pages;
            Size = size;
        }

        public override void PrintDetails()
            => Console.WriteLine($"[Notebook] Id={Id}, Name={Name}, Pages={Pages}, Size={Size}, Price={Price:C}, Stock={Stock}");
    }

    public class Pencil : Product
    {
        public string Brand { get; set; }
        public string Hardness { get; set; }

        public Pencil(string name, double price, string brand, string hardness, int stock = 0)
            : base(name, price, stock)
        {
            Brand = brand;
            Hardness = hardness;
        }

        public override void PrintDetails()
            => Console.WriteLine($"[Pencil] Id={Id}, Name={Name}, Brand={Brand}, Hardness={Hardness}, Price={Price:C}, Stock={Stock}");
    }
}
