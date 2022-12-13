using lifeEcommerce.Models;
using lifeEcommerce.Models.Entities;
using lifeEcommerce.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace lifeEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : Controller
    {
        private readonly IStatisticService _statisticService;
        private readonly IConfiguration _configuration;

        public StatisticController(IStatisticService statisticService, IConfiguration configuration)
        {
            _statisticService = statisticService;
            _configuration = configuration;
        }

        [HttpGet("AllStatistic")]
        public async Task<IActionResult> AllStatistic()
        {
            var allStatistic = new AllStatistics();

            allStatistic.UserWithMostOrders = await _statisticService.UserWithMostOrders();
            allStatistic.UserWithMostMoneySpent = await _statisticService.UserWithMostMoneySpent();
            allStatistic.BestDay = await _statisticService.BestDay();
            allStatistic.WorstDay = await _statisticService.WorstDay();
            allStatistic.MostExpensiveOrder = await _statisticService.MostExpensiveOrder();
            allStatistic.CheapestOrder = await _statisticService.CheapestOrder();
            allStatistic.MostSoldProduct = await _statisticService.MostSoldProduct();
            allStatistic.LeastSoldProduct = await _statisticService.LeastSoldProduct();
            allStatistic.GetStatistic = await _statisticService.GetStatistic();

            return Ok(allStatistic);
        }


        //[HttpGet("GetMetrix")]
        //public async Task<IActionResult> GetMetrix()
        //{
        //    var metrix = new Statistic();

        //    metrix.MostSoldProduct = await _statisticService.MostSoldProduct();
        //    metrix.LeastSoldProduct = await _statisticService.LeastSoldProduct();

        //    return Ok(metrix);
        //}

    }
}
