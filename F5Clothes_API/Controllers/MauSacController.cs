using AutoMapper;
using F5Clothes_DAL.DTOs;
using F5Clothes_DAL.IReponsitories;
using F5Clothes_DAL.Models;
using F5Clothes_DAL.Reponsitories;
using F5Clothes_Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F5Clothes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MauSacController : ControllerBase
    {
        private readonly IMauSacServices _mauSacSer;
        public MauSacController(IMauSacServices mauSacSer)
        {
            _mauSacSer = mauSacSer;
        }
        [HttpGet]
        public async Task<List<MauSac>> GetAll()
        {
            return await _mauSacSer.GetAllMauSac();
        }

        [HttpGet("{id}")]
        public async Task<MauSac> GetById(Guid id)
        {
            return await _mauSacSer.GetByIdMauSac(id);
        }

        [HttpPost]
        public async Task<ActionResult> Add(MauSacDtos mauSacDto)
        {
            await _mauSacSer.AddMauSac(mauSacDto);
            return CreatedAtAction(nameof(GetById), new { id = mauSacDto.Id }, mauSacDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, MauSacDtos mauSacDto)
        {
            if (id != mauSacDto.Id)
            {
                return BadRequest("ID không khớp");
            }

            try
            {
                await _mauSacSer.UpdateMauSac(mauSacDto);
                return Ok(mauSacDto); // Trả về dữ liệu đã cập nhật
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _mauSacSer.DeleteMauSac(id);
        }
    }
}
