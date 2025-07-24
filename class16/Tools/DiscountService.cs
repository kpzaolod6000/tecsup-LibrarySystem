using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class16
{
    public class DiscountService
    {
        private List<IDiscount> _discounts = new List<IDiscount>();
        public void RegisterDiscount(IDiscount discount) => _discounts.Add(discount);
        public double ApplyAll(double total)
        {
            var result = total;
            foreach (var d in _discounts)
                result = d.Apply(result);
            return result;
        }
    }
}
