using F5Clothes_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.DTOs
{
    public class StaticPageDtos
    {
        public int TotalProductsSold { get; set; } // Add this property to fix the error

        public class MonthlyRevenueDto
        {
            public int Month { get; set; }
            public decimal Revenue { get; set; }
        }
        public class OrderStatusCountDto
        {
            public OrderStatus Status { get; set; }
            public int Count { get; set; }
        }
        public class DailyRevenueDto
        {
            public DateTime Date { get; set; }
            public decimal Revenue { get; set; }
        }
    }
}
