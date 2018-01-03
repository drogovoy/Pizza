using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ConsoleApplication1
{
    public class PizzaOrderProcessor : IOrderProcessor
    {
        public IEnumerable<Order> GetOrders(string filePath)
        {
            List<PizzaOrder> orders = null;
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                orders = JsonConvert.DeserializeObject<List<PizzaOrder>>(json);
            }
            return orders;
        }

        public IEnumerable<RankedPizzaOrder> RankOrders(int numberOfSprotsForRanking, IEnumerable<Order> orders)
        {
            if (orders == null || !orders.Any()) return null;
            string[] stringSeparators = { PizzaOrder.TOPPING_SEPARATOR };

          
            IEnumerable<PizzaOrder> pizzaOrders = orders as IEnumerable<PizzaOrder>;
            PizzaOrderComparer comparer = new PizzaOrderComparer();
            IEnumerable<RankedPizzaOrder> result = pizzaOrders
                                                          
                                                            .GroupBy(o => o, comparer)
                                                            .OrderByDescending(g => g.Count())
                                                            .Select((g, index) => new RankedPizzaOrder()
                                                            {
                                                                Rank = index + 1,
                                                                OrderedTimes = g.Count(),
                                                                Toppings = g.Key.Toppings
                                                                
                                                            })
                                                            .Take(numberOfSprotsForRanking)
                                                            .ToList();
            return result;
           
        }

        /*
        //this method can be used without a need for comparer
        public IEnumerable<RankedPizzaOrder> RankOrders(int numberOfSprotsForRanking, IEnumerable<Order> orders)
        {
            if (orders == null || !orders.Any()) return null;
            string[] stringSeparators = new string[] { PizzaOrder.TOPPING_SEPARATOR };

          
            IEnumerable<PizzaOrder> pizzaOrders = orders as IEnumerable<PizzaOrder>;
            IEnumerable<RankedPizzaOrder> result = pizzaOrders
                                                            .GroupBy(o => o.ToppingsHash)
                                                            .OrderByDescending(g => g.Count())
                                                            .Select((g, index) => new RankedPizzaOrder()
                                                            {
                                                                Rank = index + 1,
                                                                OrderedTimes = g.Count(),
                                                                Toppings = g.Key.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries)
                                                                //Toppings = g.SelectMany(o=>o.Toppings).Distinct()
                                                            })
                                                            .Take(numberOfSprotsForRanking)
                                                            .ToList();
            return result;
           
        }
        */
    }
}
