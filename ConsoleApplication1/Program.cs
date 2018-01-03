
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;


namespace ConsoleApplication1
{
    static class Program
    {
        static void Main(string[] args)
        {
            const int HOW_MANY = 20;
            const string URL = "http://files.olo.com/pizzas.json";
            IOrderProcessor orderProcessor = new PizzaOrderProcessor();
            JsonToTWebParser jsonParser = new JsonToTWebParser();
            //IEnumerable<RankedPizzaOrder> result = RankIt(URL, HOW_MANY, orderProcessor, jsonParser);
            IEnumerable<RankedPizzaOrder> result = RankItWithAsync(URL, HOW_MANY, orderProcessor, jsonParser);
            DisplayResult(result);
        }

        /*
        private static IEnumerable<RankedPizzaOrder> RankIt(string url, int howMany, IOrderProcessor orderProcessor, JsonToTWebParser jsonParser)
        {
            //string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //IEnumerable<Order> orders = orderProcessor.GetOrders(path + "\\pizzas.json");

            IEnumerable<Order> orders = jsonParser.GetRecordsFromJson<PizzaOrder>(url);
            IEnumerable<RankedPizzaOrder> result = orderProcessor.RankOrders(howMany, orders);
            return result;
        }
        */

        private static IEnumerable<RankedPizzaOrder> RankItWithAsync(string url, int howMany, IOrderProcessor orderProcessor, JsonToTWebParser jsonParser)
        {
            Task<IEnumerable<PizzaOrder>> task = jsonParser.GetRecordsFromJsonAsync<PizzaOrder>(url);
            IEnumerable<RankedPizzaOrder> result = orderProcessor.RankOrders(howMany, task.Result);
            return result;
        }

        private static void DisplayResult(IEnumerable<RankedPizzaOrder> rankedOrders)
        {
            foreach (RankedPizzaOrder rankedPizzaOrders in rankedOrders)
            {
                Console.WriteLine(rankedPizzaOrders);
            }
            Console.ReadKey();
        }
    }
}
