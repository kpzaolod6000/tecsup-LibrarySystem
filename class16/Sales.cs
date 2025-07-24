using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class16
{
    public interface ISale { void Execute(); void Cancel(); }

    public abstract class SaleBase : ISale
    {
        public IOrder Order;
        public IPaymentMethod PaymentMethod;
        public User Buyer;
        public User Seller;
        protected List<IDiscount> SaleDiscounts = new List<IDiscount>();

        public SaleBase(IOrder order, IPaymentMethod payment, User buyer, User seller)
        {
            Order = order;
            PaymentMethod = payment;
            Buyer = buyer;
            Seller = seller;
            Console.WriteLine($"Venta Id={Guid.NewGuid()}, Buyer={Buyer.Id}, Seller={Seller.Id}");
        }

        public void AddDiscount(IDiscount discount) => SaleDiscounts.Add(discount);
        public abstract void Execute();
        public virtual void Cancel()
        {
            Order.Cancel();
            Console.WriteLine("Venta cancelada.");
        }
    }

    public class PresentialSale : SaleBase
    {
        public PresentialSale(IOrder order, IPaymentMethod payment, User buyer, User seller)
            : base(order, payment, buyer, seller)
        {
            Console.WriteLine("Creando venta presencial...");
        }

        public override void Execute()
        {
            var total = SaleDiscounts.Aggregate(Order.Total, (t, d) => d.Apply(t));
            Console.WriteLine($"Total tras descuentos de venta: {total:C}");
            new PaymentProcessor(PaymentMethod).Pay(total);
        }
    }

    public class OnlineSale : SaleBase
    {
        public OnlineSale(IOrder order, IPaymentMethod payment, User buyer, User seller)
            : base(order, payment, buyer, seller)
        {
            Console.WriteLine("Creando venta online...");
        }

        public override void Execute()
        {
            var total = SaleDiscounts.Aggregate(Order.Total, (t, d) => d.Apply(t));
            Console.WriteLine($"Total tras descuentos de venta: {total:C}");
            new PaymentProcessor(PaymentMethod).Pay(total);
        }
    }

}
