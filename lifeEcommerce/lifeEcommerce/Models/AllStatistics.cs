using lifeEcommerce.Models.Entities;

namespace lifeEcommerce.Models
{
    public class AllStatistics
    {
        public User UserWithMostOrders { get; set; }
        public User UserWithMostMoneySpent { get; set; }
        public DateTime BestDay { get; set; }
        public DateTime WorstDay { get; set; }
        public OrderDetails MostExpensiveOrder { get; set; }
        public OrderDetails CheapestOrder { get; set; }
        public Product MostSoldProduct { get; set; }
        public Product LeastSoldProduct { get; set; }
        public List<OrderData> GetStatistic { get; set; }
}
}
