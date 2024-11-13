using F5Clothes_Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F5Clothes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("total-revenue")]
        public async Task<IActionResult> GetTotalRevenue(DateTime startDate, DateTime endDate)
        {
            var revenue = await _statisticsService.CalculateTotalRevenueAsync(startDate, endDate);
            return Ok(revenue);
        }

        [HttpGet("total-orders")]
        public async Task<IActionResult> GetTotalOrders(DateTime startDate, DateTime endDate)
        {
            var totalOrders = await _statisticsService.CalculateTotalOrdersAsync(startDate, endDate);
            return Ok(totalOrders);
        }

        [HttpGet("total-products-sold")]
        public async Task<IActionResult> GetTotalProductsSold(DateTime startDate, DateTime endDate)
        {
            var totalProducts = await _statisticsService.CalculateTotalProductsSoldAsync(startDate, endDate);
            return Ok(totalProducts);
        }

        [HttpGet("order-status-counts")]
        public async Task<IActionResult> GetOrderStatusCounts(DateTime startDate, DateTime endDate)
        {
            var statusCounts = await _statisticsService.CalculateOrderStatusCountsAsync(startDate, endDate);
            return Ok(statusCounts);
        }

        [HttpGet("monthly-revenue")]
        public async Task<IActionResult> GetMonthlyRevenue(int year)
        {
            var monthlyRevenue = await _statisticsService.GetMonthlyRevenueAsync(year);
            return Ok(monthlyRevenue);
        }
    }
}
