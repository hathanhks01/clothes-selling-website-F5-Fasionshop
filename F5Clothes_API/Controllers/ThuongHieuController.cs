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
    public class ThuongHieuController : ControllerBase
    {
        private readonly IThuongHieuService _thuongHieuSer;

        public ThuongHieuController(IThuongHieuService thuongHieuSer)
        {
            _thuongHieuSer = thuongHieuSer;
        }
        [HttpGet]
        public async Task<List<ThuongHieu>> GetAll()
        {
            return await _thuongHieuSer.GetAllThuongHieu();
        }

        [HttpGet("{id}")]
        public async Task<ThuongHieu> GetById(Guid id)
        {
            return await _thuongHieuSer.GetByIdThuongHieu(id);
        }

        [HttpPost]
        public async Task<ActionResult> Add(ThuongHieuDtos thuongHieuDto)
        {
            await _thuongHieuSer.AddThuongHieu(thuongHieuDto);
            return CreatedAtAction(nameof(GetById), new { id = thuongHieuDto.Id }, thuongHieuDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ThuongHieuDtos thuongHieuDto)
        {
            if (id != thuongHieuDto.Id)
            {
                return BadRequest("ID không khớp");
            }

            try
            {
                await _thuongHieuSer.UpdateThuongHieu(thuongHieuDto);
                return Ok(thuongHieuDto); // Trả về dữ liệu đã cập nhật
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _thuongHieuSer.DeleteThuongHieu(id);
        }
    }
}
