using AutoMapper;
using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using F5Clothes_Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F5Clothes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SPCTController : ControllerBase
    {
        private readonly ISanPhamChiTietServices _sanPhamChiTietSer;   
        public SPCTController(ISanPhamChiTietServices sanPhamChiTietSer)
        {
            _sanPhamChiTietSer = sanPhamChiTietSer;
        }
        [HttpGet]
        public async Task<List<SanPhamChiTiet>> GetAll()
        {
            return await _sanPhamChiTietSer.GetAllSanPhamChiTiet();
        }

        [HttpGet("{id}")]
        public async Task<SanPhamChiTiet> GetById(Guid id)
        {
            return await _sanPhamChiTietSer.GetByIdSanPhamChiTiet(id);
        }

        [HttpPost]
        public async Task<ActionResult> Add(SanPhamChiTietDtos sanPhamChiTietDto)
        {
            await _sanPhamChiTietSer.AddSanPhamChiTiet(sanPhamChiTietDto);
            return CreatedAtAction(nameof(GetById), new { id = sanPhamChiTietDto.Id }, sanPhamChiTietDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, SanPhamChiTietDtos sanPhamChiTietDto)
        {
            if (id != sanPhamChiTietDto.Id)
            {
                return BadRequest("ID không khớp");
            }

            try
            {
                await _sanPhamChiTietSer.UpdateSanPhamChiTiet(sanPhamChiTietDto);
                return Ok(sanPhamChiTietDto); // Trả về dữ liệu đã cập nhật
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _sanPhamChiTietSer.DeleteSanPhamChiTiet(id);
        }

    }
}
