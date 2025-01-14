﻿using AutoMapper;
using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F5Clothes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly IKhachhangRepo _KhachHangRepo;
        private readonly IMapper _mapper;

        public KhachHangController(IKhachhangRepo khRepo, IMapper mapper)
        {
            _KhachHangRepo = khRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhachHang>>> GetAllKh()
        {
            var KhachHangList = await _KhachHangRepo.GetAllKhachHang();
            var mappeKh = _mapper.Map<List<KhachHangDtos>>(KhachHangList);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappeKh);
        }

        [HttpGet("profile-by-username/{username}")]


        public async Task<IActionResult> GetByTK(string username)
        {
            // Fetch the customer data by username
            var khachHang = await _KhachHangRepo.GetByTK(username);

            // Check if the customer data is null
            if (khachHang == null)
            {
                return NotFound(new { message = "Customer not found" });
            }

            // Map the customer data to KhachHangDtos
            var mappedKh = _mapper.Map<KhachHangDtos>(khachHang);

            // Validate the model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappedKh);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByKhachhang(Guid id)
        {


            var mappeKh = _mapper.Map<KhachHangDtos>(await _KhachHangRepo.GetByKhachHang(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return Ok(mappeKh);
        }
        [HttpPut]
        public async Task Update(KhachHang Kh)
        {
            await _KhachHangRepo.UpdateKh(Kh);
        }
        [HttpGet("ma-khach-hang/{maKH}")]
        public async Task<IActionResult> GetByMaKhachHang(string maKH)
        {
            var khachHang = await _KhachHangRepo.GetByMaKhachHang(maKH);
            if (khachHang == null)
            {
                return NotFound($"Khách hàng với mã {maKH} không tồn tại.");
            }

            var mappeKh = _mapper.Map<KhachHangDtos>(khachHang);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappeKh);
        }
        [HttpGet("list")]
        public async Task<IActionResult> GetList([FromQuery] ListKhachHangModel model)
        {
            try
            {
                var list = await _KhachHangRepo.GetKhachHang(model);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch("profile/{id:guid}")]
        public async Task<IActionResult> UpdateProfile(Guid id, [FromBody] KhachHangProfileUpdateDto profileUpdateDto)
        {
            if (profileUpdateDto == null)
            {
                return BadRequest("Profile data is required.");
            }

            var success = await _KhachHangRepo.UpdateProfile(id, profileUpdateDto);

            if (!success)
            {
                return NotFound("Customer not found.");
            }

            return Ok("Profile updated successfully.");
        }

        [HttpPatch("change-password/{id:guid}")]
        public async Task<IActionResult> ChangePassword(Guid id, [FromQuery] string oldPassword, [FromQuery] string newPassword)
        {
            var success = await _KhachHangRepo.ChangePassword(id, oldPassword, newPassword);

            if (!success)
            {
                return BadRequest("Password change failed. Ensure that the old password is correct.");
            }

            return Ok("Password changed successfully.");
        }
    }
}
