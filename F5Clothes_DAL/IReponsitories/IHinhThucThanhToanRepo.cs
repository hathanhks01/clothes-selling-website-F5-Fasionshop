using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.IReponsitories
{
    public interface IHinhThucThanhToanRepo
    {
        Task<List<HinhThucThanhToan>> GetAllHinhThucThanhToan();
        Task<HinhThucThanhToan> GetByHinhThucThanhToan(Guid id);
        Task AddHTt(HinhThucThanhToan HTt);
        Task UpdateHTt(HinhThucThanhToan HTt);
        Task DeleteHTt(Guid Id);
        Task<HinhThucThanhToanDtos> CreateTransactionAsync(HinhThucThanhToanDtos transaction);
        Task<HinhThucThanhToanDtos> UpdateTransactionAsync(Guid transactionId, int status);
        Task<HinhThucThanhToanDtos> GetTransactionByIdAsync(Guid transactionId);
    }
}
