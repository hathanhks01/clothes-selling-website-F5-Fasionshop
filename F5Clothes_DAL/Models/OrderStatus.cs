using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.Models
{
    public enum OrderStatus
    {
        Pending = 1,      // Chờ xác nhận
        Confirmed = 2,    // Đã xác nhận
        Shipping = 3,     // Đang giao
        Delivered = 5,    // Đã giao 
        Cancelled = 6     // Đã hủy 

    }
}
