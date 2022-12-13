using AutoMapper;
using lifeEcommerce.Data.UnitOfWork;
using lifeEcommerce.Models;
using lifeEcommerce.Models.Entities;
using lifeEcommerce.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace lifeEcommerce.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StatisticService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        //1. User that has most orders from table OrderData include user data from table User
        public async Task<User> UserWithMostOrders()
        {
            var user = await _unitOfWork.Repository<OrderData>().GetAll()
                .Include(x => x.User)
                .GroupBy(x => x.User)
                .OrderByDescending(x => x.Count())
                .Select(x => x.Key).FirstOrDefaultAsync();

            return user;
        }
        

        //2. User that spend most money from table OrderDetails include  orderdata table and user data from table User
        public async Task<User> UserWithMostMoneySpent()
        {
            var user = await _unitOfWork.Repository<OrderDetails>().GetAll()
                .Include(x => x.OrderData.User)
                .GroupBy(x => x.OrderData.User)
                .OrderByDescending(x => x.Sum(y => y.Price))
                .Select(x => x.Key).FirstOrDefaultAsync();

            return user;
        }


        //3. The day with most orders and display details about it
        //if there are days with same number of orders display the first one,
        //*be careful when grouping by OrderDate, ignore time otherwise it will be 1 to 1 always
        public async Task<DateTime> BestDay()
        {
            var bestDay = await _unitOfWork.Repository<OrderData>().GetAll()
                .GroupBy(x => x.OrderDate.Date)
                .OrderByDescending(x => x.Count())
                .Select(x => x.Key).FirstOrDefaultAsync();

            return bestDay;
        }


        //4. The day with the least orders
        public async Task<DateTime> WorstDay()
        {
            var worstDay = await _unitOfWork.Repository<OrderData>().GetAll()
                .GroupBy(x => x.OrderDate.Date)
                .OrderBy(x => x.Count())
                .Select(x => x.Key).FirstOrDefaultAsync();

            return worstDay;
        }


        //5.The most expensive order OrderDetails display data regarding it including product name, user name, count and price
        public async Task<OrderDetails> MostExpensiveOrder()
        {
            var order = await _unitOfWork.Repository<OrderDetails>().GetAll()
                .Include(x => x.Product)
                .Include(x => x.OrderData.User)
                .OrderByDescending(x => x.Price)
                .FirstOrDefaultAsync();

            return order;
        }

        
        //6.The cheapest order and display data regarding it including product name, user name, count and price
        public async Task<OrderDetails> CheapestOrder()
        {
            var order = await _unitOfWork.Repository<OrderDetails>().GetAll()
                .Include(x => x.Product)
                .Include(x => x.OrderData.User)
                .OrderBy(x => x.Price)
                .FirstOrDefaultAsync();

            return order;
        }


        //7. Most sold product
        public async Task<Product> MostSoldProduct()
        {
            var product = await _unitOfWork.Repository<OrderDetails>().GetAll()
                .GroupBy(x => x.Product)
                .OrderByDescending(x => x.Count())
                .Select(x => x.Key).FirstOrDefaultAsync();

            return product;
        }


        //8. Least sold product(from those that have been sold, do not include product that never had an order
        public async Task<Product> LeastSoldProduct()
        {
            var product = await _unitOfWork.Repository<OrderDetails>().GetAll()
                .GroupBy(x => x.Product)
                .OrderBy(x => x.Count())
                .Select(x => x.Key).FirstOrDefaultAsync();

            return product;
        }


        //9. Display data that shows how many orders are there based on
        //OrderStatus, count them and display the result
        //Shipped, Approved, Created
        public async Task<List<OrderData>> GetStatistic()
        {
            var orderData = await _unitOfWork.Repository<OrderData>().GetAll()
                .GroupBy(x => x.OrderStatus)
                .Select(x => new OrderData
                {
                    OrderStatus = x.Key,
                    OrderId = Convert.ToString(x.Count())
                }).ToListAsync();

            return orderData;
        }


        //public async Task<Statistic> GetMetrix()
        //{
        //    var orders = await _unitOfWork.Repository<OrderDetails>()
        //                                                           .GetAll()
        //                                                           .Include(x => x.Product)
        //                                                           .Include(x => x.OrderData)
        //                                                           .ToListAsync();

        //    var leastSoldProduct = orders.GroupBy(x => x.Product)
        //                                 .OrderBy(x => x.Count())
        //                                 .Select(x => x.Key)
        //                                 .FirstOrDefault();

        //    var mostSoldProduct = orders.GroupBy(x => x.Product)
        //                                .OrderByDescending(x => x.Count())
        //                                    .Select(x => x.Key)
        //                                    .FirstOrDefault();


        //    var statistic = new Statistic
        //    {
        //        LeastSoldProduct = leastSoldProduct,
        //        MostSoldProduct = mostSoldProduct
        //    };

        //    return statistic;
        //}

    }

}
