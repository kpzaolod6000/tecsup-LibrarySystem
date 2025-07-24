using class16.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace class16
{
    public class Client : User, IClient, IClientDataInput, IValidator
    {
        private ClientInfo _info = new ClientInfo();
        public void InputData(string name, string email, string shippingAddress) => _info = new ClientInfo { Name = name, Email = email, ShippingAddress = shippingAddress };
        public bool Validate() => !string.IsNullOrWhiteSpace(_info.Name) && !string.IsNullOrWhiteSpace(_info.Email);
        public override void Print() => Console.WriteLine($"[Client] Id={Id}, Name={_info.Name}, Email={_info.Email}, Address={_info.ShippingAddress}");
        public void Purchase(double amount, IPaymentMethod paymentMethod) { if (!Validate()) return; Print(); new PaymentProcessor(paymentMethod).Pay(amount); }
        public void PlaceOrder(IOrder order, IPaymentMethod paymentMethod, SaleService service, User seller, bool online)
        {
            if (!Validate()) return;
            Print(); var sale = service.CreateSale(order, paymentMethod, this, seller, online);
            sale.AddDiscount(new ClientTypeDiscount());
            service.ExecuteSale(sale);
        }
        public void CancelOrder(IOrder order) { if (!Validate()) return; Print(); order.Cancel(); }
    }
}
