using class16.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class16
{
    public class Admin : User, IAdmin, IAdminDataInput, IValidator
    {
        private readonly IProductRepository _repo;
        private AdminInfo _info = new AdminInfo();
        public Admin(IProductRepository repo) => _repo = repo;
        public void InputData(string name, string email) => _info = new AdminInfo { Name = name, Email = email };
        public bool Validate() => !string.IsNullOrWhiteSpace(_info.Name) && !string.IsNullOrWhiteSpace(_info.Email);
        public override void Print() => Console.WriteLine($"[Admin] Id={Id}, Name={_info.Name}, Email={_info.Email}");
        public void CreateProduct(Product p) { if (!Validate()) return; Print(); _repo.Create(p); }
        public void UpdateProduct(Product p) { if (!Validate()) return; Print(); _repo.Update(p); }
    }
}
