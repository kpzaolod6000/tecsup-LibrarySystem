using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class16
{
    public interface IDiscount
    {
        double Apply(double total);
    }

    public abstract class DiscountBase : IDiscount
    {
        protected double Rate;
        protected DiscountBase(double rate) => Rate = rate;
        public virtual double Apply(double total) => total - total * Rate;
    }

    public class ClientTypeDiscount : DiscountBase
    {
        public ClientTypeDiscount() : base(0.10) { }
    }

    public class CouponDiscount : DiscountBase
    {
        public CouponDiscount(double rate) : base(rate) { }
    }

    public class SummerSeasonDiscount : DiscountBase
    {
        public SummerSeasonDiscount() : base(0.20) { }
    }

}
