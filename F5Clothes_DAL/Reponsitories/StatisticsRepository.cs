using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static F5Clothes_DAL.DTOs.StatisticsDtos;

namespace F5Clothes_DAL.Reponsitories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly DbduAnTnContext _context;
        private readonly ILogger<StatisticsRepository> _logger;

        public StatisticsRepository(DbduAnTnContext context, ILogger<StatisticsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Phương thức tính tổng doanh thu trong khoảng thời gian
        public async Task<decimal> GetTotalRevenueAsync(DateTime startDate, DateTime endDate)
        {
            _logger.LogInformation($"Original StartDate: {startDate}, EndDate: {endDate}");

            // Lấy ngày lớn nhất và nhỏ nhất trong cơ sở dữ liệu
            var minDate = await _context.HoaDons.MinAsync(hd => hd.NgayThanhToan);
            var maxDate = await _context.HoaDons.MaxAsync(hd => hd.NgayThanhToan);

            // Điều chỉnh startDate và endDate nếu cần
            if (startDate < minDate) startDate = minDate ?? startDate;
            if (endDate > maxDate) endDate = maxDate ?? endDate;

            _logger.LogInformation($"Adjusted StartDate: {startDate}, EndDate: {endDate}");

            var revenue = await _context.HoaDons
                .Where(hd => hd.TrangThai.HasValue
                             && (OrderStatus)hd.TrangThai.Value == OrderStatus.Delivered
                             && hd.NgayThanhToan >= startDate
                             && hd.NgayThanhToan <= endDate)
                .SumAsync(hd => hd.ThanhTien ?? 0);

            _logger.LogInformation($"Total Revenue: {revenue}");
            return revenue;
        }

        // Phương thức đếm tổng số đơn hàng trong khoảng thời gian
        public async Task<int> GetTotalOrdersAsync(DateTime startDate, DateTime endDate)
        {
            _logger.LogInformation($"Original StartDate: {startDate}, EndDate: {endDate}");

            // Lấy ngày lớn nhất và nhỏ nhất trong cơ sở dữ liệu
            var minDate = await _context.HoaDons.MinAsync(hd => hd.NgayTao);
            var maxDate = await _context.HoaDons.MaxAsync(hd => hd.NgayTao);

            // Điều chỉnh startDate và endDate nếu cần
            if (startDate < minDate) startDate = minDate ?? startDate;
            if (endDate > maxDate) endDate = maxDate ?? endDate;

            _logger.LogInformation($"Adjusted StartDate: {startDate}, EndDate: {endDate}");

            var totalOrders = await _context.HoaDons
                .Where(hd => hd.NgayTao >= startDate && hd.NgayTao <= endDate)
                .CountAsync();

            _logger.LogInformation($"Total Orders: {totalOrders}");
            return totalOrders;
        }

        // Phương thức tính tổng số lượng sản phẩm đã bán trong khoảng thời gian
        public async Task<int> GetTotalProductsSoldAsync(DateTime startDate, DateTime endDate)
        {
            _logger.LogInformation($"Original StartDate: {startDate}, EndDate: {endDate}");

            // Lấy ngày lớn nhất và nhỏ nhất trong cơ sở dữ liệu
            var minDate = await _context.HoaDonChiTiets
                .Where(hdct => hdct.IdHdNavigation != null && hdct.IdHdNavigation.NgayThanhToan.HasValue)
                .MinAsync(hdct => hdct.IdHdNavigation.NgayThanhToan);
            var maxDate = await _context.HoaDonChiTiets
                .Where(hdct => hdct.IdHdNavigation != null && hdct.IdHdNavigation.NgayThanhToan.HasValue)
                .MaxAsync(hdct => hdct.IdHdNavigation.NgayThanhToan);

            // Điều chỉnh startDate và endDate nếu cần
            if (startDate < minDate) startDate = minDate ?? startDate;
            if (endDate > maxDate) endDate = maxDate ?? endDate;

            _logger.LogInformation($"Adjusted StartDate: {startDate}, EndDate: {endDate}");

            var totalProductsSold = await _context.HoaDonChiTiets
                .Where(hdct => hdct.IdHdNavigation != null
                               && hdct.IdHdNavigation.TrangThai.HasValue
                               && (OrderStatus)hdct.IdHdNavigation.TrangThai.Value == OrderStatus.Delivered
                               && hdct.IdHdNavigation.NgayThanhToan >= startDate
                               && hdct.IdHdNavigation.NgayThanhToan <= endDate)
                .SumAsync(hdct => hdct.SoLuong ?? 0);

            _logger.LogInformation($"Total Products Sold: {totalProductsSold}");
            return totalProductsSold;
        }

        // Phương thức đếm số lượng đơn hàng theo trạng thái trong khoảng thời gian
        public async Task<Dictionary<OrderStatus, int>> GetOrderStatusCountsAsync(DateTime startDate, DateTime endDate)
        {
            _logger.LogInformation($"Original StartDate: {startDate}, EndDate: {endDate}");

            // Lấy ngày lớn nhất và nhỏ nhất trong cơ sở dữ liệu
            var minDate = await _context.HoaDons.MinAsync(hd => hd.NgayTao);
            var maxDate = await _context.HoaDons.MaxAsync(hd => hd.NgayTao);

            // Điều chỉnh startDate và endDate nếu cần
            if (startDate < minDate) startDate = minDate ?? startDate;
            if (endDate > maxDate) endDate = maxDate ?? endDate;

            _logger.LogInformation($"Adjusted StartDate: {startDate}, EndDate: {endDate}");

            var orderStatusCounts = await _context.HoaDons
                .Where(hd => hd.NgayTao >= startDate && hd.NgayTao <= endDate && hd.TrangThai.HasValue)
                .GroupBy(hd => (OrderStatus)hd.TrangThai.Value)
                .ToDictionaryAsync(g => g.Key, g => g.Count());

            _logger.LogInformation($"Order Status Counts: {string.Join(", ", orderStatusCounts.Select(kvp => $"{kvp.Key}: {kvp.Value}"))}");
            return orderStatusCounts;
        }

        // Phương thức tính tổng doanh thu theo tháng cho một năm
        public async Task<List<MonthlyRevenueDto>> GetMonthlyRevenueAsync(int year)
        {
            _logger.LogInformation($"Year: {year}");

            var monthlyRevenue = await _context.HoaDons
                .Where(hd => hd.TrangThai.HasValue && (OrderStatus)hd.TrangThai.Value == OrderStatus.Delivered)
                .GroupBy(hd => hd.NgayThanhToan.Value.Month)
                .Select(g => new MonthlyRevenueDto
                {
                    Month = g.Key,
                    Revenue = g.Sum(hd => hd.ThanhTien ?? 0)
                })
                .ToListAsync();

            _logger.LogInformation($"Monthly Revenue: {string.Join(", ", monthlyRevenue.Select(mr => $"Month {mr.Month}: {mr.Revenue}"))}");
            return monthlyRevenue;
        }
    }
}
