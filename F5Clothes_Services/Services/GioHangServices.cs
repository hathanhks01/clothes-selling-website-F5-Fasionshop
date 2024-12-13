using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using F5Clothes_Services.IServices;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F5Clothes_DAL.Reponsitories;
using Microsoft.EntityFrameworkCore;

namespace F5Clothes_Services.Services
{
    public class GioHangServices : IGioHangServices
    {
        private readonly IGioHangRepo _gioHangRepo;

        private readonly IMapper _mapper;
        private readonly ISPCTRepo _sPCTRepo;
        private readonly IHoaDonRepo _hoaDonRepo;
        private readonly IHDCTRepo _hDCTRepo;
        private readonly IVoucherRepo _voucherRepo;
        private readonly IDiaChiRepo _diaChiRepo;
        private readonly ISanPhamRepo _sanPhamRepo;
        private readonly IHinhThucThanhToanRepo _hinhThucThanhToanRepo;

        public GioHangServices(IGioHangRepo gioHangRepo,
            IMapper mapper, ISPCTRepo sPCTRepo, IHoaDonRepo hoaDonRepo,
            IHDCTRepo hDCTRepo, IVoucherRepo voucherRepo, IDiaChiRepo diaChiRepo,
            ISanPhamRepo sanPhamRepo,IHinhThucThanhToanRepo hinhThucThanhToanRepo)
        {
            _gioHangRepo = gioHangRepo;
            _mapper = mapper;
            _sPCTRepo = sPCTRepo;
            _hoaDonRepo = hoaDonRepo;
            _hDCTRepo = hDCTRepo;
            _voucherRepo = voucherRepo;
            _diaChiRepo = diaChiRepo;
            _sanPhamRepo = sanPhamRepo;
            _hinhThucThanhToanRepo = hinhThucThanhToanRepo;
        }

        // Retrieve all cart items for a customer by ID
        public async Task<List<GiohangDtos>> GetAllGioHangAsync(Guid idKh)
        {

            var gioHangChiTiets = await _gioHangRepo.GetAllGioHangAsync(idKh);

            return _mapper.Map<List<GiohangDtos>>(gioHangChiTiets);
        }

        // Retrieve a specific cart item by its ID
        public async Task<GiohangDtos> GetGioHangByIdAsync(Guid id)
        {
            var gioHangChiTiet = await _gioHangRepo.GetGioHangByIdAsync(id);
            if (gioHangChiTiet == null) throw new Exception("Không có sản phẩm nào trong giỏ hàng.");
            return _mapper.Map<GiohangDtos>(gioHangChiTiet);
        }
        public async Task<decimal?> ApplyVoucherAsync(OrderInfoDto orderInfo, decimal tongTien)
        {
            if (!orderInfo.VoucherId.HasValue)
                return 0;

            var voucher = await _voucherRepo.GetByVouCher(orderInfo.VoucherId.Value);
            if (voucher == null || voucher.TrangThai == 2)
                throw new Exception("Voucher không hợp lệ hoặc đã hết hiệu lực.");

            var now = DateTime.UtcNow; // Or DateTime.Now based on your time zone choice
            if (DateTime.Compare(voucher.NgayBatDau.Value, now) > 0 || DateTime.Compare(voucher.NgayKetThuc.Value, now) < 0)
            {
                throw new Exception("Voucher không còn hiệu lực.");
            }

            // Check if the voucher has enough available quantity
            if (voucher.SoLuongMa > 0)
            {
                // Decrease the available quantity and increase the used quantity
                voucher.SoLuongMa--;
                voucher.SoLuongDung++;
                await _voucherRepo.UpdateVc(voucher);  // Update voucher in the database
            }
            else
            {
                throw new Exception("Voucher đã hết số lượng sử dụng.");
            }

            if (tongTien < (voucher.DieuKienToiThieuHoaDon ?? 0))
                throw new Exception($"Hóa đơn không đạt điều kiện tối thiểu để sử dụng mã giảm giá. Yêu cầu tối thiểu: {voucher.DieuKienToiThieuHoaDon}.");

            decimal? giaTriGiam = 0;
            if (voucher.HinhThucGiam == 1) // Giảm theo phần trăm
            {
                var discountPercent = (voucher.GiaTriGiam ?? 0) / 100m;
                giaTriGiam = tongTien * discountPercent;

                if (giaTriGiam > tongTien)
                    giaTriGiam = tongTien;
            }
            else if (voucher.HinhThucGiam == 2) // Giảm số tiền cố định
            {
                giaTriGiam = voucher.GiaTriGiam;

                if (giaTriGiam > tongTien)
                    giaTriGiam = tongTien;
            }

            return giaTriGiam;
        }

        public async Task PlaceOrderAsync(Guid customerId, OrderInfoDto orderInfo)
        {
            string diaChiNhanHang;
            const string DiaChiCuaHang = "Số 123 Đường ABC, Quận 1, TP. Hồ Chí Minh";

            // Nếu IdDiaChi là GUID mặc định thì sử dụng địa chỉ cố định
            if (orderInfo.IdDiaChi == null)
            {
                diaChiNhanHang = DiaChiCuaHang;
            }
            else
            {
                // Lấy danh sách địa chỉ của khách hàng
                var diaChiList = await _diaChiRepo.GetByDiaChi(customerId);

                if (diaChiList == null || !diaChiList.Any())
                    throw new Exception("Không tìm thấy địa chỉ nào cho khách hàng.");

                // Tìm địa chỉ theo IdDiaChi trong orderInfo, nếu không tìm thấy thì lấy địa chỉ đầu tiên
                var diaChi = diaChiList.FirstOrDefault(x => x.Id == orderInfo.IdDiaChi)
                             ?? diaChiList.FirstOrDefault();

                if (diaChi == null || string.IsNullOrEmpty(diaChi.DiaChiChiTiet))
                    throw new Exception("Không có địa chỉ nhận hàng hợp lệ.");

                diaChiNhanHang = $"{diaChi.DiaChiChiTiet ?? ""}, {diaChi.PhuongXa ?? ""}, {diaChi.QuanHuyen ?? ""}, {diaChi.TinhThanh ?? ""}";
            }


            var cartItems = await _gioHangRepo.GetAllGioHangAsync(customerId);

            if (cartItems == null || !cartItems.Any())
                throw new Exception("Giỏ hàng trống, không thể đặt hàng.");

            decimal tongTien;
            if (cartItems.Any(item => item.DonGiaKhiGiam.HasValue && item.DonGiaKhiGiam.Value > 0))
            {
                tongTien = cartItems.Sum(item => item.SoLuong * (item.DonGiaKhiGiam ?? 0)); // Sử dụng DonGiaKhiGiam nếu có, nếu không thì dùng 0.0m
            }
            else
            {
                tongTien = cartItems.Sum(item => item.SoLuong * (item.DonGia)); // Sử dụng DonGia nếu có, nếu không thì dùng 0.0m
            }

            // Áp dụng mã giảm giá
            decimal? giaTriGiam = await ApplyVoucherAsync(orderInfo, tongTien);
            tongTien -= giaTriGiam ?? 0;

            // Generate MaHoaDon
            var maHoaDon = await _hoaDonRepo.GenerateMaHoaDon();

            var hoaDon = new HoaDon
            {
                MaHoaDon = maHoaDon,
                IdKh = customerId,
                NgayTao = DateTime.Now,
                TrangThai = 1,
                LoaiHoaDon = 2,
                DiaChiNhanHang = diaChiNhanHang, // Gán địa chỉ nhận hàng
                TenNguoiNhan = orderInfo.TenNguoiNhan,
                SdtnguoiNhan = orderInfo.SdtNguoiNhan,
                NgayNhanHang = orderInfo.NgayNhanHang,
                IdVouCher = orderInfo.VoucherId,
                ThanhTien = tongTien,
                GiaTriGiam = giaTriGiam,
                GhiChu = orderInfo.GhiChu


            };

            await _hoaDonRepo.AddHdgioHang(hoaDon);
            var hinhThucThanhToan = new HinhThucThanhToan
            {
                IdHd = hoaDon.Id,
                IdKh = customerId,
                HinhThucThanhToan1 = 1, // Assuming 1 represents "COD"
                TrangThai = 0,  // Assuming 0 means "Not Paid"
                NgayTao = DateTime.Now,
                MaGiaoDich = null,  // Generate a unique transaction code if needed
                GhiChu = "Thanh toán khi nhận hàng"
            };
            await _hinhThucThanhToanRepo.AddHTt(hinhThucThanhToan);

            foreach (var item in cartItems)
            {
                var product = await _sPCTRepo.GetByIdSanPhamChiTiet(item.IdSpct);
                if (product.SoLuongTon < item.SoLuong)
                    throw new Exception($"Không đủ số lượng sản phẩm {product.IdSpNavigation.TenSp} trong kho.");

                await _sPCTRepo.UpdateSanPhamChiTiet(_mapper.Map<SanPhamChiTietDtos>(product));

                var hoaDonChiTiet = new HoaDonChiTiet
                {
                    IdHd = hoaDon.Id,
                    IdSpct = item.IdSpct,
                    SoLuong = item.SoLuong,
                    DonGia = item.DonGia,

                    NgayTao = DateTime.Now,
                    DonGiaKhiGiam = item.DonGiaKhiGiam
                };
                await _hDCTRepo.CreateDatHang(hoaDonChiTiet);

                await _gioHangRepo.DeleteGioHangAsync(item.Id);


            }
        }





        public async Task AddGioHangAsync(AddGioHangDtos addDto)
        {
            // Kiểm tra xem sản phẩm đã có trong giỏ hàng chưa
            var existingCartItem = await _gioHangRepo.GetCartItemByIdsAsync(addDto.IdGh, addDto.IdSpct);

            // Lấy giá sản phẩm chi tiết (DonGia)
            var productPrice = await _gioHangRepo.GetProductPriceAsync(addDto.IdSpct);
            if (productPrice == 0)
            {
                throw new Exception("Sản phẩm không tồn tại hoặc không có giá.");
            }

            // Lấy sản phẩm chi tiết và thông tin sản phẩm
            var productDetail = await _sPCTRepo.GetByIdSanPhamChiTiet(addDto.IdSpct);
            if (productDetail == null || !productDetail.IdSp.HasValue)
            {
                throw new Exception("Không tìm thấy thông tin sản phẩm.");
            }

            var product = await _sanPhamRepo.GetByIdSanPham(productDetail.IdSp.Value);
            if (product == null)
            {
                throw new Exception("Không tìm thấy sản phẩm.");
            }

            // Kiểm tra số lượng tồn kho
            if (productDetail.SoLuongTon < addDto.SoLuong + (existingCartItem?.SoLuong ?? 0))
            {
                throw new Exception($"Không đủ số lượng sản phẩm {product.TenSp} trong kho.");
            }

            // Lấy giá khi giảm
            decimal? donGiaKhiGiam = product.DonGiaKhiGiam ?? productPrice;

            if (existingCartItem != null)
            {
                // Nếu sản phẩm đã tồn tại, cộng dồn số lượng và cập nhật giá trị
                existingCartItem.SoLuong += addDto.SoLuong;
                existingCartItem.DonGia = productPrice;
                existingCartItem.DonGiaKhiGiam = donGiaKhiGiam;
                existingCartItem.NgayCapNhat = DateTime.Now;

                // Cập nhật giỏ hàng trong repository
                await _gioHangRepo.UpdateGioHangAsync(existingCartItem);
            }
            else
            {
                // Nếu sản phẩm chưa tồn tại, thêm mới
                var newCartItem = _mapper.Map<GioHangChiTiet>(addDto);

                // Generate a new ID if not provided
                if (addDto.Id == Guid.Empty)
                {
                    addDto.Id = Guid.NewGuid(); // Update the DTO
                }

                // Set các thuộc tính cho giỏ hàng mới
                newCartItem.Id = addDto.Id;
                newCartItem.IdGh = addDto.IdGh;
                newCartItem.IdSpct = addDto.IdSpct;
                newCartItem.SoLuong = addDto.SoLuong;
                newCartItem.DonGia = productPrice;
                newCartItem.DonGiaKhiGiam = donGiaKhiGiam;
                newCartItem.NgayTao = DateTime.Now;
                newCartItem.TrangThai = 0; // Giỏ hàng hoạt động

                // Thêm giỏ hàng vào repository
                await _gioHangRepo.AddGioHangAsync(newCartItem);
            }
        }





        // Update an existing cart item
        public async Task UpdateGioHangAsync(GioHangUpdate updateDto)
        {
            // Fetch the existing cart item from the repository
            var existingCartItem = await _gioHangRepo.GetGioHangById(updateDto.id);

            if (existingCartItem == null)
                throw new Exception("Cart item not found.");

            // Lấy sản phẩm chi tiết để kiểm tra số lượng tồn kho
            var productDetail = await _sPCTRepo.GetByIdSanPhamChiTiet(existingCartItem.IdSpct);
            if (productDetail == null)
            {
                throw new Exception("Không tìm thấy thông tin sản phẩm.");
            }

            // Kiểm tra số lượng tồn kho
            if (productDetail.SoLuongTon < updateDto.SoLuong)
            {
                var product = await _sanPhamRepo.GetByIdSanPham(productDetail.IdSp.Value);
                throw new Exception($"Không đủ số lượng sản phẩm {product.TenSp} trong kho.");
            }

            // Map DTO fields to the existing cart item
            existingCartItem.SoLuong = updateDto.SoLuong;
            // Set other fields if needed
            // Example: existingCartItem.IdSpct = updateDto.IdSpct;

            // Update the cart item in the repository
            await _gioHangRepo.UpdateGioHangAsync(existingCartItem);
        }

        // Delete a cart item
        public async Task DeleteGioHangAsync(Guid id)
        {
            var existingCartItem = await _gioHangRepo.GetGioHangByIdAsync(id);
            if (existingCartItem == null) throw new Exception("Cart item not found.");

            // Delete the cart item from the repository
            await _gioHangRepo.DeleteGioHangAsync(id);
        }

        public async Task<GioHang> GetByGioHang(Guid idKh)
        {
            return await _gioHangRepo.GetByGioHang(idKh);
        }
    }
}
