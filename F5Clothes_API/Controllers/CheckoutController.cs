﻿using Microsoft.AspNetCore.Mvc;
using F5Clothes_DAL.Models.VNPay;
using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.Models;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_Services.IServices;
using AutoMapper;

namespace F5Clothes_API.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IDiaChiRepo _diaChiRepo;
        private readonly IGioHangRepo _gioHangRepo;
        private readonly IHoaDonRepo _hoaDonRepo;
        private readonly IVnPayService _vnPayService;
        private readonly IMapper _mapper;
        public CheckoutController(IDiaChiRepo diaChiRepo, IGioHangRepo gioHangRepo, IHoaDonRepo hoaDonRepo, IVnPayService vnPayService, IMapper mapper)
        {
            _diaChiRepo = diaChiRepo;
            _gioHangRepo = gioHangRepo;
            _hoaDonRepo = hoaDonRepo;
            _vnPayService = vnPayService;
            _mapper = mapper;
        }

        [HttpPost("vnpay-payment")]
        public async Task<IActionResult> VNPayPayment(Guid customerId, OrderInfoDto orderInfo)
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
                    return BadRequest("Giỏ hàng trống, không thể đặt hàng.");

                // Tính tổng tiền
                decimal tongTien = cartItems.Sum(item =>
                    item.SoLuong * (item.DonGiaKhiGiam.HasValue && item.DonGiaKhiGiam > 0
                        ? item.DonGiaKhiGiam.Value
                        : item.DonGia)
                );

                // Áp dụng voucher
                decimal? giaTriGiam = await ApplyVoucherAsync(orderInfo, tongTien);
                tongTien -= giaTriGiam ?? 0;

                // Tạo hóa đơn
                var maHoaDon = await _hoaDonRepo.GenerateMaHoaDon();
                var hoaDon = new HoaDon
                {
                    MaHoaDon = maHoaDon,
                    IdKh = customerId,
                    NgayTao = DateTime.Now,
                    TrangThai = 1,
                    LoaiHoaDon = 2,
                    DiaChiNhanHang = diaChiNhanHang,
                    TenNguoiNhan = orderInfo.TenNguoiNhan,
                    SdtnguoiNhan = orderInfo.SdtNguoiNhan,
                    NgayNhanHang = orderInfo.NgayNhanHang,
                    IdVouCher = orderInfo.VoucherId,
                    ThanhTien = tongTien,
                    GiaTriGiam = giaTriGiam,
                    GhiChu = orderInfo.GhiChu
                };

                await _hoaDonRepo.AddHdgioHang(hoaDon);

                // Tạo URL thanh toán
                var vnPayModel = new PaymentInformationModel
                {
                    Amount = (double)tongTien*100,
                    OrderDescription = $"Thanh toán đơn hàng {maHoaDon}",
                    FullName = orderInfo.TenNguoiNhan,
                    OrderType = Guid.NewGuid().ToString(), // Sử dụng GUID thay cho Random
                };

                var paymentUrl = _vnPayService.CreatePaymentUrl(vnPayModel, HttpContext);
                return Ok(new { paymentUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Đã xảy ra lỗi: {ex.Message}");
            }
        }


        private async Task<decimal?> ApplyVoucherAsync(OrderInfoDto orderInfo, decimal tongTien)
        {
            return await Task.FromResult<decimal?>(0);
        }

        public IActionResult PaymentCallBack()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            if (response == null || response.VnPayResponseCode != "00")
            {
                TempData["Message"] = $"Lỗi thanh toán VN Pay: {response.VnPayResponseCode}";
                return RedirectToAction("PaymentFail");
            }

            TempData["Message"] = $"Thanh toán VNPay thành công";
            return RedirectToAction("PaymentSuccess");
        }
    }
}