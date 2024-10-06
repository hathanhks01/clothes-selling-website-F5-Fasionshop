using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Security.Cryptography;


namespace F5Clothes_DAL.DTOs
{
    public class VNPay
    {
        private const string TmnCode = "B8O6IOC8"; // Your VNPay Merchant ID
        private const string HashSecret = "A5VGV7E82O5TACEEIJSAV15K4OBZ1DDT"; // Your VNPay Secret Key
        private const string VnPayUrl = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html"; // Test URL

        public string CreatePaymentUrl(decimal amount, string orderId, string returnUrl)
        {
            var transactionId = Guid.NewGuid().ToString(); // Unique transaction ID
            var orderInfo = "Payment for order " + orderId; // Order description

            // Build the query parameters
            var vnpParams = new SortedDictionary<string, string>
        {
            { "vnp_Version", "2.1.0" },
            { "vnp_TmnCode", TmnCode },
            { "vnp_Amount", (amount * 100).ToString() }, // Convert to VND
            { "vnp_OrderId", orderId },
            { "vnp_OrderInfo", orderInfo },
            { "vnp_ReturnUrl", returnUrl },
            { "vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss") },
            { "vnp_IpAddr", GetIpAddress() },
            { "vnp_Locale", "vn" },
            { "vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss") } // Set expiration time
        };

            // Create the checksum
            var checksum = GenerateChecksum(vnpParams);
            vnpParams.Add("vnp_SecureHash", checksum);

            // Build the payment URL
            var paymentUrl = VnPayUrl + "?" + ToQueryString(vnpParams);
            return paymentUrl;
        }

        private string GenerateChecksum(SortedDictionary<string, string> parameters)
        {
            var sb = new StringBuilder();
            foreach (var param in parameters)
            {
                sb.Append(param.Key).Append("=").Append(param.Value).Append("&");
            }
            sb.Append("vnp_HashSecret=").Append(HashSecret);
            var hashData = sb.ToString();

            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(hashData));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        private string ToQueryString(SortedDictionary<string, string> parameters)
        {
            var array = new List<string>();
            foreach (var param in parameters)
            {
                array.Add($"{param.Key}={HttpUtility.UrlEncode(param.Value)}");
            }
            return string.Join("&", array);
        }

        private string GetIpAddress()
        {
            // Your logic to get the client's IP address
            return "127.0.0.1"; // Placeholder for local testing
        }
    }
}
