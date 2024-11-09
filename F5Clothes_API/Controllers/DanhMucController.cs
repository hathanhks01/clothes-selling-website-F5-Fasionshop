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
    public class DanhMucController : ControllerBase
    {
        private readonly IDanhMucService _danhMucSer;
        public DanhMucController(IDanhMucService danhMucSer)
        {
            _danhMucSer = danhMucSer;
        }
        [HttpGet]
        public async Task<List<DanhMuc>> GetAll()
        {
            return await _danhMucSer.GetAllDanhMuc();
        }

        [HttpGet("{id}")]
        public async Task<DanhMuc> GetById(Guid id)
        {
            return await _danhMucSer.GetByIdDanhMuc(id);
        }

        [HttpPost]
        public async Task<ActionResult> Add(DanhMucDtos danhMucDto)
        {
            await _danhMucSer.AddDanhMuc(danhMucDto);
            return CreatedAtAction(nameof(GetById), new { id = danhMucDto.Id }, danhMucDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, DanhMucDtos danhMucDto)
        {
            if (id != danhMucDto.Id)
            {
                return BadRequest("ID không khớp");
            }

            try
            {
                await _danhMucSer.UpdateDanhMuc(danhMucDto);
                return Ok(danhMucDto); // Trả về dữ liệu đã cập nhật
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _danhMucSer.DeleteDanhMuc(id);
        }

    }
}
