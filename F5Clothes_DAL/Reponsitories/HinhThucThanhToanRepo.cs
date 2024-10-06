using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F5Clothes_DAL.Reponsitories
{
    public class HinhThucThanhToanRepo: IHinhThucThanhToanRepo
    {
        private readonly DbduAnTnContext _context;
        public HinhThucThanhToanRepo(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task AddHTt(HinhThucThanhToan HTt)
        {
            _context.Add(HTt);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHTt(Guid Id)
        {
            var HTt = await GetByHinhThucThanhToan(Id);
            _context.Remove(HTt);
            await _context.SaveChangesAsync();
        }

        public async Task<List<HinhThucThanhToan>> GetAllHinhThucThanhToan()
        {
            return await _context.HinhThucThanhToans.ToListAsync();
        }

        public async Task<HinhThucThanhToan> GetByHinhThucThanhToan(Guid id)
        {
            return await _context.HinhThucThanhToans.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateHTt(HinhThucThanhToan HTt)
        {
            _context.Entry(HTt).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<HinhThucThanhToanDtos> CreateTransactionAsync(HinhThucThanhToanDtos transaction)
        {
            var newTransaction = new HinhThucThanhToan
            {
                Id = Guid.NewGuid(),
                IdHd = transaction.IdHd,
                IdKh = transaction.IdKh,
                MaGiaoDich = transaction.MaGiaoDich,
                NgayThanhToan = transaction.NgayThanhToan ?? DateTime.Now,
                SoTienTra = transaction.SoTienTra,
                NgayTao = DateTime.Now,
                TrangThai = transaction.TrangThai ?? 0
            };

            _context.HinhThucThanhToans.Add(newTransaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<HinhThucThanhToanDtos> UpdateTransactionAsync(Guid transactionId, int status)
        {
            var transaction = await _context.HinhThucThanhToans.FirstOrDefaultAsync(t => t.Id == transactionId);
            if (transaction == null) return null;

            transaction.TrangThai = status;
            transaction.NgayCapNhat = DateTime.Now;
            await _context.SaveChangesAsync();

            return new HinhThucThanhToanDtos
            {
                Id = transaction.Id,
                MaGiaoDich = transaction.MaGiaoDich,
                NgayThanhToan = transaction.NgayThanhToan,
                SoTienTra = transaction.SoTienTra,
                TrangThai = transaction.TrangThai
            };
        }

        public async Task<HinhThucThanhToanDtos> GetTransactionByIdAsync(Guid transactionId)
        {
            var transaction = await _context.HinhThucThanhToans.FirstOrDefaultAsync(t => t.Id == transactionId);
            if (transaction == null) return null;

            return new HinhThucThanhToanDtos
            {
                Id = transaction.Id,
                MaGiaoDich = transaction.MaGiaoDich,
                NgayThanhToan = transaction.NgayThanhToan,
                SoTienTra = transaction.SoTienTra,
                TrangThai = transaction.TrangThai
            };
        }
    }
}
