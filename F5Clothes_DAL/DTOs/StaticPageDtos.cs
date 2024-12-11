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
        // DTO cho doanh thu theo tháng
        public class MonthlyRevenueDto
        {
            public int Month { get; set; }
            public decimal Revenue { get; set; }
        }

        // DTO cho số lượng đơn hàng theo trạng thái
        public class OrderStatusCountDto
        {
            public OrderStatus Status { get; set; }
            public int Count { get; set; }
        }

    }
}
