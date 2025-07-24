using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace class16.Tools
{
    public class AdminInfo { public string Name = ""; public string Email = ""; }
    public class ProviderInfo { public string Name = ""; public string Email = ""; public string PersonalId = ""; public string RucId = ""; public string LegalAddress = ""; }
    public class ClientInfo { public string Name = ""; public string Email = ""; public string ShippingAddress = ""; }
    public class VendorInfo { public string Name = ""; public string Email = ""; public string VendorCode = ""; }


    public interface IClientDataInput
    {
        void InputData(string name, string email, string shippingAddress);
    }

    public interface IVendorDataInput
    {
        void InputData(string name, string email, string vendorCode);
    }

    public interface IProviderDataInput
    {
        void InputData(string name, string email, string personalId);
        void InputData(string companyName, string contactEmail, string rucId, string legalAddress);
    }

    public interface IAdminDataInput
    {
        void InputData(string name, string email);
    }

    // ROLES
    public interface IClient
    {
        void Purchase(double amount, IPaymentMethod paymentMethod);
        void PlaceOrder(IOrder order, IPaymentMethod paymentMethod, SaleService service, User seller, bool online);
        void CancelOrder(IOrder order);
    }

    public interface IProvider
    {
        void ProvideProduct(string product);
    }

    public interface IVendor
    {
        void MakeSale(IOrder order, IPaymentMethod paymentMethod, SaleService service, User buyer, bool online);
    }

    public interface IAdmin
    {
        void CreateProduct(Product product); 
        void UpdateProduct(Product product); 
    }




}
