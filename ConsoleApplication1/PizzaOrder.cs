using System.Collections.Generic;
using System.Linq;


namespace ConsoleApplication1
{
    public class PizzaOrder : Order
    {
        public IEnumerable<string> Toppings { get; set; }
        public const string TOPPING_SEPARATOR = ", ";
        public string ToppingsHash
        {
            get
            {
                string hash = string.Empty;
                IOrderedEnumerable<string> sorted = Toppings.OrderBy(t => t);
                foreach (string topping in sorted)
                {
                    if (!string.IsNullOrEmpty(hash))
                    {
                        hash += TOPPING_SEPARATOR;
                    } 
                    hash += topping;
                }
                return hash;
            }
        }

    }
}
