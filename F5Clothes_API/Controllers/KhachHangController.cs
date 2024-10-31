using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.Models;

using F5Clothes_Services.IServices;
using F5Clothes_Services.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F5Clothes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly IKhachHangService _KhachHangSev;
        private readonly IMapper _mapper;

        public KhachHangController(IKhachHangService khSev, IMapper mapper)
        {
            _KhachHangSev = khSev;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhachHangDtos>>> GetAllKh()
        {
            var KhachHangList = await _KhachHangSev.GetAllKhachHang();
            var mappeKh = _mapper.Map<List<KhachHangDtos>>(KhachHangList);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappeKh);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByKhachhang(Guid id)
        {
            var khachHang = await _KhachHangSev.GetByKhachHang(id);
            if (khachHang == null)
            {
                return NotFound($"Customer with ID {id} not found.");
            }

            var mappeKh = _mapper.Map<KhachHangDtos>(khachHang);
            return Ok(mappeKh);
        }
        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> UpdateKhachHang(Guid id, [FromBody] KhachHangDtos khachHangDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingKhachHang = await _KhachHangSev.GetByKhachHang(id);
            if (existingKhachHang == null)
            {
                return NotFound(new { message = $"Customer with ID {id} not found." });
            }

            // Cập nhật các thuộc tính cụ thể từ DTO
            existingKhachHang.HoVaTenKh = khachHangDto.HoVaTenKh;
            existingKhachHang.GioiTinh = khachHangDto.GioiTinh;
            existingKhachHang.NgaySinh = khachHangDto.NgaySinh != null ? khachHangDto.NgaySinh : existingKhachHang.NgaySinh;
            existingKhachHang.TaiKhoan = khachHangDto.TaiKhoan;
            existingKhachHang.SoDienThoai = khachHangDto.SoDienThoai;
            existingKhachHang.Email = khachHangDto.Email;
            existingKhachHang.Image = khachHangDto.Image;

            var result = await _KhachHangSev.UpdateKh(existingKhachHang);
            if (result == null)
            {
                return StatusCode(422, "Unable to update the customer. Please check the details.");
            }

            var updatedKhachHangDto = _mapper.Map<KhachHangDtos>(existingKhachHang);
            return Ok(updatedKhachHangDto);
        }
        [HttpPatch("change-password/{id:guid}")]
        public async Task<IActionResult> ChangePassword(Guid id, [FromBody] ChangePasswordDto changePasswordDto)
        {
            if (changePasswordDto == null || string.IsNullOrWhiteSpace(changePasswordDto.OldPassword) || string.IsNullOrWhiteSpace(changePasswordDto.NewPassword))
            {
                return BadRequest("Invalid request data.");
            }

            // Tìm khách hàng theo ID
            var khachHang = await _KhachHangSev.GetByKhachHang(id);
            if (khachHang == null)
            {
                return NotFound("Customer not found.");
            }

            // Xác thực mật khẩu cũ
            if (!BCrypt.Net.BCrypt.Verify(changePasswordDto.OldPassword, khachHang.MatKhau))
            {
                return BadRequest("Old password is incorrect.");
            }

            // Mã hóa mật khẩu mới và cập nhật
            khachHang.MatKhau = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);
            await _KhachHangSev.UpdateKh(khachHang);

            return Ok("Password changed successfully.");
        }



    }
}
