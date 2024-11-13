using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static F5Clothes_DAL.DTOs.StatisticsDtos;

namespace F5Clothes_Services.IServices
{
    public interface IStatisticsService
    {

        Task<decimal> CalculateTotalRevenueAsync(DateTime startDate, DateTime endDate);
        Task<int> CalculateTotalOrdersAsync(DateTime startDate, DateTime endDate);
        Task<int> CalculateTotalProductsSoldAsync(DateTime startDate, DateTime endDate);
        Task<Dictionary<OrderStatus, int>> CalculateOrderStatusCountsAsync(DateTime startDate, DateTime endDate);
        Task<List<MonthlyRevenueDto>> GetMonthlyRevenueAsync(int year);
    }
}
