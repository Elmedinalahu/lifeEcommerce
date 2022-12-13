using lifeEcommerce.Models;
using lifeEcommerce.Models.Entities;

namespace lifeEcommerce.Services.IService
{
    public interface IStatisticService
    {
        Task<User> UserWithMostOrders();
        Task<User> UserWithMostMoneySpent();
        Task<DateTime> BestDay();
        Task<DateTime> WorstDay();
        Task<OrderDetails> MostExpensiveOrder();
        Task<OrderDetails> CheapestOrder();
        Task<Product> MostSoldProduct();
        Task<Product> LeastSoldProduct();
        Task<List<OrderData>> GetStatistic();
        //Task<Statistic> GetMetrix();
    }
}
