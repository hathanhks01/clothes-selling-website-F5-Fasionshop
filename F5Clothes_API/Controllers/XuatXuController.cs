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
    public class XuatXuController : ControllerBase
    {
        private readonly IXuatXuService _xuatXuSer;

        public XuatXuController(IXuatXuService xuatXuSer)
        {
            _xuatXuSer = xuatXuSer;
        }

        [HttpGet]
        public async Task<List<XuatXu>> GetAll()
        {
            return await _xuatXuSer.GetAllXuatXu();
        }

        [HttpGet("{id}")]
        public async Task<XuatXu> GetById(Guid id)
        {
            return await _xuatXuSer.GetByIdXuatXu(id);
        }

        [HttpPost]
        public async Task<ActionResult> Add(XuatXuDtos xuatXuDto)
        {
            await _xuatXuSer.AddXuatXu(xuatXuDto);
            return CreatedAtAction(nameof(GetById), new { id = xuatXuDto.Id }, xuatXuDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, XuatXuDtos xuatXuDto)
        {
            if (id != xuatXuDto.Id)
            {
                return BadRequest("ID không khớp");
            }

            try
            {
                await _xuatXuSer.UpdateXuatXu(xuatXuDto);
                return Ok(xuatXuDto); // Trả về dữ liệu đã cập nhật
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _xuatXuSer.DeleteXuatXu(id);
        }
    }
}
