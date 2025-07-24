using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class16
{
    public class SaleService
    {
        private readonly DiscountService _discountService;
        private readonly PaymentService _paymentService;

        public SaleService(DiscountService discountService, PaymentService paymentService)
        {
            _discountService = discountService;
            _paymentService = paymentService;
        }

        public SaleBase CreateSale(IOrder order, IPaymentMethod paymentMethod,
                              User buyer, User seller, bool online)
        {
            if (online)
            {
                return new OnlineSale(order, paymentMethod, buyer, seller);
            }
            else
            {
                return new PresentialSale(order, paymentMethod, buyer, seller);
            }
        }

        public void ExecuteSale(ISale sale)
        {
            if (sale is SaleBase sb)
            {
                var original = sb.Order.Total;
                var global = _discountService.ApplyAll(original);
                Console.WriteLine($"Total tras descuentos globales: {global:C}");
                _paymentService.ProcessPayment(global, sb.PaymentMethod);
            }
            else sale.Execute();
        }
    }

}
