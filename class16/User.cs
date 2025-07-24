using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class16
{
    public abstract class User
    {
        private static int _nextId;
        public int Id { get; }
        protected User() => Id = ++_nextId; // Parameterless constructor
        public virtual void Print() => Console.WriteLine($"[User] Id={Id}");
    }
    //protected User() => Id = ++_nextId;
    //    public virtual void Print() => Console.WriteLine($"[User] Id={Id}");
    //}


}
