using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class16
{
    public class PaymentService
    {
        public void ProcessPayment(double amount, IPaymentMethod method)
        {
            var processor = new PaymentProcessor(method);
            processor.Pay(amount);
        }
    }

}
