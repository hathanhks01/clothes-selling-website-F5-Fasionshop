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
    public class DiaChiRepo : IDiaChiRepo
    {
        private readonly DbduAnTnContext _context;
        public DiaChiRepo(DbduAnTnContext context)
        {
            _context = context;
        }
        public async Task<DiaChi> Adddc(DiaChiDtos dc)
        {
            // Kiểm tra xem địa chỉ đã tồn tại chưa
            var diaChi = await _context.DiaChis.FirstOrDefaultAsync(d => d.Id == dc.Id);

            if (diaChi == null)
            {
                var diaChis = new DiaChi
                {
                    Id = Guid.NewGuid(),
                    IdKh = dc.IdKh,  // Dùng IdKh từ DTOs để liên kết với khách hàng
                    DiaChiChiTiet = dc.DiaChiChiTiet,
                    GhiChu = dc.GhiChu,
                    QuanHuyen = dc.QuanHuyen,
                    NgayTao = dc.NgayTao,
                    PhuongXa = dc.PhuongXa,
                    QuocGia = dc.QuocGia,
                    TrangThai = dc.TrangThai,
                };

                _context.DiaChis.Add(diaChis);
                await _context.SaveChangesAsync();

                return diaChis;
            }
            else
            {
                diaChi.DiaChiChiTiet = dc.DiaChiChiTiet;
                diaChi.GhiChu = dc.GhiChu;
                diaChi.QuanHuyen = dc.QuanHuyen;
                diaChi.NgayTao = dc.NgayTao;
                diaChi.PhuongXa = dc.PhuongXa;
                diaChi.QuocGia = dc.QuocGia;
                diaChi.TrangThai = dc.TrangThai;

                await _context.SaveChangesAsync();
                return diaChi;
            }

        }

        public async Task Deletedc(Guid Id)
        {
            var dc = await GetByDiaChi(Id);
            _context.Remove(dc);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DiaChi>> GetAllDiaChi()
        {
            return await _context.DiaChis.ToListAsync();
        }

        public async Task<DiaChi> GetByDiaChi(Guid idKH)
        {
            return await _context.DiaChis.FirstOrDefaultAsync(x => x.IdKh == idKH);
        }

        public async Task Updatedc(DiaChi dc)
        {
            _context.Entry(dc).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
