using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;

using F5Clothes_Services.IServices;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.Services
{
    public class VNPayService:IVNPayService
    {
        private readonly VNPay _vnpay;
        private readonly IHinhThucThanhToanRepo _vnpayRepository;

        public VNPayService(VNPay vnpay, IHinhThucThanhToanRepo vnpayRepository)
        {
            _vnpay = vnpay;
            _vnpayRepository = vnpayRepository;
        }

        public async Task<string> InitiatePaymentAsync(decimal amount, string orderId, string returnUrl)
        {
            // Create new transaction in database
            var transaction = new HinhThucThanhToanDtos
            {
                IdHd = Guid.NewGuid(), // Assuming IdHd represents order ID
                SoTienTra = amount,
                TrangThai = 0 // Pending payment
            };

            await _vnpayRepository.CreateTransactionAsync(transaction);

            // Generate VNPay payment URL
            var paymentUrl = _vnpay.CreatePaymentUrl(amount, orderId, returnUrl);
            return paymentUrl;
        }

        public async Task<HinhThucThanhToanDtos> UpdatePaymentStatusAsync(Guid transactionId, int status)
        {
            // Update the payment status
            return await _vnpayRepository.UpdateTransactionAsync(transactionId, status);
        }

        public async Task<HinhThucThanhToanDtos> GetTransactionDetailsAsync(Guid transactionId)
        {
            // Retrieve transaction details by ID
            return await _vnpayRepository.GetTransactionByIdAsync(transactionId);
        }
    }
}
