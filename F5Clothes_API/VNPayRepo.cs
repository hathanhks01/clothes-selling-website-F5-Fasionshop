using F5Clothes_DAL.Models;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace F5Clothes_API
{
    public class VNPaySettings
    {
        public string VnpUrl { get; set; }
        public string VnpTmnCode { get; set; }
        public string VnpHashSecret { get; set; }
        public string VnpReturnUrl { get; set; }
    }
    public class VNPayRepo
    {
        private readonly VNPaySettings _settings;
        private readonly DbduAnTnContext _context;

        public VNPayRepo(DbduAnTnContext context, IConfiguration configuration)
        {
            // Đọc cấu hình từ file appsettings.json
            _settings = configuration.GetSection("VNPaySettings").Get<VNPaySettings>();

            _context = context;
        }

        public string CreatePaymentRequest(HinhThucThanhToan thanhToan)
        {
            var vnpayData = new Dictionary<string, string>
            {
                { "vnp_Version", "2.1.0" },
                { "vnp_Command", "pay" },
                { "vnp_TmnCode", _settings.VnpTmnCode },
                { "vnp_TxnRef", thanhToan.MaGiaoDich ?? Guid.NewGuid().ToString() }, // Ensure TxnRef is a string
                { "vnp_Amount", ((int)(thanhToan.SoTienTra ?? 0 * 100)).ToString() }, // Ensure SoTienTra is not null
                { "vnp_CurrCode", "VND" },
                { "vnp_OrderInfo", "Thanh toán đơn hàng " + (thanhToan.IdHd?.ToString() ?? string.Empty) },
                { "vnp_OrderType", "other" },
                { "vnp_ReturnUrl", _settings.VnpReturnUrl },
                { "vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss") }
            };

            var hashData = string.Join("&", vnpayData.Select(kv => kv.Key + "=" + kv.Value));
            var secureHash = CalculateHash(hashData, _settings.VnpHashSecret);

            var paymentUrl = _settings.VnpUrl + "?" + hashData + "&vnp_SecureHash=" + secureHash;
            return paymentUrl;
        }

        private string CalculateHash(string data, string secretKey)
        {
            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(secretKey)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hash).Replace("-", "").ToUpper();
            }
        }
    }
}
