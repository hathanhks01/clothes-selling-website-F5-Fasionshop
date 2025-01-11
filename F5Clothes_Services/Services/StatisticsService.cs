using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using F5Clothes_Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static F5Clothes_DAL.DTOs.StaticPageDtos;

namespace F5Clothes_Services.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public StatisticsService(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<decimal> CalculateTotalRevenueAsync(DateTime startDate, DateTime endDate)
        {
            return await _statisticsRepository.GetTotalRevenueAsync(startDate, endDate);
        }

        public async Task<int> CalculateTotalOrdersAsync(DateTime startDate, DateTime endDate)
        {
            return await _statisticsRepository.GetTotalOrdersAsync(startDate, endDate);
        }

        public async Task<StaticPageDtos> CalculateTotalProductsSoldAsync(DateTime startDate, DateTime endDate)
        {
            return await _statisticsRepository.GetTotalProductsSoldAsync(startDate, endDate);
        }

        public async Task<Dictionary<OrderStatus, int>> CalculateOrderStatusCountsAsync(DateTime startDate, DateTime endDate)
        {
            return await _statisticsRepository.GetOrderStatusCountsAsync(startDate, endDate);
        }

        public async Task<List<MonthlyRevenueDto>> GetMonthlyRevenueAsync(int year)
        {
            return await _statisticsRepository.GetMonthlyRevenueAsync(year);
        }

        public async Task<int> GetTotalCustomersAsync()
        {
            return await _statisticsRepository.GetTotalCustomersAsync();
        }

        //// Phương thức lấy tất cả các đơn hàng
        //public async Task<List<HoaDon>> GetAllOrdersAsync()
        //{
        //    return await _statisticsRepository.GetAllOrdersAsync();
        //}

        //// Phương thức lấy tất cả các chi tiết đơn hàng
        //public async Task<List<HoaDonChiTiet>> GetAllOrderDetailsAsync()
        //{
        //    return await _statisticsRepository.GetAllOrderDetailsAsync();
        //}

    }
}
