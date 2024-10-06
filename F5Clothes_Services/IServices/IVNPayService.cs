using F5Clothes_DAL.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_Services.IServices
{
    public interface IVNPayService
    {
        Task<string> InitiatePaymentAsync(decimal amount, string orderId, string returnUrl);
        Task<HinhThucThanhToanDtos> UpdatePaymentStatusAsync(Guid transactionId, int status);
        Task<HinhThucThanhToanDtos> GetTransactionDetailsAsync(Guid transactionId);
    }
}
