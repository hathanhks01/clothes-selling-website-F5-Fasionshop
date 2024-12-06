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
    public class HoaDonRepo: IHoaDonRepo
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
       .Include(hd => hd.IdVouCherNavigation)
       .Include(hd => hd.HinhThucThanhToans)
       .Include(hd => hd.LichSuHoaDons)
       .Include(hd => hd.HoaDonChiTiets)
           .ThenInclude(hdt => hdt.IdSpctNavigation)
               .ThenInclude(spct => spct.IdSpNavigation) // Ensure product name is included
       .Select(hd => new HoaDon
       {
           Id = hd.Id,
           IdNv = hd.IdNv,
           IdKh = hd.IdKh,
           IdVouCher = hd.IdVouCher,
           MaHoaDon = hd.MaHoaDon,
           NgayTao = hd.NgayTao,
           NgayCapNhat = hd.NgayCapNhat,
           NgayXacNhan = hd.NgayXacNhan,
           NgayChoGiaoHang = hd.NgayChoGiaoHang,
           NgayGiaoHang = hd.NgayGiaoHang,
           DonViGiaoHang = hd.DonViGiaoHang,
           TenNguoiGiao = hd.TenNguoiGiao,
           SdtnguoiGiao = hd.SdtnguoiGiao,
           TienGiaoHang = hd.TienGiaoHang,
           NgayNhanHang = hd.NgayNhanHang,
           TenNguoiNhan = hd.TenNguoiNhan,
           SdtnguoiNhan = hd.SdtnguoiNhan,
           EmailNguoiNhan = hd.EmailNguoiNhan,
           DiaChiNhanHang = hd.DiaChiNhanHang,
           NgayThanhToan = hd.NgayThanhToan,
           NgayHuy = hd.NgayHuy,
           GiaTriGiam = hd.GiaTriGiam,
           TienKhachTra = hd.TienKhachTra,
           TienThua = hd.TienThua,
           ThanhTien = hd.ThanhTien,
           GhiChu = hd.GhiChu,
           LoaiHoaDon = hd.LoaiHoaDon,
           TrangThai = hd.TrangThai,
           IdKhNavigation = hd.IdKhNavigation == null ? null : new KhachHang
           {
               Id = hd.IdKhNavigation.Id,
               HoVaTenKh = hd.IdKhNavigation.HoVaTenKh
           },
           IdNvNavigation = hd.IdNvNavigation == null ? null : new NhanVien
           {
               Id = hd.IdNvNavigation.Id,
               HoVaTenNv = hd.IdNvNavigation.HoVaTenNv
           },
           HoaDonChiTiets = hd.HoaDonChiTiets.Select(hdt => new HoaDonChiTiet
           {
               Id = hdt.Id,
               IdHdNavigation = hdt.IdHdNavigation,
               IdSpct = hdt.IdSpct,
               SoLuong = hdt.SoLuong,
               DonGia = hdt.DonGia,
               IdSpctNavigation = hdt.IdSpctNavigation == null ? null : new SanPhamChiTiet
               {
                   Id = hdt.IdSpctNavigation.Id,
                   IdSp = hdt.IdSpctNavigation.IdSp,
                   MoTa = hdt.IdSpctNavigation.MoTa,
                   IdSpNavigation = hdt.IdSpctNavigation.IdSpNavigation == null ? null : new SanPham
                   {
                       Id = hdt.IdSpctNavigation.IdSpNavigation.Id,
                       ImageDefaul=hdt.IdSpctNavigation.IdSpNavigation.ImageDefaul,
                   }
               }
           }).ToList(),
           IdVouCherNavigation = hd.IdVouCherNavigation
       })
       .ToListAsync();     
    }

        public async Task<object> GetByHoaDon(Guid id)
        {

            var result = await _context.HoaDons
                .Where(hd => hd.Id == id)
                .Select(hd => new
                {
                    hd.Id,
                    hd.MaHoaDon,
                    hd.NgayTao,
                    KhachHang = hd.IdKhNavigation == null ? null : new
                    {
                        hd.IdKhNavigation.Id,
                        hd.IdKhNavigation.HoVaTenKh,
                        hd.IdKhNavigation.SoDienThoai,
                        hd.IdKhNavigation.Email,
                        hd.IdKhNavigation.DiaChis
                    },  // Thông tin khách hàng
                    NhanVien = hd.IdNvNavigation == null ? null : new
                    {
                        hd.IdNvNavigation.Id,
                        hd.IdNvNavigation.HoVaTenNv,
                        hd.IdNvNavigation.SoDienThoai,
                        hd.IdNvNavigation.Email
                    },  // Thông tin nhân viên
                    HoaDonChiTiets = hd.HoaDonChiTiets
                        .Where(hdct => hdct.IdHd == hd.Id)
                        .Select(hdct => new
                        {
                            hdct.Id,
                            hdct.IdSpct,
                            SanPham = hdct.IdSpctNavigation == null ? null : new
                            {
                                hdct.IdSpctNavigation.Id,
                                TenSanPham = hdct.IdSpctNavigation.IdSpNavigation != null
                                    ? hdct.IdSpctNavigation.IdSpNavigation.TenSp
                                    : null,  // Tên sản phẩm
                                GiaBan = hdct.IdSpctNavigation.IdSpNavigation != null
                                    ? hdct.IdSpctNavigation.IdSpNavigation.GiaBan
                                    : 0  // Giá bán
                            },  // Thông tin sản phẩm trong chi tiết hóa đơn
                            hdct.SoLuong,
                            hdct.DonGia,
                            ThanhTien = hdct.SoLuong * hdct.DonGia  // Thành tiền của sản phẩm
                        })
                        .ToList(),
                    TongTien = hd.HoaDonChiTiets.Sum(hdct => hdct.SoLuong * hdct.DonGia),  // Tổng tiền của hóa đơn
                    TinhTrangThanhToan = hd.TrangThai // Trạng thái thanh toán của hóa đơn
                })
                .FirstOrDefaultAsync();

            return result;
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
                hoaDon.TrangThai = hoaDon.TrangThai;

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
        public async Task<bool> UpdateHd(HoaDon Hd)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Find the existing invoice
                var existingHoaDon = await _context.HoaDons
                    .Include(h => h.HoaDonChiTiets)
                    .Include(h => h.LichSuHoaDons)
                    .FirstOrDefaultAsync(h => h.Id == Hd.Id);

                if (existingHoaDon == null)
                {
                    throw new Exception("Hóa đơn không tồn tại");
                }

                // Update basic invoice information
                UpdateInvoiceBasicInfo(existingHoaDon, Hd);

                // Update delivery information
                UpdateDeliveryInfo(existingHoaDon, Hd);

                // Update financial information
                UpdateFinancialInfo(existingHoaDon, Hd);

                // Update invoice details
                await UpdateHoaDonChiTiets(existingHoaDon, Hd);

                // Track status changes
                TrackStatusChanges(existingHoaDon, Hd);

                // Update last modified date
                existingHoaDon.NgayCapNhat = DateTime.Now;

                // Save changes
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                // Log the exception
                return false;
            }
        }
        private void UpdateInvoiceBasicInfo(HoaDon existingHoaDon, HoaDon hoaDon)
        {
            // Update staff, customer, and voucher
            existingHoaDon.IdNv = hoaDon.IdNv ?? existingHoaDon.IdNv;
            existingHoaDon.IdKh = hoaDon.IdKh ?? existingHoaDon.IdKh;
            existingHoaDon.IdVouCher = hoaDon.IdVouCher ?? existingHoaDon.IdVouCher;

            // Update time-related fields
            existingHoaDon.NgayXacNhan = hoaDon.NgayXacNhan ?? existingHoaDon.NgayXacNhan;
            existingHoaDon.NgayChoGiaoHang = hoaDon.NgayChoGiaoHang ?? existingHoaDon.NgayChoGiaoHang;
            existingHoaDon.NgayGiaoHang = hoaDon.NgayGiaoHang ?? existingHoaDon.NgayGiaoHang;
            existingHoaDon.NgayNhanHang = hoaDon.NgayNhanHang ?? existingHoaDon.NgayNhanHang;
            existingHoaDon.NgayThanhToan = hoaDon.NgayThanhToan ?? existingHoaDon.NgayThanhToan;
            existingHoaDon.NgayHuy = hoaDon.NgayHuy ?? existingHoaDon.NgayHuy;

            // Update other basic fields
            existingHoaDon.GhiChu = hoaDon.GhiChu ?? existingHoaDon.GhiChu;
            existingHoaDon.LoaiHoaDon = hoaDon.LoaiHoaDon ?? existingHoaDon.LoaiHoaDon;
            existingHoaDon.TrangThai = hoaDon.TrangThai ?? existingHoaDon.TrangThai;
        }

        private void UpdateDeliveryInfo(HoaDon existingHoaDon, HoaDon hoaDon)
        {
            // Update delivery company information
            existingHoaDon.DonViGiaoHang = hoaDon.DonViGiaoHang ?? existingHoaDon.DonViGiaoHang;
            existingHoaDon.TenNguoiGiao = hoaDon.TenNguoiGiao ?? existingHoaDon.TenNguoiGiao;
            existingHoaDon.SdtnguoiGiao = hoaDon.SdtnguoiGiao ?? existingHoaDon.SdtnguoiGiao;
            existingHoaDon.TienGiaoHang = hoaDon.TienGiaoHang ?? existingHoaDon.TienGiaoHang;

            // Update recipient information
            existingHoaDon.TenNguoiNhan = hoaDon.TenNguoiNhan ?? existingHoaDon.TenNguoiNhan;
            existingHoaDon.SdtnguoiNhan = hoaDon.SdtnguoiNhan ?? existingHoaDon.SdtnguoiNhan;
            existingHoaDon.EmailNguoiNhan = hoaDon.EmailNguoiNhan ?? existingHoaDon.EmailNguoiNhan;
            existingHoaDon.DiaChiNhanHang = hoaDon.DiaChiNhanHang ?? existingHoaDon.DiaChiNhanHang;
        }

        private void UpdateFinancialInfo(HoaDon existingHoaDon, HoaDon hoaDon)
        {
            // Update financial information
            existingHoaDon.GiaTriGiam = hoaDon.GiaTriGiam ?? existingHoaDon.GiaTriGiam;
            existingHoaDon.TienKhachTra = hoaDon.TienKhachTra ?? existingHoaDon.TienKhachTra;
            existingHoaDon.TienThua = hoaDon.TienThua ?? existingHoaDon.TienThua;
            existingHoaDon.ThanhTien = hoaDon.ThanhTien ?? existingHoaDon.ThanhTien;
        }

        private async Task UpdateHoaDonChiTiets(HoaDon existingHoaDon, HoaDon hoaDon)
        {
            if (hoaDon.HoaDonChiTiets == null || !hoaDon.HoaDonChiTiets.Any())
                return;

            foreach (var updateChiTiet in hoaDon.HoaDonChiTiets)
            {
                var existingChiTiet = existingHoaDon.HoaDonChiTiets
                    .FirstOrDefault(x => x.Id == updateChiTiet.Id);

                if (existingChiTiet == null)
                {
                    // If the detail doesn't exist, create a new one
                    var newChiTiet = new HoaDonChiTiet
                    {
                        Id = Guid.NewGuid(),
                        IdHd = existingHoaDon.Id,
                        IdSpct = updateChiTiet.IdSpct,
                        SoLuong = updateChiTiet.SoLuong ?? 0,
                        DonGia = updateChiTiet.DonGia,
                        DonGiaKhiGiam = updateChiTiet.DonGiaKhiGiam,
                        GhiChu = updateChiTiet.GhiChu,
                        TrangThai = updateChiTiet.TrangThai ?? 1,
                        NgayTao = DateTime.Now
                    };

                    existingHoaDon.HoaDonChiTiets.Add(newChiTiet);
                    continue;
                }

                // Update existing detail
                existingChiTiet.IdSpct = updateChiTiet.IdSpct ?? existingChiTiet.IdSpct;
                existingChiTiet.SoLuong = updateChiTiet.SoLuong ?? existingChiTiet.SoLuong;
                existingChiTiet.DonGia = updateChiTiet.DonGia ?? existingChiTiet.DonGia;
                existingChiTiet.DonGiaKhiGiam = updateChiTiet.DonGiaKhiGiam ?? existingChiTiet.DonGiaKhiGiam;
                existingChiTiet.GhiChu = updateChiTiet.GhiChu ?? existingChiTiet.GhiChu;
                existingChiTiet.TrangThai = updateChiTiet.TrangThai ?? existingChiTiet.TrangThai;
                existingChiTiet.NgayCapNhat = DateTime.Now;
            }
        }

        private void TrackStatusChanges(HoaDon existingHoaDon, HoaDon hoaDon)
        {
            // Track status changes in the history
            if (hoaDon.TrangThai.HasValue && hoaDon.TrangThai != existingHoaDon.TrangThai)
            {
                var lichSuHoaDon = new LichSuHoaDon
                {
                    Id = Guid.NewGuid(),
                    IdHd = existingHoaDon.Id,
                    NguoiThaoTac = existingHoaDon.IdNvNavigation?.HoVaTenNv ?? "Hệ thống", // Optional: add the name of the user making the change
                    GhiChu = $"Thay đổi trạng thái từ {existingHoaDon.TrangThai} sang {hoaDon.TrangThai}",
                    TrangThai = hoaDon.TrangThai,
                    NgayTao = DateTime.Now
                };

                existingHoaDon.LichSuHoaDons.Add(lichSuHoaDon);
            }
        }

        public async Task updateStatus(HoaDon Hd)
        {
            var invoiceStatus = await _context.HoaDons.FindAsync(Hd.Id);
            if (invoiceStatus != null)
            {
                try
                {
                    invoiceStatus.TrangThai = Hd.TrangThai;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    // Log the error (ex) here
                    throw new Exception("An error occurred while updating the invoice status.", ex);
                }
            }
            else
            {
                throw new KeyNotFoundException("Invoice not found.");
            }
        }
    }
}
