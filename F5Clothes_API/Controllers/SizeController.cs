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
    public class SizeController : ControllerBase
    {
        private readonly ISizeServices _sizeSer;

        public SizeController(ISizeServices sizeSer)
        {
            _sizeSer = sizeSer;
        }

        [HttpGet]
        public async Task<List<Size>> GetAll()
        {
            return await _sizeSer.GetAllSize();
        }

        [HttpGet("{id}")]
        public async Task<Size> GetById(Guid id)
        {
            return await _sizeSer.GetByIdSize(id);
        }

        [HttpPost]
        public async Task<ActionResult> Add(SizeDtos sizeDto)
        {
            await _sizeSer.AddSize(sizeDto);
            return CreatedAtAction(nameof(GetById), new { id = sizeDto.Id }, sizeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, SizeDtos sizeDto)
        {
            if (id != sizeDto.Id)
            {
                return BadRequest("ID không khớp");
            }

            try
            {
                await _sizeSer.UpdateSize(sizeDto);
                return Ok(sizeDto); // Trả về dữ liệu đã cập nhật
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _sizeSer.DeleteSize(id);
        }
    }
}
