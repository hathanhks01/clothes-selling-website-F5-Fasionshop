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
    public class DiaChiController : ControllerBase
    {
        private readonly IDiaChiRepo _DiaChiRepo;
        private readonly IMapper _mapper;

        public DiaChiController(IDiaChiRepo dcRepo, IMapper mapper)
        {
            _DiaChiRepo = dcRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiaChi>>> GetAll()
        {
            var DiaChiList = await _DiaChiRepo.GetAllDiaChi();
            var mappeddc = _mapper.Map<List<DiaChiDtos>>(DiaChiList);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mappeddc);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllDiaChiByCustomerId(Guid id)
        {
            // Lấy danh sách các địa chỉ của khách hàng từ repository
            var diaChiList = await _DiaChiRepo.GetByDiaChi(id);

            // Nếu không tìm thấy địa chỉ nào, trả về NotFound
            if (diaChiList == null || !diaChiList.Any())
            {
                return NotFound("Không tìm thấy địa chỉ cho khách hàng này.");
            }

            // Chuyển đổi danh sách DiaChi sang DTO
            var mappeddc = _mapper.Map<List<DiaChiDtos>>(diaChiList);

            // Trả về danh sách địa chỉ đã được map sang DTO
            return Ok(mappeddc);
        }


        [HttpPost]
        public async Task<IActionResult> DiaChiAdd(DiaChiDtos dc)
        {
            if (dc == null || dc.IdKh == null)
            {
                return BadRequest("Thông tin địa chỉ hoặc Id khách hàng không hợp lệ.");
            }

            try
            {
                var diaChi = await _DiaChiRepo.Adddc(dc); // Gọi phương thức Adddc từ repository để thêm hoặc cập nhật
                return Ok(diaChi); // Trả về địa chỉ sau khi thêm hoặc cập nhật thành công
            }
            catch (Exception ex)
            {
                // Log lỗi (nếu cần)
                return StatusCode(500, "Đã xảy ra lỗi khi lưu thông tin địa chỉ: " + ex.Message);
            }
        }


        [HttpPut]
        public async Task Update(DiaChi dc)
        {
            await _DiaChiRepo.Updatedc(dc);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _DiaChiRepo.Deletedc(id);
        }
    }
}
