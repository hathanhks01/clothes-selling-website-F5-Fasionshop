using Microsoft.AspNetCore.Mvc;
using F5Clothes_DAL.Models.VNPay;
using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.Models;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_Services.IServices;
using AutoMapper;
using F5Clothes_DAL.Reponsitories;

namespace F5Clothes_API.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IVoucherRepo _voucherRepo;
        private readonly IDiaChiRepo _diaChiRepo;
        private readonly IHDCTRepo _hDCTRepo;
        private readonly IGioHangRepo _gioHangRepo;
        private readonly IHoaDonRepo _hoaDonRepo;
        private readonly IVnPayService _vnPayService;
        private readonly IMapper _mapper;
        private readonly ISPCTRepo _sPCTRepo;
        private readonly IHinhThucThanhToanRepo _hinhThucThanhToanRepo;
        public CheckoutController(IVoucherRepo voucherRepo,IDiaChiRepo diaChiRepo, IHDCTRepo hDCTRepo, IGioHangRepo gioHangRepo, IHoaDonRepo hoaDonRepo, IVnPayService vnPayService, ISPCTRepo sPCTRepo, IMapper mapper, IHinhThucThanhToanRepo hinhThucThanhToanRepo)
        {
            _voucherRepo = voucherRepo;
            _diaChiRepo = diaChiRepo;
            _hDCTRepo = hDCTRepo;
            _gioHangRepo = gioHangRepo;
            _hoaDonRepo = hoaDonRepo;
            _vnPayService = vnPayService;
            _sPCTRepo = sPCTRepo;
            _mapper = mapper;
            _hinhThucThanhToanRepo = hinhThucThanhToanRepo;
        }

        [HttpPost("vnpay-payment")]
        public async Task<IActionResult> VNPayPayment([FromQuery] Guid customerId, [FromBody] OrderInfoDto orderInfo)
        {
            try
            {
                string diaChiNhanHang;
                const string DiaChiCuaHang = "Số 123 Đường ABC, Quận 1, TP. Hồ Chí Minh";

                // Xác định địa chỉ nhận hàng
                if (orderInfo.IdDiaChi == null)
                {
                    diaChiNhanHang = DiaChiCuaHang;
                }
                else
                {
                    var diaChiList = await _diaChiRepo.GetByDiaChi(customerId);
                    if (diaChiList == null || !diaChiList.Any())
                        throw new Exception("Không tìm thấy địa chỉ nào cho khách hàng.");

                    var diaChi = diaChiList.FirstOrDefault(x => x.Id == orderInfo.IdDiaChi)
                                 ?? diaChiList.FirstOrDefault();

                    if (diaChi == null || string.IsNullOrEmpty(diaChi.DiaChiChiTiet))
                        throw new Exception("Không có địa chỉ nhận hàng hợp lệ.");

                    diaChiNhanHang = $"{diaChi.DiaChiChiTiet}, {diaChi.PhuongXa}, {diaChi.QuanHuyen}, {diaChi.TinhThanh}";
                }

                // Kiểm tra giỏ hàng
                var cartItems = await _gioHangRepo.GetAllGioHangAsync(customerId);
                if (cartItems == null || !cartItems.Any())
                    throw new Exception("Giỏ hàng trống, không thể đặt hàng.");

                // Tính tổng tiền (sử dụng DonGiaKhiGiam nếu có, nếu không thì dùng DonGia)
                decimal tongTien = cartItems.Sum(item => item.SoLuong * (item.DonGiaKhiGiam ?? item.DonGia));

                // Áp dụng mã giảm giá
                decimal? giaTriGiam = await ApplyVoucherAsync(orderInfo, tongTien);
                tongTien -= giaTriGiam ?? 0;

                decimal? ThanhTien = tongTien + orderInfo.TienShip;

                // Tạo hóa đơn
                var maHoaDon = await _hoaDonRepo.GenerateMaHoaDon();
                var hoaDon = new HoaDon
                {
                    MaHoaDon = maHoaDon,
                    IdKh = customerId,
                    NgayTao = DateTime.Now,
                    TrangThai = 2, // Da xac nhan
                    LoaiHoaDon = 2, // Mua online
                    DiaChiNhanHang = diaChiNhanHang,
                    TenNguoiNhan = orderInfo.TenNguoiNhan,
                    SdtnguoiNhan = orderInfo.SdtNguoiNhan,
                    NgayNhanHang = orderInfo.NgayNhanHang,
                    IdVouCher = orderInfo.VoucherId,
                    ThanhTien = ThanhTien,
                    DonViGiaoHang = "GHN",
                    TienGiaoHang = orderInfo.TienShip,
                    NgayThanhToan = DateTime.Now,
                    GiaTriGiam = giaTriGiam,
                    GhiChu = orderInfo.GhiChu
                };
                await _hoaDonRepo.AddHdgioHang(hoaDon);

                // Cập nhật voucher sau khi tạo hóa đơn thành công
                if (orderInfo.VoucherId.HasValue)
                {
                    var voucher = await _voucherRepo.GetByVouCher(orderInfo.VoucherId.Value);
                    voucher.SoLuongMa--;
                    voucher.SoLuongDung++;
                    await _voucherRepo.UpdateVc(voucher);
                }

                // Create VNPay model for payment
                var vnPayModel = new PaymentInformationModel
                {
                    Amount = (double)ThanhTien,
                    OrderDescription = $"Thanh toán đơn hàng {maHoaDon}",
                    FullName = orderInfo.TenNguoiNhan,
                    OrderType = Guid.NewGuid().ToString() // Generate a unique transaction code
                };

                // Tạo URL thanh toán VNPay
                var paymentUrl = _vnPayService.CreatePaymentUrl(vnPayModel, HttpContext);

                // Create HinhThucThanhToan record with COD payment method
                var hinhThucThanhToan = new HinhThucThanhToan
                {
                    IdHd = hoaDon.Id,
                    IdKh = customerId,
                    HinhThucThanhToan1 = 2, // Assuming 1 represents "VNPAY"
                    TrangThai = 1,  // Assuming 0 means "Not Paid"
                    
                    MaGiaoDich = vnPayModel.OrderType,  // VNPay order type (transaction code)
                    SoTienTra = (decimal)vnPayModel.Amount,
                    GhiChu = "Thanh toán VNPAY"
                };
                await _hinhThucThanhToanRepo.AddHTt(hinhThucThanhToan);

                // Xử lý từng sản phẩm trong giỏ hàng
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

                return Ok(new { paymentUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Đã xảy ra lỗi: {ex.Message}");
            }
        }




        private async Task<decimal?> ApplyVoucherAsync(OrderInfoDto orderInfo, decimal tongTien)
        {
            if (!orderInfo.VoucherId.HasValue)
                return 0;

            var voucher = await _voucherRepo.GetByVouCher(orderInfo.VoucherId.Value);
            if (voucher == null || voucher.TrangThai == 2)
                throw new Exception("Voucher không hợp lệ hoặc đã hết hiệu lực.");

            var now = DateTime.UtcNow;
            if (DateTime.Compare(voucher.NgayBatDau.Value, now) > 0 || DateTime.Compare(voucher.NgayKetThuc.Value, now) < 0)
                throw new Exception("Voucher không còn hiệu lực.");

            if (voucher.SoLuongMa <= 0)
                throw new Exception("Voucher đã hết số lượng sử dụng.");

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

        public IActionResult PaymentCallBack()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            if (response == null || response.VnPayResponseCode != "00")
            {
                TempData["Message"] = response == null
                    ? "Không nhận được phản hồi từ VNPay."
                    : $"Lỗi thanh toán VN Pay: {response.VnPayResponseCode}";
                return RedirectToAction("PaymentFail");
            }


            TempData["Message"] = $"Thanh toán VNPay thành công";
            return RedirectToAction("PaymentSuccess");
        }
    }
}
