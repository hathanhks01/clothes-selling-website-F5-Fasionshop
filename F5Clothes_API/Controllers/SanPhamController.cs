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
        public SanPhamController(ISanPhamServices sanPhamSer)
        {
            _sanPhamSer = sanPhamSer;
        }
        [HttpGet]
        public async Task<List<SanPham>> GetAll()
        {
            return await _sanPhamSer.GetAllSanPham();
        }

        [HttpGet("{id}")]
        public async Task<SanPham> GetById(Guid id)
        {
            return await _sanPhamSer.GetByIdSanPham(id);
        }

        [HttpPost]
        public async Task<ActionResult> Add(SanPhamDtos sanPhamDto)
        {
            await _sanPhamSer.AddSanPham(sanPhamDto);
            return CreatedAtAction(nameof(GetById), new { id = sanPhamDto.Id }, sanPhamDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, SanPhamDtos sanPhamDto)
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
