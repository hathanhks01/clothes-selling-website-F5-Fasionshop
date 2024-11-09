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
    public class GiamGiaController : ControllerBase
    {
        private readonly IGiamGiaService _giamGiaSer;

        public GiamGiaController(IGiamGiaService giamGiaSer)
        {
            _giamGiaSer = giamGiaSer;
        }

        [HttpGet]
        public async Task<List<GiamGium>> GetAll()
        {
            return await _giamGiaSer.GetAllGiamGia();
        }

        [HttpGet("{id}")]
        public async Task<GiamGium> GetById(Guid id)
        {
            return await _giamGiaSer.GetByIdGiamGia(id);
        }

        [HttpPost]
        public async Task<ActionResult> Add(GiamGiaDtos giamGiaDto)
        {
            await _giamGiaSer.AddGiamGia(giamGiaDto);
            return CreatedAtAction(nameof(GetById), new { id = giamGiaDto.Id }, giamGiaDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, GiamGiaDtos giamGiaDto)
        {
            if (id != giamGiaDto.Id)
            {
                return BadRequest("ID không khớp");
            }

            try
            {
                await _giamGiaSer.UpdateGiamGia(giamGiaDto);
                return Ok(giamGiaDto); // Trả về dữ liệu đã cập nhật
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _giamGiaSer.DeleteGiamGia(id);
        }
    }
}
