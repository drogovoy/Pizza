
namespace ConsoleApplication1
{
    public class RankedPizzaOrder : PizzaOrder
    {
        public int Rank { get; set; }
        public int OrderedTimes { get; set; }
        
        public override string ToString()
        {
            return $"{Rank}. Ordered {OrderedTimes} times. Toppings: {ToppingsHash}";
        }
    }
}
