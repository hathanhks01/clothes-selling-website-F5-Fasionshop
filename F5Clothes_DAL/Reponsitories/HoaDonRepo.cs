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
    public class HoaDonRepo : IHoaDonRepo
    {
        private readonly DbduAnTnContext _context;
        public HoaDonRepo(DbduAnTnContext context)
        {
            _context = context;
        }


        public async Task DeleteHd(Guid Id)
        {
            var Hd = await _context.HoaDons
                .Include(h => h.HoaDonChiTiets)  // Đảm bảo các bản ghi HoaDonChiTiets được tải
                .Include(h => h.HinhThucThanhToans)  // Đảm bảo các bản ghi HinhThucThanhToans được tải
                .Include(h => h.LichSuHoaDons)
                .FirstOrDefaultAsync(h => h.Id == Id);

            if (Hd == null)
            {
                throw new Exception("HoaDon not found");
            }
            _context.LichSuHoaDons.RemoveRange(Hd.LichSuHoaDons);
            // Xóa các bản ghi phụ thuộc trước
            _context.HoaDonChiTiets.RemoveRange(Hd.HoaDonChiTiets);  // Xóa bản ghi HoaDonChiTiet
            _context.HinhThucThanhToans.RemoveRange(Hd.HinhThucThanhToans);  // Xóa bản ghi HinhThucThanhToan

            // Xóa bản ghi HoaDon
            _context.HoaDons.Remove(Hd);

            // Lưu các thay đổi
            await _context.SaveChangesAsync();
        }
        public async Task<List<HoaDon>> GetAllHoaDon()
        {
            return await _context.HoaDons
                         .Include(hd => hd.IdNvNavigation)
                         .Include(hd => hd.IdKhNavigation)
                         .ToListAsync();
        }

        public async Task<HoaDon> GetByHoaDon(Guid id)
        {
            return await _context.HoaDons.FindAsync(id);
        }

        public async Task UpdateHd(HoaDon Hd)
        {
            _context.Entry(Hd).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<int> CountHdByMaHoaDonPrefix(string prefix)
        {
            return await _context.HoaDons
                                 .Where(hd => hd.MaHoaDon.StartsWith(prefix))
                                 .CountAsync();
        }

        public async Task<HoaDon> AddHd(HoaDon hoaDon)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Thiết lập các giá trị mặc định cho hóa đơn mới
                hoaDon.Id = Guid.NewGuid();
                hoaDon.MaHoaDon = await GenerateMaHoaDon();
                hoaDon.NgayTao = DateTime.Now;
                hoaDon.NgayCapNhat = DateTime.Now;
                hoaDon.NgayXacNhan = hoaDon.LoaiHoaDon == 1 ? DateTime.Now : null;
                hoaDon.TrangThai = 1;

                // Tạo danh sách chi tiết hóa đơn mới
                var chiTietList = hoaDon.HoaDonChiTiets.ToList();
                hoaDon.HoaDonChiTiets = null; // Tách chi tiết ra khỏi hóa đơn


                await _context.HoaDons.AddAsync(hoaDon);
                await _context.SaveChangesAsync();

                decimal tongTien = 0;

                // Kiểm tra chi tiết hóa đơn
                if (chiTietList == null || !chiTietList.Any())
                {
                    throw new Exception("Chi tiết hóa đơn là bắt buộc");
                }

                // Xử lý từng chi tiết hóa đơn
                foreach (var chiTiet in chiTietList)
                {
                    var sanPhamChiTiet = await _context.SanPhamChiTiets
                        .Include(sp => sp.IdSpNavigation)
                        .FirstOrDefaultAsync(sp => sp.Id == chiTiet.IdSpct);

                    if (sanPhamChiTiet == null)
                        throw new Exception($"Sản phẩm chi tiết {chiTiet.IdSpct} không tồn tại");

                    if (sanPhamChiTiet.SoLuongTon < chiTiet.SoLuong)
                        throw new Exception($"Sản phẩm {sanPhamChiTiet.Id} không đủ số lượng");

                    // Cập nhật số lượng tồn
                    sanPhamChiTiet.SoLuongTon -= chiTiet.SoLuong;

                    // Tạo chi tiết hóa đơn mới
                    var newChiTiet = new HoaDonChiTiet
                    {
                        Id = Guid.NewGuid(),
                        IdHd = hoaDon.Id,
                        IdSpct = chiTiet.IdSpct,
                        SoLuong = chiTiet.SoLuong,
                        DonGia = sanPhamChiTiet.IdSpNavigation.GiaBan,
                        DonGiaKhiGiam = sanPhamChiTiet.IdSpNavigation.DonGiaKhiGiam,
                        NgayTao = DateTime.Now,
                        TrangThai = 1
                    };

                    await _context.HoaDonChiTiets.AddAsync(newChiTiet);

                    // Tính tổng tiền
                    decimal giaBan = newChiTiet.DonGiaKhiGiam ?? newChiTiet.DonGia ?? 0;
                    tongTien += giaBan * (decimal)newChiTiet.SoLuong;
                }

                // Xử lý voucher
                if (hoaDon.IdVouCher.HasValue)
                {
                    var voucher = await _context.VouChers
                        .FirstOrDefaultAsync(v => v.Id == hoaDon.IdVouCher);

                    if (voucher != null && voucher.GiaTriGiam.HasValue)
                    {
                        tongTien -= voucher.GiaTriGiam.Value;
                        hoaDon.GiaTriGiam = voucher.GiaTriGiam;
                    }
                }
                hoaDon.ThanhTien = tongTien;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return await _context.HoaDons
                    .Include(h => h.HoaDonChiTiets)
                    .FirstOrDefaultAsync(h => h.Id == hoaDon.Id);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<string> GenerateMaHoaDon()
        {
            string prefix = "HD";
            string datePart = DateTime.Now.ToString("yyyyMMdd");
            string randomPart = new Random().Next(1000, 9999).ToString();

            return await Task.FromResult($"{prefix}{datePart}{randomPart}");
        }

        public async Task AddHdgioHang(HoaDon Hd)
        {
            _context.Add(Hd);
            await _context.SaveChangesAsync();
        }
    }
}
