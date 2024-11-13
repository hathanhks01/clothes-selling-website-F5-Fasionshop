using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static F5Clothes_DAL.DTOs.StatisticsDtos;

namespace F5Clothes_DAL.IReponsitories
{
     public interface IStatisticsRepository
    {
        Task<decimal> GetTotalRevenueAsync(DateTime startDate, DateTime endDate);
        Task<int> GetTotalOrdersAsync(DateTime startDate, DateTime endDate);
        Task<int> GetTotalProductsSoldAsync(DateTime startDate, DateTime endDate);
        Task<Dictionary<OrderStatus, int>> GetOrderStatusCountsAsync(DateTime startDate, DateTime endDate);
        Task<List<MonthlyRevenueDto>> GetMonthlyRevenueAsync(int year);
    }
}
