using System;
using System.Collections.Generic;


namespace ConsoleApplication1
{
    public interface IOrderProcessor
    {
        IEnumerable<Order> GetOrders(string filePath);
        IEnumerable<RankedPizzaOrder> RankOrders(int numberOfSprotsForRanking, IEnumerable<Order> orders);
    }
}
