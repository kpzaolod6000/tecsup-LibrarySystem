using class16.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class16
{
    public class Provider : User, IProvider, IProviderDataInput, IValidator
    {
        private readonly IProductRepository _repo;
        private ProviderInfo _info = new ProviderInfo();
        public Provider(IProductRepository repo) => _repo = repo;
        public void InputData(string name, string email, string personalId) => _info = new ProviderInfo { Name = name, Email = email, PersonalId = personalId };
        public void InputData(string companyName, string contactEmail, string rucId, string legalAddress) => _info = new ProviderInfo { Name = companyName, Email = contactEmail, RucId = rucId, LegalAddress = legalAddress };
        public bool Validate() => !string.IsNullOrWhiteSpace(_info.Name) && !string.IsNullOrWhiteSpace(_info.Email);
        public override void Print() => Console.WriteLine($"[Provider] Id={Id}, Name={_info.Name}, Email={_info.Email}");
        public void ProvideProduct(string product) => Console.WriteLine($"Proveedor agrega producto: {product}");
        public void ProvideStock(Guid productId, int quantity)
        {
            if (!Validate()) return;
            Print(); var prod = _repo.GetById(productId); if (prod == null) return; prod.Stock += quantity; _repo.Update(prod);
        }
    }
}
