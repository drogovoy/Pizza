using System.Collections.Generic;

namespace ConsoleApplication1
{
    public class PizzaOrderComparer : IEqualityComparer<PizzaOrder>
    {
        public bool Equals(PizzaOrder x, PizzaOrder y)
        {
            if (x == null || y == null) return false;
            return x.ToppingsHash.Equals(y.ToppingsHash);
        }

        public int GetHashCode(PizzaOrder obj)
        {
            return obj.ToppingsHash.GetHashCode();
        }
    }
}
