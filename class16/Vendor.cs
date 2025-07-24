using class16.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class16
{
    public class Vendor : User, IVendor, IVendorDataInput, IValidator
    {
        private VendorInfo _info = new VendorInfo();
        public void InputData(string name, string email, string vendorCode) => _info = new VendorInfo { Name = name, Email = email, VendorCode = vendorCode };
        public bool Validate() => !string.IsNullOrWhiteSpace(_info.Name) && !string.IsNullOrWhiteSpace(_info.Email) && !string.IsNullOrWhiteSpace(_info.VendorCode);
        public override void Print() => Console.WriteLine($"[Vendor] Id={Id}, Name={_info.Name}, Email={_info.Email}, Code={_info.VendorCode}");
        public void MakeSale(IOrder order, IPaymentMethod paymentMethod, SaleService service, User buyer, bool online)
        {
            if (!Validate()) return;
            Print(); var sale = service.CreateSale(order, paymentMethod, buyer, this, online);
            sale.AddDiscount(new CouponDiscount(0.15));
            service.ExecuteSale(sale);
        }
    }
}
