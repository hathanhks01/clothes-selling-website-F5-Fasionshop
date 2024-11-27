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
        [HttpGet("store")]
        public async Task<IEnumerable<object>> GetAllSanPhamsAsync()
        {
            return await _sanPhamRepo.GetAllSanPhamsWithDetailsAsync();
        }

        [HttpGet("details/{id}")]
        public async Task<object> GetSanPhamDetailsByIdAsync(Guid id)
        {
            return await _sanPhamRepo.GetSanPhamWithDetailsAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult> Add(SanPhamDtos sanPhamDto)
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
    } 

}
