using AutoMapper;
using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using F5Clothes_Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace F5Clothes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly ISanPhamServices _sanPhamSer;
        private readonly ISanPhamRepo _sanPhamRepo;
        public SanPhamController(ISanPhamServices sanPhamSer, ISanPhamRepo sanPhamRepo)
        {
            _sanPhamSer = sanPhamSer;
            _sanPhamRepo = sanPhamRepo;
        }
        [HttpGet("GetAll")]
        public async Task<IEnumerable<object>> GetAll()
        {
            return await _sanPhamRepo.GetAllSanPham();
        }

        [HttpGet("{id}")]
        public async Task<SanPham> GetById(Guid id)
        {
            return await _sanPhamSer.GetByIdSanPham(id);
        }
        [HttpGet("sanPhamChiTiet/{id}")]
        public async Task<SanPhamChiTiet> GetByIdSanPhamChiTiet(Guid id)
        {
            return await _sanPhamRepo.GetByIdSanPhamChiTiet(id);
        }

        
        [HttpGet("store")]
        public async Task<IEnumerable<object>> GetAllSanPhamsAsync()
        {
            return await _sanPhamRepo.GetAllSanPhamsWithDetailsAsync();
        }
        [HttpGet("Image")]
        public async Task<IEnumerable<object>> GetAllImageBySanPham()
        {
            return await _sanPhamRepo.GetAllImageBySanPham();
        }
        [HttpGet("details/{id}")]
        public async Task<object> GetSanPhamDetailsByIdAsync(Guid id)
        {
            return await _sanPhamRepo.GetSanPhamWithDetailsAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult> Add([FromBody]SanPhamDtos sanPhamDto)
        {
            await _sanPhamSer.AddSanPham(sanPhamDto);
            return CreatedAtAction(nameof(GetById), new { id = sanPhamDto.Id }, sanPhamDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SanPhamDtos sanPhamDto)
        {
            if (id != sanPhamDto.Id)
            {
                return BadRequest("ID không khớp");
            }

            try
            {
                await _sanPhamSer.UpdateSanPham(sanPhamDto);
                return Ok(sanPhamDto); // Trả về dữ liệu đã cập nhật
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _sanPhamSer.DeleteSanPham(id);
        }
        [HttpPost]
        [Route("AddOrUpdate")]
        public async Task<IActionResult> AddOrUpdateSanPhamChiTiet([FromBody] SanPhamChiTietDtos chiTietDtos)
        {
            await _sanPhamRepo.AddOrUpdateSanPhamChiTiet(chiTietDtos);
            return CreatedAtAction(nameof(GetByIdSanPhamChiTiet), new { id = chiTietDtos.Id }, chiTietDtos);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateSanPhamChiTiet(Guid sanPhamId, [FromBody] IEnumerable<SanPhamChiTietDtos> chiTietDtos)
        {
            if (chiTietDtos == null)
            {
                return BadRequest("Chi tiết sản phẩm không hợp lệ.");
            }

            try
            {
                await _sanPhamRepo.UpdateSanPhamChiTiet(sanPhamId, chiTietDtos);
                return Ok("Chi tiết sản phẩm đã được cập nhật thành công.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Đã xảy ra lỗi: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("GetBySanPhamId/{sanPhamId}")]
        public async Task<IActionResult> GetSanPhamChiTietBySanPhamId(Guid sanPhamId)
        {
            try
            {
                var chiTietSanPhams = await _sanPhamRepo.GetSanPhamChiTietBySanPhamId(sanPhamId);
                if (chiTietSanPhams == null || !chiTietSanPhams.Any())
                {
                    return NotFound("Không tìm thấy chi tiết sản phẩm cho sản phẩm này.");
                }

                return Ok(chiTietSanPhams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Đã xảy ra lỗi: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("AddOrUpdateImage")]
        public async Task<IActionResult> AddOrUpdateHinhAnhChiTiet([FromBody] ImageDtos chiTietDtos)
        {
            await _sanPhamRepo.AddOrUpdateHinhAnhChiTiet(chiTietDtos);
            return CreatedAtAction(nameof(GetById), new { id = chiTietDtos.Id }, chiTietDtos);
        }
    } 

}
