using F5Clothes_DAL.DTOs;

using F5Clothes_Services.IServices;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F5Clothes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        private readonly IVNPayService _vnPayService;

        public VNPayController(IVNPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        /// <summary>
        /// Khởi tạo thanh toán VNPay
        /// </summary>
        /// <param name="paymentRequest">Chi tiết thanh toán</param>
        /// <returns>URL để chuyển hướng tới trang thanh toán VNPay</returns>
        [HttpPost("initiatePayment")]
        public async Task<IActionResult> InitiatePayment([FromBody] HinhThucThanhToanDtos paymentRequest)
        {
            if (paymentRequest == null || paymentRequest.SoTienTra <= 0 || string.IsNullOrEmpty(paymentRequest.MaGiaoDich))
            {
                return BadRequest("Yêu cầu thanh toán không hợp lệ.");
            }

            try
            {
                // Tạo URL thanh toán
                var paymentUrl = await _vnPayService.InitiatePaymentAsync(paymentRequest.SoTienTra.Value, paymentRequest.MaGiaoDich, "http://your_return_url_here");

                return Ok(new { PaymentUrl = paymentUrl });
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần
                return StatusCode(500, "Có lỗi xảy ra trong quá trình khởi tạo thanh toán.");
            }
        }

        /// <summary>
        /// Cập nhật trạng thái thanh toán
        /// </summary>
        /// <param name="transactionId">ID giao dịch cần cập nhật</param>
        /// <param name="status">Trạng thái mới của giao dịch</param>
        /// <returns>Chi tiết giao dịch sau khi cập nhật</returns>
        [HttpPut("updatePaymentStatus/{transactionId}")]
        public async Task<IActionResult> UpdatePaymentStatus(Guid transactionId, [FromBody] int status)
        {
            if (transactionId == Guid.Empty)
            {
                return BadRequest("ID giao dịch không hợp lệ.");
            }

            try
            {
                var updatedTransaction = await _vnPayService.UpdatePaymentStatusAsync(transactionId, status);
                if (updatedTransaction == null)
                {
                    return NotFound("Giao dịch không tìm thấy.");
                }

                return Ok(updatedTransaction);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần
                return StatusCode(500, "Có lỗi xảy ra trong quá trình cập nhật trạng thái thanh toán.");
            }
        }

        /// <summary>
        /// Lấy thông tin giao dịch theo ID
        /// </summary>
        /// <param name="transactionId">ID giao dịch</param>
        /// <returns>Chi tiết giao dịch</returns>
        [HttpGet("transactionDetails/{transactionId}")]
        public async Task<IActionResult> GetTransactionDetails(Guid transactionId)
        {
            if (transactionId == Guid.Empty)
            {
                return BadRequest("ID giao dịch không hợp lệ.");
            }

            try
            {
                var transactionDetails = await _vnPayService.GetTransactionDetailsAsync(transactionId);
                if (transactionDetails == null)
                {
                    return NotFound("Giao dịch không tìm thấy.");
                }

                return Ok(transactionDetails);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần
                return StatusCode(500, "Có lỗi xảy ra trong quá trình lấy thông tin giao dịch.");
            }
        }
    }
}

